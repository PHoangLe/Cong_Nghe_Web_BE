using System.Security.Claims;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO.FoodOrderDTO;
using BusinessObjects.DTO.ServingDTO;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/food-order")]
    public class FoodOrderController : ControllerBase
    {
        private readonly FoodOrderService _foodOrderService;
        public FoodOrderController(FoodOrderService foodOrderService)
        {
            this._foodOrderService = foodOrderService;
        }

        [HttpGet]
        public async Task<ActionResult> GetFoodOrders([FromQuery] string? status)
        {
            ApiResponse<List<FoodOrderShortDto>> response = new ApiResponse<List<FoodOrderShortDto>>();
            List<FoodOrderShortDto> foods = await this._foodOrderService.GetFoodOrder(status);
            response.data = foods;
            return Ok(response);
        }

        [HttpPut("update-status/{foodOrderId}")]
        public async Task<ActionResult> UpdateFoodOrderStatus ([FromQuery] string? status, int foodOrderId)
        {
            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await this._foodOrderService.UpdateFoodOrder(foodOrderId, status);
            response.message = "Updated status success";
            return Ok(response);
        }
    }
}
