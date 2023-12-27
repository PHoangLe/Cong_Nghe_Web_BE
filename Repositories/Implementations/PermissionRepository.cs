using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
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
        private readonly IMapper _mapper;

        public PermissionRepository(Menu_minder_dbContext context, ILogger<PermissionRepository> logger, IMapper mapper)
        {
            this._context = context;
            this._logger = logger;
            this._mapper = mapper;
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

        public async Task<List<PermissonDto>> GetPermissionsByUserId(Guid userId)
        {
            List<PermissonDto> data = new List<PermissonDto>();
            try
            {
                data = await _context.Permits
                    .Where(p => p.AccountId == userId)
                    .Join(
                        _context.Permissions,
                        permit => permit.PermissionId,
                        permission => permission.PermissionId,
                        (permit, permission) => permission
                    ).Select(permission => this._mapper.Map<PermissonDto>(permission))
                    .ToListAsync();
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
