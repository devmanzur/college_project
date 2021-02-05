using System.Collections.Generic;
using System.Text.RegularExpressions;
using FluentValidation;
using Snapkart.Domain.Extensions;

namespace Snapkart.Domain.Dto.Request
{
    public class MerchantRegisterDto : CustomerRegisterDto
    {
        public List<int> SubscriptionIds { get; set; }
    }

    public class MerchantRegisterDtoValidator : AbstractValidator<MerchantRegisterDto>
    {
        public MerchantRegisterDtoValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.SubscriptionIds).NotNull().NotEmpty().Must(x => x.Count > 0);
            RuleFor(x => x.ImageUrl).NotNull();
            RuleFor(x => x.Address).NotNull().NotEmpty();
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Must(x => x.ValidPhoneNumber());
            RuleFor(x => x.Password).NotNull().NotEmpty().Must(x => x.Length >= 6);
        }
    }
}