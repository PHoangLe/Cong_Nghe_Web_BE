using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessObjects.DataModels;
using Repositories;
using Services;
using BusinessObjects.DTO;
using System.Net;
using BusinessObjects.DTO.AccountDTO;

namespace MenuMinderAPI.Controllers
{
    [Route("api/permissions")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly PermissionService _permissionService;
        private readonly ILogger<PermissionsController> _logger;

        public PermissionsController(PermissionService permissionService, ILogger<PermissionsController> logger)
        {
            this._permissionService = permissionService;
            this._logger = logger;
        }

        // GET: api/Permissions
        [HttpGet]
        public async Task<ActionResult> GetPermissions()
        {

            ApiResponse<List<Permission>> response = new ApiResponse<List<Permission>>();

            try
            {
                List<Permission> results = await this._permissionService.GetAllPermissions();
                response.data = results;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                response.statusCode = (int)HttpStatusCode.BadRequest;
            }

            return Ok(response);
        }
    }
}
