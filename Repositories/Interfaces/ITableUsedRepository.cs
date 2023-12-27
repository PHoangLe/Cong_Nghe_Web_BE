using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;

namespace Repositories.Interfaces
{
    public interface ITableUsedRepository
    {
        public Task InsertBulk(List<TableUsed> datas);
        public Task<List<DiningTable>> GetTableIdsInServing(int servingId);
    }
}
