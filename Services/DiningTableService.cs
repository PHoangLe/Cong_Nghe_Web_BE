using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using Repositories;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using BusinessObjects.Enum;

namespace Services
{
    public class DiningTableService
    {
        private readonly DiningTableRepository _diningTableRepository;

        public DiningTableService(DiningTableRepository diningTableRepository)
        {
            this._diningTableRepository = diningTableRepository;
        }

        public async Task<List<ResultDiningTableDto>> GetAllDiningTables()
        {
            List<ResultDiningTableDto> diningTableResults = await this._diningTableRepository.GetAllDiningTables();
            return diningTableResults;
        }

        public async Task<DiningTable> FindDiningTableEntityById(int tableId)
        {
            return await _diningTableRepository.FindDiningTableEntityById(tableId);
        }

        public async Task<ResultDiningTableDto> FindDiningTableById(int tableId)
        {
            return await _diningTableRepository.FindDiningTableById(tableId);
        }

        public async Task CreateDiningTable(CreateDiningTableDto dataInvo)
        {
            DiningTable diningTableCreate = new DiningTable();
            // create data
            diningTableCreate.TableNumber = dataInvo.TableNumber;
            diningTableCreate.Capacity = dataInvo.Capacity;
            diningTableCreate.CreatedBy = dataInvo.CreatedBy;
            diningTableCreate.Status = EnumTableStatus.AVAILABLE.ToString();
            // save data
            await _diningTableRepository.SaveDiningTable(diningTableCreate);
        }

        public async Task UpdateDiningTable(UpdateDiningTableDto dataInvo, int tableId)
        {
            DiningTable diningTableUpdate = await FindDiningTableEntityById(tableId);

            if (diningTableUpdate == null)
            {
                throw new Exception($"Cannot find table with id: {tableId}");

            }

            // set data
            diningTableUpdate.TableNumber = dataInvo.TableNumber;
            diningTableUpdate.Capacity = dataInvo.Capacity;
            diningTableUpdate.UpdatedAt = DateTime.Now;
            diningTableUpdate.Status = dataInvo.Status;
            // update data
            await _diningTableRepository.UpdateDiningTable(diningTableUpdate);
        }

        public async Task DeleteDiningTableById(int tableId)
        {
            DiningTable diningTable = await FindDiningTableEntityById(tableId);

            if (diningTable == null)
            {
                throw new Exception($"Cannot find table with id: {tableId}");
            }

            await _diningTableRepository.DeleteDiningTable(diningTable);
        }
    }
}