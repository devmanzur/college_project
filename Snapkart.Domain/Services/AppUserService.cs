using System;
using System.Linq;
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

        public async Task<Result<AppUserDto>> RegisterCustomer(CustomerRegisterDto dto)
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
                    : Result.Failure<AppUserDto>(registration.Errors.FirstOrDefault()?.Description);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Result.Failure<AppUserDto>("failed to register user");
            }
        }

        public async Task<Result<AppUserDto>> RegisterMerchant(MerchantRegisterDto dto)
        {
            try
            {
                var imageUpload = await _imageServer.UploadImage(dto.Image);
                if (imageUpload.IsFailure)
                {
                    return Result.Failure<AppUserDto>("failed to upload image");
                }

                var merchant = new AppUser(UserRole.Merchant, dto.Name, dto.PhoneNumber, dto.Address,
                    imageUpload.Value);
                merchant.AddSubscriptions(dto.SubscriptionIds);

                var registration =
                    await _userManager.CreateAsync(merchant, dto.Password);

                return registration.Succeeded
                    ? Result.Success(new AppUserDto(merchant))
                    : Result.Failure<AppUserDto>(registration.Errors.FirstOrDefault()?.Description);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return Result.Failure<AppUserDto>("failed to register merchant");
            }
        }

        public async Task<Result<AppUser>> SignIn(UserSignInDto dto)
        {
            var user = await _userManager.FindByNameAsync(dto.PhoneNumber);
            if (user == null)
            {
                return Result.Failure<AppUser>("user not found");
            }

            var isValidPassword = await _userManager.CheckPasswordAsync(user, dto.Password);
            if (isValidPassword)
            {
                return Result.Success(user);
            }

            return Result.Failure<AppUser>("invalid user/password combination");
        }
    }
}