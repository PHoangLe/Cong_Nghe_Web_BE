using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;

namespace Repositories.Interfaces
{
    public interface IDiningTableRepository
    {
        public Task<List<ResultDiningTableDto>> GetAllDiningTables();
        public Task SaveDiningTable(DiningTable table);
        public Task<ResultDiningTableDto> FindDiningTableById(int tableId);
        public Task<DiningTable> FindDiningTableEntityById(int tableId);
        public Task UpdateDiningTable(DiningTable table);
        public Task DeleteDiningTable(DiningTable table);

    }
}
