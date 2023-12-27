using BusinessObjects.DataModels;
using BusinessObjects.DTO.PermitDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPermitRepository
    {
        public Task createBulkPermits(Permit[] permits);
        public Task SavePermit(Permit permit);
        public Task<List<Permit>> GetPermitsByUserId(Guid userId);
        public Task DeletePermitByUserId(Guid userId);
        public Task<List<int>> FindPermissions(string accountID);
        public Task deleteManyPermits(string accountId, List<int> permissionId);
        public Task<List<int>> getPermitIdsOfAccount(string accountId);
    }
}
