using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snapkart.Contract;
using Snapkart.Domain.Constants;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Extensions;
using Snapkart.Domain.Interfaces;
using Snapkart.Infrastructure.Data;

namespace Snapkart.Controllers
{
    public class PostsController : AuthorizedEndpoint
    {
        private readonly ICrudRepository<SnapQuery> _postRepository;
        private readonly ApplicationDbContext _dbContext;
        private readonly ICrudRepository<Bid> _bidRepository;
        private readonly UserManager<AppUser> _userManager;

        public PostsController(ICrudRepository<Bid> bidRepository, UserManager<AppUser> userManager,
            ISnapQueryRepository postRepository, ApplicationDbContext dbContext)
        {
            _postRepository = postRepository;
            _dbContext = dbContext;
            _bidRepository = bidRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts([FromQuery] PagingQuery query)
        {
            var user = await _dbContext.Users.Include(x => x.Subscriptions)
                .FirstOrDefaultAsync(x => x.Id == User.GetUserId());

            List<SnapPostDto> posts;
            if (user.Role == UserRole.Merchant)
            {
                var subscriptions = user.Subscriptions?.Select(x => x.CategoryId).ToList();
                //only show items within his subscription and area
                posts = await _postRepository.Query()
                    .Include(x => x.AppUser)
                    .Where(x => subscriptions.Contains(x.CategoryId) && x.AreaId == user.AreaId)
                    .Select(x => new SnapPostDto()
                    {
                        Id = x.Id,
                        CreatedBy = x.AppUser.Name,
                        UserImageUrl = x.AppUser.ImageUrl,
                        CreatedAt = x.CreatedAt,
                        AreaId = x.AreaId,
                        CityId = x.CityId,
                        Description = x.Description,
                        ImageUrl = x.ImageUrl,
                        CategoryId = x.CategoryId,
                        Location = x.Location,
                        Bids = x.Bids.Count,
                        Likes = x.Likes.Count,
                    })
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(query.Skip())
                    .Take(query.PageSize)
                    .ToListAsync();
            }
            else
            {
                posts = await _postRepository.Query()
                    .Select(x => new SnapPostDto()
                    {
                        Id = x.Id,
                        CreatedBy = x.AppUser.Name,
                        UserImageUrl = x.AppUser.ImageUrl,
                        CreatedAt = x.CreatedAt,
                        AreaId = x.AreaId,
                        CityId = x.CityId,
                        Location = x.Location,
                        Description = x.Description,
                        ImageUrl = x.ImageUrl,
                        CategoryId = x.CategoryId,
                        Bids = x.Bids.Count,
                        Likes = x.Likes.Count,
                    })
                    .OrderByDescending(x => x.CreatedAt)
                    .Skip(query.Skip())
                    .Take(query.PageSize)
                    .ToListAsync();
            }

            return Ok(Envelope.Ok(posts));
        }

        [HttpGet("{id}/bids")]
        public async Task<IActionResult> GetBids(int id)
        {
            var post = await _postRepository.FindById(id);
            if (post == null)
            {
                return BadRequest(Envelope.Error("post not found"));
            }

            List<Bid> bids = new List<Bid>();
            if (User.GetUserRole() == UserRole.Merchant.ToString())
            {
                //only show his own bids
                bids = await _bidRepository.Query().Include(x => x.Maker)
                    .Where(x => x.SnapQueryId == id && x.MakerId == User.GetUserId())
                    .ToListAsync();
            }
            else if (post.AppUserId == User.GetUserId())
            {
                bids = await _bidRepository.Query().Include(x => x.Maker).Where(x => x.SnapQueryId == id).ToListAsync();
            }

            return Ok(Envelope.Ok(bids.Select(x => new BidResponseDto(x))));
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto dto)
        {
            var validator = new CreatePostDtoValidator();
            var validate = await validator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return BadRequest(Envelope.Error(validate.Errors.FirstOrDefault()?.ErrorMessage));
            }

            var city = await _dbContext.Cities.Include(x => x.Areas).FirstOrDefaultAsync(x => x.Id == dto.CityId);
            var area = city?.Areas.FirstOrDefault(x => x.Id == dto.AreaId);

            if (city == null || area == null)
            {
                return BadRequest(Envelope.Error("invalid location"));
            }

            var user = await _dbContext.Users.Include(x => x.Subscriptions)
                .FirstOrDefaultAsync(x => x.Id == User.GetUserId());

            var query = new SnapQuery(dto.Details, dto.Image, dto.CategoryId,
                dto.TagIds, area, city);

            user.Add(query);

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return Ok(Envelope.Ok());
        }

        [Authorize(Roles = "Customer")]
        [HttpPost("{id}/bids/{bidId}/accept")]
        public async Task<IActionResult> AcceptBid(int id, int bidId)
        {
            var post = await _postRepository.FindById(id);
            if (post == null)
            {
                return BadRequest(Envelope.Error("post not found"));
            }

            var accept = post.Accept(User.GetUserId(), bidId);
            if (accept.IsFailure)
            {
                return BadRequest(Envelope.Error(accept.Error));
            }

            accept.Value.AddNotification(new AppNotification("SnapQuery", id,
                $"User has accepted your bid, you can now contact with him"));

            await _postRepository.Update(post);
            await _userManager.UpdateAsync(accept.Value);

            return Ok(Envelope.Ok(new ContactDetailResponseDto(accept.Value)));
        }

        [HttpPost("{id}/likes")]
        public async Task<IActionResult> Like(int id)
        {
            var post = await _postRepository.FindById(id);
            if (post == null)
            {
                return BadRequest(Envelope.Error("post not found"));
            }

            post.LikedBy(User.GetUserId());
            await _postRepository.Update(post);
            return Ok(Envelope.Ok());
        }

        [Authorize(Roles = "Merchant")]
        [HttpPost("{id}/bids")]
        public async Task<IActionResult> MakeBid(int id, [FromForm] CreateBidDto dto)
        {
            var validator = new CreateBidDtoValidator();
            var validate = await validator.ValidateAsync(dto);
            if (!validate.IsValid)
            {
                return BadRequest(Envelope.Error(validate.Errors.FirstOrDefault()?.ErrorMessage));
            }

            var user = await _userManager.FindByIdAsync(User.GetUserId());
            var post = await _postRepository.FindById(id);
            if (post == null)
            {
                return BadRequest(Envelope.Error("post not found"));
            }

            var bid = new Bid(dto.Image, dto.Details, dto.Price);
            var makeBid = user.Add(bid);

            if (makeBid.IsSuccess)
            {
                post.Add(bid);
                await _userManager.UpdateAsync(user);
                await _postRepository.Update(post);

                return Ok(Envelope.Ok());
            }

            return BadRequest(Envelope.Error(makeBid.Error));
        }
    }
}