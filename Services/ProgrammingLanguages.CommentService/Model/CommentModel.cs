using AutoMapper;
using ProgrammingLanguages.Db.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CommentService.Model
{
    public class CommentModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public int LanguageId { get; set; }
    }
    public class CommentModelProfile : Profile
    {
        public CommentModelProfile()
        {
            CreateMap<Comment, CommentModel>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName));
        }
    }

}
