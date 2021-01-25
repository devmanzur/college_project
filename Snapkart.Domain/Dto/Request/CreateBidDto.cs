using System.Data;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Snapkart.Domain.Dto.Request
{
    public class CreateBidDto
    {
        public IFormFile Image { get; set; }
        public decimal Price { get; set; }
        public string Details { get; set; }
    }

    public class CreateBidDtoValidator : AbstractValidator<CreateBidDto>
    {
        public CreateBidDtoValidator()
        {
            RuleFor(x => x.Image).NotNull();
            RuleFor(x => x.Details).NotNull();
            RuleFor(x => x.Price).NotNull().Must(x => x > 0);
        }
    }
}