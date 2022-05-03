using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using ProgrammingLanguages.Api.Controllers.Languages.Models;
using ProgrammingLanguages.Db.Entities;
using ProgrammingLanguages.LanguageService;
using ProgrammingLanguages.LanguageService.Models;
using ProgrammingLanguages.Shared.Common.Security;

namespace ProgrammingLanguages.Api.Controllers.Languages
{
    [Route("api/v{version:apiVersion}/language")]
    [ApiController]
    [ApiVersion("1.0")]
    //[Authorize]
    public class LanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<LanguagesController> logger;
        private readonly ILanguageService languageService;
        public LanguagesController(IMapper mapper, ILogger<LanguagesController> logger, ILanguageService languageService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.languageService = languageService;
        }

        [Authorize(AppScopes.LanguageRead)]
        [HttpGet("")]
        public async Task<IEnumerable<LanguageResponse>> GetLanguagesAsync([FromQuery] int offset = 0, [FromQuery] int limit = 10)
        {
            var languages = await languageService.GetLanguages(offset,limit);
            var response = mapper.Map<IEnumerable<LanguageResponse>>(languages);
            return response;
        }

        [Authorize(AppScopes.LanguageRead)]
        [HttpGet("{id}")]
        public async Task<LanguageResponse> GetLanguageById([FromRoute] int id)
        {
            var language = await languageService.GetLanguage(id);
            var response = mapper.Map<LanguageResponse>(language);
            return response;
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpPost("")]
        public async Task<LanguageResponse> AddLanguageAsync([FromBody] AddLanguageRequest request)
        {
            var model = mapper.Map<AddLanguageModel>(request);
            var language = await languageService.AddLanguage(model);
            var result = mapper.Map<LanguageResponse>(language);
            return result;
        }

        [Authorize(AppScopes.LanguageWrite)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguageAsync([FromRoute] int id, [FromBody] UpdateLanguageRequest request)
        {
            var model = mapper.Map<UpdateLanguageModel>(request);
            await languageService.UpdateLanguage(id, model);
            return Ok();
        }



        [Authorize(AppScopes.LanguageWrite)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguageAsync([FromRoute] int id)
        {

            await languageService.DeleteLanguage(id);
            return Ok();
        }


    }
}
