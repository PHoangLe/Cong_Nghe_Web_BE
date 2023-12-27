using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using System.Security.Claims;
using BusinessObjects.DTO.ServingDTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.FoodOrderDTO;
using NuGet.Packaging.Signing;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/serving")]
    public class ServingController : ControllerBase
    {
        private readonly ServingService _servingService;

        public ServingController(ServingService servingService)
        {
            this._servingService = servingService;
        }

        // POST: api/serving/create
        [HttpPost("create")]
        public async Task<ActionResult> createServing([FromBody] CreateServingDTO dataInvo)
        {
            ClaimsPrincipal claimsPrincipal = HttpContext.User;
            var userFromToken = new ResultValidateTokenDto
            {
                AccountId = HttpContext.User.FindFirstValue("AccountId"),
                Email = HttpContext.User.FindFirstValue("Email"),
                Role = HttpContext.User.FindFirstValue("Role"),
            };

            dataInvo.CreatedBy = Guid.Parse(userFromToken.AccountId);

            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await this._servingService.createServing(dataInvo);
            response.message = "Created success";

            return Ok(response);
        }

        // GET: api/serving/unpaid
        [HttpGet("unpaid")]
        public async Task<ActionResult> GetAllUnpaidServing()
        {

            ApiResponse<object> response = new ApiResponse<object>();
            object servingResults= await this._servingService.GetAllUnpaidServing();
            response.data = servingResults;

            return Ok(response);
        }

        // POST: api/serving/:servingId/food-order
        [HttpPost("{servingId}/food-order")]
        public async Task<ActionResult> OrderFood(int servingId, [FromBody] List<FoodOrderAddDTO> foods)
        {
            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await this._servingService.OrderFood(servingId, foods);
            response.message = "Order food success";
            return Ok(response);
        }

        // GET: api/serving/detail/:servingId
        [HttpGet("detail/{servingId}")]
        public async Task<ActionResult> GetDeatailServing(int servingId)
        {
            ApiResponse<object> response = new ApiResponse<object>();
            var result = await this._servingService.GetDetailServing(servingId);
            response.data = result;
            return Ok(response);
        }
    }
}
