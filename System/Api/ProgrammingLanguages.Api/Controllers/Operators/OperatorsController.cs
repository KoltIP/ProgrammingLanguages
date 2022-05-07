using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ProgrammingLanguages.Api.Controllers.Operators.Models;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.LanguageService.Models;
using ProgrammingLanguages.OperatorService;
using ProgrammingLanguages.OperatorService.Models;
using ProgrammingLanguages.Shared.Common.Security;

namespace ProgrammingLanguages.Api.Controllers.Operators
{
    [Route("api/v{version:apiVersion}/operator")]
    [ApiController]
    [ApiVersion("1.0")]
    [Authorize]
    public class OperatorsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<OperatorsController> logger;
        private readonly IOperatorService operatorService;
        public OperatorsController(IMapper mapper, ILogger<OperatorsController> logger, IOperatorService operatorService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.operatorService = operatorService;
        }

        [Authorize(AppScopes.LanguageRead)]
        [HttpGet("")]
        public async Task<IEnumerable<OperatorResponse>> GetOperatorsAsync()
        {
            var operators = await operatorService.GetOperators();
            var response = mapper.Map<IEnumerable<OperatorResponse>>(operators);
            return response;
        }

        [Authorize(AppScopes.LanguageRead)]
        [HttpGet("{id}")]
        public async Task<OperatorResponse> GetOperatorById([FromRoute] int id)
        {
            var _operator = await operatorService.GetOperator(id);
            var response = mapper.Map<OperatorResponse>(_operator);
            return response;
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpPost("")]
        public async Task<OperatorResponse> AddOperatorAsync([FromBody] AddOperatorRequest request)
        {
            var model = mapper.Map<AddOperatorModel>(request);
            var _operator = await operatorService.AddOperator(model);
            var result = mapper.Map<OperatorResponse>(_operator);
            return result;
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOperatorAsync([FromRoute] int id, [FromBody] UpdateOperatorRequest request)
        {
            var model = mapper.Map<UpdateOperatorModel>(request);
            await operatorService.UpdateOperator(id, model);
            return Ok();
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOperatorAsync([FromRoute] int id)
        {

            await operatorService.DeleteOperator(id);
            return Ok();
        }
    }
}
