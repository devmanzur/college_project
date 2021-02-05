using System.Collections.Generic;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Snapkart.Domain.Dto.Request
{
    public class CreatePostDto
    {
        public string Details { get; set; }
        public string Image{ get; set; }
        public int CategoryId { get; set; }
        public int AreaId { get; set; }
        public int CityId { get; set; }
        public List<int> TagIds { get; set; }
    }

    public class CreatePostDtoValidator : AbstractValidator<CreatePostDto>
    {
        public CreatePostDtoValidator()
        {
            RuleFor(x => x.Details).NotNull().NotEmpty();
            RuleFor(x => x.CategoryId).NotNull().NotEqual(0);
            RuleFor(x => x.Image).NotNull();
            RuleFor(x => x.AreaId).NotNull().NotEqual(0);
            RuleFor(x => x.CityId).NotNull().NotEqual(0);
        }
    }
}