using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProgrammingLanguages.Api.Controllers.Languages.Models;
using ProgrammingLanguages.LanguageService;
using ProgrammingLanguages.LanguageService.Models;

namespace ProgrammingLanguages.Api.Controllers.Languages
{
    [Route("api/v{version:apiVersion}/language")]
    [ApiController]
    [ApiVersion("1.0")]
    public class LanguagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<LanguagesController> logger;
        private readonly ILanguageService languageService;
        public LanguagesController(IMapper mapper, ILogger<LanguagesController> logger, ILanguageService languageService)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.languageService = languageService;
        }

        [HttpGet("")]
        public async Task<IEnumerable<LanguageResponse>> GetLanguagesAsync()
        {
            var languages = await languageService.GetLanguages();
            var response = mapper.Map<IEnumerable<LanguageResponse>>(languages);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<LanguageResponse> GetLanguageById([FromRoute] int id)
        {
            var language = await languageService.GetLanguage(id);
            var response = mapper.Map<LanguageResponse>(language);
            return response;
        }


        [HttpPost("")]
        public async Task<LanguageResponse> AddLanguageAsync([FromBody] AddLanguageRequest request)
        {
            var model = mapper.Map<AddLanguageModel>(request);
            var language = await languageService.AddLanguage(model);

            var result = mapper.Map<LanguageResponse>(language);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateLanguageAsync([FromRoute] int id, [FromBody] UpdateLanguageRequest request)
        {
            var model = mapper.Map<UpdateLanguageModel>(request);
            await languageService.UpdateLanguage(id, model);
            return Ok();
        }
    }
}
