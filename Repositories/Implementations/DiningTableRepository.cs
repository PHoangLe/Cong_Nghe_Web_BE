/*using BusinessObjects.DataModels;*/
using System.Net.NetworkInformation;
using System;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Repositories.Interfaces;
using AutoMapper;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Repositories
{
    public class DiningTableRepository : IDiningTableRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<DiningTableRepository> _logger;

        public DiningTableRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<DiningTableRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task DeleteDiningTable(DiningTable table)
        {
            try
            {
                table.Status = EnumTableStatus.DELETED.ToString();
                await UpdateDiningTable(table);
            }
            catch(Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<ResultDiningTableDto> FindDiningTableById(int tableId)
        {
            try
            {
                DiningTable diningTable = await FindDiningTableEntityById(tableId);

                if (diningTable != null)
                {
                    ResultDiningTableDto data = _mapper.Map<ResultDiningTableDto>(diningTable);
                    return data;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<DiningTable> FindDiningTableEntityById(int tableId)
        {
            try
            {
                return await _context.DiningTables.FirstOrDefaultAsync(dt => dt.TableId == tableId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<List<ResultDiningTableDto>> GetAllDiningTables()
        {

            List <ResultDiningTableDto> data  = new List<ResultDiningTableDto>();
            try
            { 
                data = await _context.DiningTables
                    .Where(diningTable => diningTable.Status != EnumTableStatus.DELETED.ToString())
                    .Select(diningTable => _mapper.Map<ResultDiningTableDto>(diningTable)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task SaveDiningTable(DiningTable table)
        {
            try
            {
                this._context.DiningTables.Add(table);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task UpdateDiningTable(DiningTable table)
        {
            try
            {
                this._context.DiningTables.Update(table);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }
    }
}
