using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.BillDTO;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<IBillRepository> _logger;
        private readonly IMapper _mapper;


        public BillRepository(Menu_minder_dbContext context, ILogger<IBillRepository> logger, IMapper mapper)
        {
            _context = context;
            _logger = logger;
            _mapper = mapper;

        }

        public async Task<List<BillResultDto>> GetAllBill()
        {
            List<BillResultDto> data = new List<BillResultDto>();
            try
            {
                data = await _context.Bills
                    .OrderByDescending(bill => bill.CreatedAt)
                    .Select(bill => _mapper.Map<BillResultDto>(bill))
                    .ToListAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task InsertBill(Bill billCreate)
        {
            try
            {
                this._context.Bills.Add(billCreate);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
