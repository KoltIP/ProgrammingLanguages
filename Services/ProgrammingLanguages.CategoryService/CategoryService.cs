using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.CategoryService.Models;
using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.Shared.Common.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        public CategoryService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
        }

        public async Task<CategoryModel> GetCategory(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = context.Categories.FirstOrDefault(x => x.Id.Equals(id));

            var data = mapper.Map<CategoryModel>(category);

            return data;
        }

        public async Task<IEnumerable<CategoryModel>> GetCategories(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = context.Categories.AsQueryable();

            category = category
                        .Skip(Math.Max(offset, 0))
                        .Take(Math.Min(limit, 1000));

            var data = (await category.ToListAsync()).Select(category => mapper.Map<CategoryModel>(category));
            return data;
        }


        public async Task<CategoryModel> AddCategory(AddCategoryModel model)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = mapper.Map<Category>(model);
            await context.Categories.AddAsync(category);
            context.SaveChanges();

            return mapper.Map<CategoryModel>(category);
        }

        public async Task UpdateCategory(int id, UpdateCategoryModel model)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id));
            ProcessException.ThrowIf(() => category is null, $"The category (id: {id}) was not found");

            category = mapper.Map(model, category);

            context.Categories.Update(category);
            context.SaveChanges();
        }

        public async Task DeleteCategory(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var category = await context.Categories.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The category (id: {id}) was not found");

            context.Remove(category);
            context.SaveChanges();
        }
    }
}
