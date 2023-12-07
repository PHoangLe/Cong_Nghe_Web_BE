using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DataModels;
using Services;
using BusinessObjects.DTO;
using System.Net;
using Repositories;
using BusinessObjects.Enum;

namespace MenuMinderAPI.Controllers
{
    [Route("api/foods")]
    [ApiController]
    public class FoodsController : ControllerBase
    {
        private readonly FoodService _service;
        private readonly ILogger<FoodsController> _logger;

        public FoodsController(FoodService service, ILogger<FoodsController> logger)
        {
            _service = service;
            _logger = logger;   
        }

        // GET: api/foods/admin
        [HttpGet("admin")]
        public async Task<ActionResult<List<ResultFoodDto>>> GetFoodsForAdmin()
        {
            ApiResponse<List<ResultFoodDto>> response = new ApiResponse<List<ResultFoodDto>>();

            try
            {
                List<ResultFoodDto> results = await this._service.GetAllFoodsForAdmin();
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

        // GET: api/foods/customer
        [HttpGet("customer")]
        public async Task<ActionResult<List<ResultFoodDto>>> GetFoodsForCustomer()
        {
            ApiResponse<List<ResultFoodDto>> response = new ApiResponse<List<ResultFoodDto>>();

            try
            {
                List<ResultFoodDto> results = await this._service.GetAllFoodsForCustomer();
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

        // GET: api/foods/group-by-category
        [HttpGet("group-by-category")]
        public async Task<ActionResult<Dictionary<string, List<ResultFoodDto>>>> GetFoodsGroupByCategory()
        {
            ApiResponse<Dictionary<string, List<ResultFoodDto>>> response = new ApiResponse<Dictionary<string, List<ResultFoodDto>>>();

            try
            {
                Dictionary<string, List<ResultFoodDto>> results = await this._service.GetAllFoodsForCustomerGroupedByCategory();
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

        // GET: api/foods/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Food>> GetFood(int id)
        {
            ApiResponse<ResultFoodDto> response = new ApiResponse<ResultFoodDto>();

            try
            {
                ResultFoodDto results = await this._service.FindFoodById(id);
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

        // PUT: api/foods/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFood([FromRoute] int id,[FromBody] CreateFoodDto dataInvo)
        {
            ApiResponse<String> response = new ApiResponse<String>();

            try
            {
                await this._service.UpdateFood(dataInvo, id);
                response.data = "update food success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // PUT: api/foods/status/{id}
        [HttpPut("status/{id}")]
        public async Task<IActionResult> UpdateFoodStatus([FromRoute] int id, [FromBody] EnumFoodStatus foodStatus)
        {
            ApiResponse<String> response = new ApiResponse<String>();

            try
            {
                await this._service.UpdateFoodStatus(id, foodStatus);
                response.data = "update food's status success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // POST: api/Foods
        [HttpPost]
        public async Task<ActionResult<Food>> CreateFood([FromBody] CreateFoodDto dataInvo)
        {
            ApiResponse<string> response = new ApiResponse<string>();

            try
            {
                await this._service.CreateFood(dataInvo);
                response.message = "created food success.";
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        // DELETE: api/Foods/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFood(int id)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                await this._service.DeleteFoodById(id);
                response.message = "delete food success.";
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
