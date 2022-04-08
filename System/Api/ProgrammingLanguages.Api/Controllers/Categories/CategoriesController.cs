using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProgrammingLanguages.Api.Controllers.Categories.Models;
using ProgrammingLanguages.CategoryService;
using ProgrammingLanguages.CategoryService.Models;
using ProgrammingLanguages.Db.Entities;

namespace ProgrammingLanguages.Api.Controllers.Categories
{
    [Route("api/v{version:apiVersion}/category")]
    [ApiController]
    [ApiVersion("1.0")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILogger<CategoriesController> logger;
        private readonly ICategoryService categoryService;
        private readonly UserManager<User> userManager;
        public CategoriesController(IMapper mapper, ILogger<CategoriesController> logger, ICategoryService categoryService, UserManager<User> userManager)
        {
            this.mapper = mapper;
            this.logger = logger;
            this.categoryService = categoryService;
            this.userManager = userManager;
        }

        [HttpGet("")]
        public async Task<IEnumerable<CategoryResponse>> GetCategoriesAsync()
        {
            var languages = await categoryService.GetCategories();
            var response = mapper.Map<IEnumerable<CategoryResponse>>(languages);
            return response;
        }

        [HttpGet("{id}")]
        public async Task<CategoryResponse> GetCategoryById([FromRoute] int id)
        {
            var language = await categoryService.GetCategory(id);
            var response = mapper.Map<CategoryResponse>(language);
            return response;
        }

        [HttpPost("")]
        public async Task<CategoryResponse> AddCategoryAsync([FromBody] AddCategoryRequest request)
        {
            var model = mapper.Map<AddCategoryModel>(request);
            var language = await categoryService.AddCategory(model);
            var result = mapper.Map<CategoryResponse>(language);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategoryAsync([FromRoute] int id, [FromBody] UpdateCategoryRequest request)
        {
            var model = mapper.Map<UpdateCategoryModel>(request);
            await categoryService.UpdateCategory(id, model);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategoryAsync([FromRoute] int id)
        {

            await categoryService.DeleteCategory(id);
            return Ok();
        }
    }
}
