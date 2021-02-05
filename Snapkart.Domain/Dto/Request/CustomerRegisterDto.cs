using System.Text.RegularExpressions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Snapkart.Domain.Extensions;

namespace Snapkart.Domain.Dto.Request
{
    public class CustomerRegisterDto
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string ImageUrl { get; set; }
        public string Password { get; set; }
    }

    public class CustomerRegisterDtoValidator : AbstractValidator<CustomerRegisterDto>
    {
        public CustomerRegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.ImageUrl).NotNull();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.Password).NotNull().NotEmpty().Must(x=>x.Length>=6);
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Must(x=>x.ValidPhoneNumber());
        }
    }
}