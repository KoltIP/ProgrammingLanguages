using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.LanguageService.Models;
using ProgrammingLanguages.Shared.Common.Exceptions;
using ProgrammingLanguages.Shared.Common.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.LanguageService
{
    public class LanguageService : ILanguageService
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IModelValidator<AddLanguageModel> addLanguageModelValidator;
        private readonly IModelValidator<UpdateLanguageModel> updateLanguageModelValidator;
        public LanguageService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper,
            IModelValidator<AddLanguageModel> addLanguageModelValidator,
             IModelValidator<UpdateLanguageModel> updateLanguageModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addLanguageModelValidator = addLanguageModelValidator;
            this.updateLanguageModelValidator = updateLanguageModelValidator;
        }

        public async Task<LanguageModel> AddLanguage(AddLanguageModel model)
        {
            addLanguageModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();

            var language = mapper.Map<Language>(model);
            await context.Languages.AddAsync(language);
            context.SaveChanges();

            return mapper.Map<LanguageModel>(language);
        }

        public async Task<LanguageModel> GetLanguage(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var language = context.Languages.Include(x => x.Category).FirstOrDefault(x => x.Id.Equals(id));

            var data = mapper.Map<LanguageModel>(language);

            return data;
        }

        public async Task<IEnumerable<LanguageModel>> GetLanguages(int offset = 0, int limit =10)
        {            
            using var context = await contextFactory.CreateDbContextAsync();

            var languages = context.Languages.Include(x=>x.Category).AsQueryable();

            languages = languages
                        .Skip(Math.Max(offset, 0))
                        .Take(Math.Max(0, Math.Min(limit, 1000)));

            var data = (await languages.ToListAsync()).Select(language => mapper.Map<LanguageModel>(language));
            return data;
        }

        public async Task UpdateLanguage(int id, UpdateLanguageModel model)
        {
            updateLanguageModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();

            var language = await context.Languages.FirstOrDefaultAsync(x => x.Id.Equals(id));
            ProcessException.ThrowIf(() => language is null, $"The language (id: {id}) was not found");

            language = mapper.Map(model, language);

            context.Languages.Update(language);
            context.SaveChanges();
        }

        public async Task DeleteLanguage(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var language = await context.Languages.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The language (id: {id}) was not found");

            context.Remove(language);
            context.SaveChanges();
        }
    }
}
