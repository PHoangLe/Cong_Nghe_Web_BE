using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class BillRepository : IBillRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<IBillRepository> _logger;

        public BillRepository(Menu_minder_dbContext context, ILogger<IBillRepository> logger)
        {
            _context = context;
            _logger = logger;
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
