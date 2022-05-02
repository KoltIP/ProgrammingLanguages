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
    [Authorize]
    public class LanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<LanguagesController> logger;
        private readonly ILanguageService languageService;
        private readonly UserManager<User> userManager;
        public LanguagesController(IMapper mapper, ILogger<LanguagesController> logger, ILanguageService languageService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.languageService = languageService;
            this.userManager = userManager;
        }

        [RequiredScope(AppScopes.LanguageRead)]
        [HttpGet("")]
        public async Task<IEnumerable<LanguageResponse>> GetLanguagesAsync()
        {
            var languages = await languageService.GetLanguages();
            var response = mapper.Map<IEnumerable<LanguageResponse>>(languages);
            return response;
        }

        [RequiredScope(AppScopes.LanguageRead)]
        [HttpGet("{id}")]
        public async Task<LanguageResponse> GetLanguageById([FromRoute] int id)
        {
            var language = await languageService.GetLanguage(id);
            var response = mapper.Map<LanguageResponse>(language);
            return response;
        }

        [RequiredScope(AppScopes.LanguageWrite)]
        [HttpPost("")]
        public async Task<LanguageResponse> AddLanguageAsync([FromBody] AddLanguageRequest request)
        {
            var model = mapper.Map<AddLanguageModel>(request);
            var language = await languageService.AddLanguage(model);
            var result = mapper.Map<LanguageResponse>(language);
            return result;
        }

        [RequiredScope(AppScopes.LanguageWrite)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguageAsync([FromRoute] int id, [FromBody] UpdateLanguageRequest request)
        {
            var model = mapper.Map<UpdateLanguageModel>(request);
            await languageService.UpdateLanguage(id, model);
            return Ok();
        }



        [RequiredScope(AppScopes.LanguageWrite)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguageAsync([FromRoute] int id)
        {

            await languageService.DeleteLanguage(id);
            return Ok();
        }
    }
}
