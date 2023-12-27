using System.Security.Claims;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;
using BusinessObjects.DTO.ReservationDTO;
using BusinessObjects.DTO.AccountDTO;

namespace MenuMinderAPI.Controllers
{
    [ApiController]
    [Route("/api/reservations")]
    public class ReservationContronller : ControllerBase
    {
        private readonly ReservationService _reservationService;

        public ReservationContronller(ReservationService reservationService)
        {
            this._reservationService = reservationService;
        }

        // POST: api/reservations/create
        [HttpPost("create")]
        public async Task<ActionResult> CreateReservation(CreateReservationDto dataInvo)
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
            await this._reservationService.createReservation(dataInvo);
            response.message = "Created reservation success";

            return Ok(response);
        }

        // GET: api/reservations/all
        [HttpGet("all")]
        public async Task<ActionResult> GetAllReservation()
        {
            ApiResponse<List<Reservation>> response = new ApiResponse<List<Reservation>>();
            List<Reservation> reservationResults = await this._reservationService.getAllReservation();
            response.data = reservationResults;
            return Ok(response);
        }

        // GET: api/reservations/{reservationId}
        [HttpGet("{reservationId}")]
        public async Task<ActionResult> GetDetailReservation(int reservationId)
        {
            ApiResponse<Reservation> response = new ApiResponse<Reservation>();
            Reservation reservationResults = await this._reservationService.getAnReservation(reservationId);
            response.data = reservationResults;
            return Ok(response);
        }

        // PUT: api/reservations/update/{reservationId}
        [HttpPut("update/{reservationId}")]
        public async Task<ActionResult> UpdateReservation (int reservationId, [FromBody] UpdateReservationDto dataInvo)
        {
            ApiResponse<Reservation> response = new ApiResponse<Reservation>();
            Reservation reservationResult = await this._reservationService.updateReservation(reservationId, dataInvo);
            response.message = "Updated success";
            response.data = reservationResult;
            return Ok(response);
        }

        // DELETE: api/reservations/delete/{reservationId}
        [HttpDelete("delete/{reservationId}")]
        public async Task<ActionResult> DeleteReservation(int reservationId)
        {
            ApiResponse<NoContentResult> response = new ApiResponse<NoContentResult>();
            await this._reservationService.DeleteReservation(reservationId);
            response.message = "Deleted success";
            return Ok(response);
        }
    }
}
