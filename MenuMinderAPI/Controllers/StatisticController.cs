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
            ApiResponse<List<RevenueStatisticDTO>> response = new ApiResponse<List<RevenueStatisticDTO>>();

            try
            {
                List<RevenueStatisticDTO> result = this._statisticService.GetRevenueReport(fromDate, toDate, reportType);
                response.data = result;
            }
            catch (Exception ex)
            {
                response.errorMessage = ex.Message;
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }

        [HttpGet("general")]
        public ActionResult GetRevenueReport([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            ApiResponse<GeneralStatisticDTO> response = new ApiResponse<GeneralStatisticDTO>();

            try
            {
                GeneralStatisticDTO result = this._statisticService.GetGeneralReport(fromDate, toDate);
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
