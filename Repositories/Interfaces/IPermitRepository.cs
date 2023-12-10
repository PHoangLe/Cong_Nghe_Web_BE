using BusinessObjects.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IPermitRepository
    {
        public Task SavePermit(Permit permit);
        public Task<List<Permit>> GetPermitsByUserId(Guid userId);
        public Task DeletePermitByUserId(Guid userId);
    }
}
