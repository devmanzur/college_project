using System;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using Snapkart.Domain.Constants;
using Snapkart.Domain.Dto.Request;
using Snapkart.Domain.Dto.Response;
using Snapkart.Domain.Entities;
using Snapkart.Domain.Interfaces;

namespace Snapkart.Domain.Services
{
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IImageServerBroker _imageServer;

        public AppUserService(UserManager<AppUser> userManager, IImageServerBroker imageServer)
        {
            _userManager = userManager;
            _imageServer = imageServer;
        }

        public async Task<Result<AppUserDto>> RegisterCustomer(AccountRegisterDto dto)
        {
            try
            {
                var imageUpload = await _imageServer.UploadImage(dto.Image);
                if (imageUpload.IsFailure)
                {
                    return Result.Failure<AppUserDto>("failed to upload image");
                }


                var user = new AppUser(UserRole.Customer, dto.Name, dto.PhoneNumber, dto.Address, imageUpload.Value);
                var registration =
                    await _userManager.CreateAsync(user, dto.Password);

                return registration.Succeeded
                    ? Result.Success(new AppUserDto(user))
                    : Result.Failure<AppUserDto>("failed to register user");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Result.Failure<AppUserDto>("failed to register user");
            }
        }
    }
}