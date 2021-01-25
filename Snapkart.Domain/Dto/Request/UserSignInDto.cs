using FluentValidation;
using Snapkart.Domain.Extensions;

namespace Snapkart.Domain.Dto.Request
{
    public class UserSignInDto
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
    }

    public class UserSignInDtoValidator : AbstractValidator<UserSignInDto>
    {
        public UserSignInDtoValidator()
        {
            RuleFor(x => x.PhoneNumber).NotNull().NotEmpty().Must(x => x.ValidPhoneNumber());
            RuleFor(x => x.Password).NotNull().NotEmpty();
        }
    }
}