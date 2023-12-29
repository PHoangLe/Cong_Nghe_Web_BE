using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Services;
using BusinessObjects.DTO.FeedBackDTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MenuMinderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedBackController : ControllerBase
    {
        private readonly FeedBackService _feedBackService;

        public FeedBackController(FeedBackService feedBackService)
        {
            this._feedBackService = feedBackService;
        }

        // GET: api/<FeedBackController>
        [HttpGet]
        public async Task<ActionResult> Get([FromQuery] DateTime? fromDate, [FromQuery] DateTime? toDate)
        {
            ApiResponse<List<ResultFeedBackDto>> response = new ApiResponse<List<ResultFeedBackDto>>();
            List<ResultFeedBackDto> result = await this._feedBackService.GetFeedBacksByDateRange(fromDate, toDate);
            response.data = result;
            response.message = "Success";

            return Ok(response);
        }

        // GET api/<FeedBackController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            ApiResponse<ResultFeedBackDto> response = new ApiResponse<ResultFeedBackDto>();
            ResultFeedBackDto result = await this._feedBackService.FindById(id);
            response.data = result;
            response.message = "Success";

            if (result == null)
            {
                response.message = "Data empty";
            }

            return Ok(response);
        }

        // POST api/<FeedBackController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateFeedBackDto dto)
        {
            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await _feedBackService.InsertFeedBack(dto);
            response.message = "Created success";

            return Ok(response);
        }

        // PUT api/<FeedBackController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, [FromBody] CreateFeedBackDto dto)
        {
            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await _feedBackService.UpdateFeedBack(dto, id);
            response.message = "Created success";

            return Ok(response);
        }
    }
}
