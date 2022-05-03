using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.CommentService.Model;
using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.Shared.Common.Exceptions;
using ProgrammingLanguages.Shared.Common.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CommentService
{
    public class CommentService:ICommentService
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IModelValidator<AddCommentModel> addCommentModelValidator;
        private readonly IModelValidator<UpdateCommentModel> updateCommentModelValidator;

        public CommentService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper,
            IModelValidator<AddCommentModel> addCommentModelValidator,
             IModelValidator<UpdateCommentModel> updateCommentModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addCommentModelValidator = addCommentModelValidator;
            this.updateCommentModelValidator = updateCommentModelValidator;
        }

        public async Task<CommentModel> AddComment(AddCommentModel model)
        {
            addCommentModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();

            var comment = mapper.Map<Comment>(model);
            await context.Comments.AddAsync(comment);
            context.SaveChanges();
          
            return mapper.Map<CommentModel>(comment);
        }

        public async Task<CommentModel> GetCommentByCommentId(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comment = await context.Comments.Include(x => x.User).FirstOrDefaultAsync(comment => comment.Id.Equals(id));

            var data = mapper.Map<CommentModel>(comment);

            return data;

        }

        public async Task<IEnumerable<CommentModel>> GetCommentsByLanguageId(int languageId)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comments = context.Comments.Include(x => x.User).AsQueryable();

            var data = (await comments.ToListAsync()).Where(comment => comment.LanguageId == languageId);
            
            return mapper.Map<IEnumerable<CommentModel>>(data); ;
        }

        public async Task UpdateComment(int id, UpdateCommentModel model)
        {
            updateCommentModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();

            var comment = await context.Comments.FirstOrDefaultAsync(x => x.Id.Equals(id));
            ProcessException.ThrowIf(() => comment is null, $"The comment (id: {id}) was not found");
            comment = mapper.Map(model, comment);

            context.Comments.Update(comment);
            context.SaveChanges();

        }

        public async Task DeleteComment(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var comment = await context.Comments.FirstOrDefaultAsync(x => x.Id.Equals(id));
            ProcessException.ThrowIf(() => comment is null, $"The comment (id: {id}) was not found");

            context.Remove(comment);
            context.SaveChanges();
        }        


    }
}
