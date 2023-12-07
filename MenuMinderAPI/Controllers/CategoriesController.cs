using BusinessObjects.DTO;
using BusinessObjects.DTO.CategoryDTO;
using Microsoft.AspNetCore.Mvc;
using Repositories;
using Services;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        private readonly ILogger<CategoriesController> _logger;

        public CategoriesController(CategoryService categoryService, ILogger<CategoriesController> logger)
        {
            this._categoryService = categoryService;
            this._logger = logger;
        }

        // GET: api/categories
        [HttpGet]
        public async Task<ActionResult> GetAllCategories()
        {
            ApiResponse<List<ResultCategoryDto>> response = new ApiResponse<List<ResultCategoryDto>>();

            try
            {
                List<ResultCategoryDto> results = await this._categoryService.GetAllCategories();
                response.data = results;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // GET: api/dining-tables/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult> GetCategoryById([FromRoute] int id)
        {
            ApiResponse<ResultCategoryDto> response = new ApiResponse<ResultCategoryDto>();

            try
            {
                ResultCategoryDto result = await this._categoryService.FindCategoryById(id);
                response.data = result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // POST: api/dining-tables/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateDiningTable([FromBody] CreateCategoryDto dataInvo)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                await this._categoryService.CreateCategory(dataInvo);
                response.message = "created category success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // PUT: api/dining-tables/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCategory([FromBody] CreateCategoryDto dataInvo, [FromRoute] int id)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                await this._categoryService.UpdateCategory(dataInvo, id);
                response.message = "update category success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // DELETE: api/dining-tables/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCategory([FromRoute] int id)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                await this._categoryService.DeleteCategoryById(id);
                response.message = "delete dining table success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }
    }
}
