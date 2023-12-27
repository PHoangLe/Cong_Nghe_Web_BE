using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO.CategoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPermissionRepository
    {
        public Task<List<Permission>> GetAllPermissions();
        public Task<List<PermissonDto>> GetPermissionsByUserId(Guid userId);
    }
}
