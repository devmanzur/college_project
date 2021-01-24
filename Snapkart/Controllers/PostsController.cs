using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Extensions;
using Snapkart.Domain.Interfaces;
using Snapkart.Infrastructure.Data;

namespace Snapkart.Controllers
{
    public class PostsController : AuthorizedEndpoint
    {
        private readonly ICrudRepository<SnapQuery> _postRepository;
        private readonly ICrudRepository<Bid> _bidRepository;
        private readonly IImageServerBroker _imageServerBroker;
        private readonly UserManager<AppUser> _userManager;

        public PostsController(ICrudRepository<Bid> bidRepository,
            IImageServerBroker imageServerBroker, UserManager<AppUser> userManager, ISnapQueryRepository postRepository)
        {
            _postRepository = postRepository;
            _bidRepository = bidRepository;
            _imageServerBroker = imageServerBroker;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.Query().ToListAsync();
            return Ok(Envelope.Ok(posts));
        }

        [HttpGet("{id}/bids")]
        public async Task<IActionResult> GetBids(int id)
        {
            var bids = await _bidRepository.Query().Where(x => x.SnapQueryId == id).ToListAsync();
            return Ok(Envelope.Ok(bids.Select(x => new BidDto(x))));
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromForm] CreatePostDto dto)
        {
            var imageUpload = await _imageServerBroker.UploadImage(dto.Image);

            if (imageUpload.IsSuccess)
            {
                await _postRepository.Create(new SnapQuery(dto.Details, imageUpload.Value, dto.CategoryId, dto.TagIds));
                return Ok(Envelope.Ok());
            }

            return BadRequest(Envelope.Error(imageUpload.Error));
        }

        [HttpPost("{id}/bids/{bidId}/accept")]
        public async Task<IActionResult> AcceptBid(int id, int bidId)
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId());
            var post = await _postRepository.FindById(id);
            var bidMaker = post.Accept(user, bidId);
            if (bidMaker == null)
            {
                return BadRequest(Envelope.Error("Bid not found"));
            }

            bidMaker.AddNotification(new AppNotification("SnapQuery", id,
                $"{user.Name} has accepted your bid, you can now contact with him"));

            await _postRepository.Update(post);
            await _userManager.UpdateAsync(bidMaker);

            return Ok(Envelope.Ok(new ContactDetailDto(bidMaker)));
        }

        [HttpPost("{id}/bids")]
        public async Task<IActionResult> MakeBid(int id, [FromForm] CreateBidDto dto)
        {
            var user = await _userManager.FindByIdAsync(User.GetUserId());
            var post = await _postRepository.FindById(id);

            var imageUpload = await _imageServerBroker.UploadImage(dto.Image);
            if (imageUpload.IsSuccess)
            {
                var bid = new Bid(imageUpload.Value, dto.Details, dto.Price);
                user.AddBid(bid);
                post.MakeBid(bid);

                await _userManager.UpdateAsync(user);
                await _postRepository.Update(post);

                return Ok(Envelope.Ok());
            }

            return BadRequest(Envelope.Error(imageUpload.Error));
        }
    }
}