using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProgrammingLanguages.Db.Context.Context;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.LanguageService.Models;
using ProgrammingLanguages.OperatorService.Models;
using ProgrammingLanguages.Shared.Common.Exceptions;
using ProgrammingLanguages.Shared.Common.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgrammingLanguages.OperatorService
{
    public class OperatorService : IOperatorService
    {
        private readonly IMapper mapper;
        private readonly IDbContextFactory<MainDbContext> contextFactory;
        private readonly IModelValidator<AddOperatorModel> addOperatorModelValidator;
        private readonly IModelValidator<UpdateOperatorModel> updateOperatorModelValidator;
        public OperatorService(IDbContextFactory<MainDbContext> contextFactory, IMapper mapper,
            IModelValidator<AddOperatorModel> addOperatorModelValidator,
            IModelValidator<UpdateOperatorModel> updateOperatorModelValidator)
        {
            this.contextFactory = contextFactory;
            this.mapper = mapper;
            this.addOperatorModelValidator = addOperatorModelValidator;
            this.updateOperatorModelValidator = updateOperatorModelValidator;
        }

        public async Task<OperatorModel> AddOperator(AddOperatorModel model)
        {
            addOperatorModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();

            var _operator = mapper.Map<Operator>(model);
            await context.Operators.AddAsync(_operator);
            context.SaveChanges();

            return mapper.Map<OperatorModel>(_operator);
        }

        public async Task<OperatorModel> GetOperator(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var _operator = context.Operators.Include(x => x.Language).FirstOrDefault(x => x.Id.Equals(id));

            var data = mapper.Map<OperatorModel>(_operator);

            return data;
        }

        public async Task<IEnumerable<OperatorModel>> GetOperators(int offset = 0, int limit = 10)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var operators = context.Operators.Include(x => x.Language).AsQueryable();

            operators = operators
                        .Skip(Math.Max(offset, 0))
                        .Take(Math.Min(limit, 1000));

            var data = (await operators.ToListAsync()).Select(_operator => mapper.Map<OperatorModel>(_operator));
            return data;
        }

        public async Task UpdateOperator(int id, UpdateOperatorModel model)
        {
            updateOperatorModelValidator.Check(model);
            using var context = await contextFactory.CreateDbContextAsync();

            var _operator = await context.Operators.FirstOrDefaultAsync(x => x.Id.Equals(id));
            ProcessException.ThrowIf(() => _operator is null, $"The operator (id: {id}) was not found");

            _operator = mapper.Map(model, _operator);

            context.Operators.Update(_operator);
            context.SaveChanges();
        }

        public async Task DeleteOperator(int id)
        {
            using var context = await contextFactory.CreateDbContextAsync();

            var _operator = await context.Operators.FirstOrDefaultAsync(x => x.Id.Equals(id))
                ?? throw new ProcessException($"The operator (id: {id}) was not found");

            context.Remove(_operator);
            context.SaveChanges();
        }
    }
}
