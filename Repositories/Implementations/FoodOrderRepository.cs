using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.FoodOrderDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class FoodOrderRepository : IFoodOrderRepository
    {

        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodOrderRepository> _logger;

        public FoodOrderRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<FoodOrderRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task InsertBulk(List<FoodOrder> datas)
        {
            try
            {
                datas.ForEach(data =>
                {
                    this._context.FoodOrders.Add(data);
                });
                await this._context.SaveChangesAsync(); 
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<List<FoodOrderShortDto>> FindFoodOrderWithStatus(string status)
        {
            try
            {
                List<FoodOrderShortDto> foodResults = new List<FoodOrderShortDto>();
                if (status == "ALL") {
                    foodResults =  await this._context.FoodOrders
                        .Select(foodOrder => new FoodOrderShortDto
                        {
                            FoodOrderId = foodOrder.FoodOrderId,
                            FoodName = foodOrder.Food.Name,
                            FoodId = foodOrder.FoodId,
                            ServingId = foodOrder.ServingId,
                            Quantity = foodOrder.Quantity,
                            Status = foodOrder.Status,
                            Note = foodOrder.Note,
                            CreatedAt = foodOrder.CreatedAt
                        })
                        .OrderBy(f => f.CreatedAt)
                        .ToListAsync();
                }
                else
                {
                    foodResults = await this._context.FoodOrders
                        .Where(foodOrder => foodOrder.Status == status)
                        .Select(foodOrder => new FoodOrderShortDto
                        {
                            FoodOrderId = foodOrder.FoodOrderId,
                            FoodName = foodOrder.Food.Name,
                            FoodId = foodOrder.FoodId,
                            ServingId = foodOrder.ServingId,
                            Quantity = foodOrder.Quantity,
                            Status = foodOrder.Status,
                            Note = foodOrder.Note,
                            CreatedAt = foodOrder.CreatedAt
                        })
                        .OrderByDescending(f => f.CreatedAt)
                        .ToListAsync();
                }
                return foodResults;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task UpdateFoodOrder(FoodOrder foodOrder)
        {
            try
            {
                this._context.FoodOrders.Update(foodOrder);
                await this._context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw;
            }
        }

        public async Task<FoodOrder> FindById(int foodOrderId)
        {
            try
            {
                FoodOrder result = await this._context.FoodOrders.Where(foodOrder => foodOrder.FoodOrderId == foodOrderId).FirstOrDefaultAsync();
                return result;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw;
            }
        }
    }
}
