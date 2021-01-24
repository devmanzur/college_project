using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Snapkart.Contract;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Extensions;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Controllers
{
    public class ProfileController : AuthorizedEndpoint
    {
        private readonly IAppUserService _appUserService;
        private readonly ICrudRepository<AppNotification> _notificationRepo;

        public ProfileController(IAppUserService appUserService, ICrudRepository<AppNotification> notificationRepo)
        {
            _appUserService = appUserService;
            _notificationRepo = notificationRepo;
        }

        [HttpGet]
        public async Task<IActionResult> MyProfile()
        {
            var user = new SignedInUser()
            {
                Claims = User
            };
            var profile = await _appUserService.GetProfile(user);
            return Ok(Envelope.Ok(profile));
        }

        [HttpGet("notifications")]
        public async Task<IActionResult> GetNotifications()
        {
            var notifications =
                await _notificationRepo.Query().Where(x => x.UserId == User.GetUserId()).ToListAsync();

            return Ok(Envelope.Ok(notifications));
        }
    }
}