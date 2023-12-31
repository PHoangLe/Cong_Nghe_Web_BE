using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.BillDTO;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;

namespace Repositories.Interfaces
{
    public interface IBillRepository
    {
        public Task InsertBill(Bill billCreate);
        public Task<List<BillResultDto>> GetAllBill();
        public List<BillDetailDto> GetBillDetails(int servingId);
    }
}
