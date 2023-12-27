using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class TableUsedRepository : ITableUsedRepository
    {

        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<TableUsedRepository> _logger;

        public TableUsedRepository (Menu_minder_dbContext context, ILogger<TableUsedRepository> logger)
        {
            this._context = context;
            this._logger = logger;
        }

        public async Task InsertBulk(List<TableUsed> datas)
        {
            try
            {
                datas.ForEach(data =>
                {
                    this._context.TableUseds.Add(data);
                });

                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<DiningTable>> GetTableIdsInServing (int servingId)
        {
            try
            {
                List<DiningTable> tables = await this._context.TableUseds
                    .Where(tbu => tbu.ServingId == servingId)
                    .Select(tbu => tbu.Table).ToListAsync();
                return tables;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
