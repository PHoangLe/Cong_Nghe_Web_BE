using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.BillDTO;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Services
{
    public class BillService
    {
        private readonly ILogger<BillService> _logger;
        private readonly IBillRepository _billRepository;
        private readonly IServingRepository _servingRepository;
        private readonly ITableUsedRepository _tableUsedRepository;
        private readonly IDiningTableRepository _diningTableRepository;

        public BillService(ILogger<BillService> logger, IBillRepository billRepository, IServingRepository servingRepository, ITableUsedRepository tableUsedRepository, IDiningTableRepository diningTableRepository)
        {
            this._logger = logger;
            this._billRepository = billRepository;
            this._servingRepository = servingRepository;
            this._tableUsedRepository = tableUsedRepository;
            _diningTableRepository = diningTableRepository;
        }

        public async Task createBill(BillCreateDto dataInvo)
        {
            try
            {
                // insert bill
                var totalPrice = await this._servingRepository.CalcTotalPrice(dataInvo.ServingId);
                Bill billCreate = new Bill
                {
                    ServingId = dataInvo.ServingId,
                    TotalPrice = (int)totalPrice,
                    CreatedBy = (Guid)dataInvo.CreatedBy,
                };
                await this._billRepository.InsertBill(billCreate);

                // update dining table status
                List<DiningTable> tables = await this._tableUsedRepository.GetTableIdsInServing(dataInvo.ServingId);
                foreach (DiningTable table in tables)
                {
                    table.Status = EnumTableStatus.PREPARING.ToString();
                }
                await this._diningTableRepository.UpdateBulk(tables);
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<BillResultDto>> GetAllBill()
        {
            List<BillResultDto> data = await _billRepository.GetAllBill();
            return data;
        }

        public List<BillDetailDto> GetBillDetails(int servingId)
        {
            return _billRepository.GetBillDetails(servingId);
        }

    }
}
