using AutoMapper;
using FluentValidation;
using ProgrammingLanguages.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.LanguageService.Model
{
    public class AddSubscribeModel
    {
        public Guid UserId { get; set; }
        public int LanguageId { get; set; }
    }
    public class AddSubscribeModelValidator : AbstractValidator<AddSubscribeModel>
    {
        public AddSubscribeModelValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty().WithMessage("User is required");
            RuleFor(x => x.LanguageId)
                .NotEmpty().WithMessage("Programming language is required");
        }
    }
    public class AddSubscribeModelProfile : Profile
    {
        public AddSubscribeModelProfile()
        {
            CreateMap<AddSubscribeModel, Subscription>();
        }
    }
}
