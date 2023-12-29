using BusinessObjects.DTO;
using BusinessObjects.DTO.StatisticDTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using System.Net;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/statistic")]
    public class StatisticController : ControllerBase
    {
        private readonly StatisticService _statisticService;

        public StatisticController(StatisticService statisticService)
        {
            this._statisticService = statisticService;
        }

        [HttpGet("revenue")]
        public ActionResult GetRevenueReport([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate, [FromQuery] string reportType)
        {
            ApiResponse<List<StatisticDTO>> response = new ApiResponse<List<StatisticDTO>>();

            try
            {
                List<StatisticDTO> result = this._statisticService.GetRevenueReport(fromDate, toDate, reportType);
                response.data = result;
            }
            catch (Exception ex)
            {
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }
    }
}
