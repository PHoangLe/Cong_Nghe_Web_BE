using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.CategoryDTO;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<PermissionRepository> _logger;

        public PermissionRepository(Menu_minder_dbContext context, ILogger<PermissionRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task<List<Permission>> GetAllPermissions()
        {
            List<Permission> data = new List<Permission>();
            try
            {
                data = await _context.Permissions.ToListAsync();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }

            return data;
        }
    }
}
