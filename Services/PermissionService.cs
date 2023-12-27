using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using Repositories;
using Repositories.Implementations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class PermissionService
    {
        private readonly PermissionRepository _permissionRepository;

        public PermissionService(PermissionRepository permissionRepository)
        {
            this._permissionRepository = permissionRepository;
        }

        public async Task<List<Permission>> GetAllPermissions()
        {
            return await _permissionRepository.GetAllPermissions();
        }
        
        public async Task<List<PermissonDto>> GetPermissionsByUserId(Guid userId)
        {
            return await _permissionRepository.GetPermissionsByUserId(userId);
        }
    }
}
