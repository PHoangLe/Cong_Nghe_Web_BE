using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.FoodOrderDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class ServingRepository : IServingRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly ILogger<ServingRepository> _logger;

        public ServingRepository(Menu_minder_dbContext context, ILogger<ServingRepository> _logger)
        {
            this._context = context;
            this._logger = _logger;
        }

        public async Task<Serving> InsertServing(Serving data)
        {
            try
            {
                this._context.Servings.Add(data);
                await this._context.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> GetAllUnpaidServing()
        {
            try
            {
                var dataQuery = await (
                    from tbServing in _context.Servings
                    join tbTableUsed in _context.TableUseds on tbServing.ServingId equals tbTableUsed.ServingId
                    join tbDiningTable in _context.DiningTables on tbTableUsed.TableId equals tbDiningTable.TableId
                    join tbAccount in _context.Accounts on tbServing.CreatedBy equals tbAccount.AccountId
                    // left join table Bill and check serving not have bill
                    join tbBill in _context.Bills on tbServing.ServingId equals tbBill.ServingId into billGroup
                    from tbBill in billGroup.DefaultIfEmpty()
                    where tbBill == null

                    // group table by serving, account
                    group new DiningTableShortDto
                    {
                        TableId = tbDiningTable.TableId,
                        TableNumber = tbDiningTable.TableNumber,
                        Capacity = tbDiningTable.Capacity,
                        Status = tbDiningTable.Status,
                    } by new { ServingId = tbServing.ServingId, AccountId = tbAccount.AccountId } into groupedData

                    // select data
                    select new
                    {
                        // Serving information
                        Serving = this._context.Servings
                        .Where(sv => sv.ServingId == groupedData.Key.ServingId)
                        .Select(sv => new
                        {
                            ServingId = sv.ServingId,
                            TimeIn = sv.TimeIn,
                            NumberOfCutomer = sv.NumberOfCutomer
                        })
                        .FirstOrDefault(),
                        // Account Created Serving
                        CreatedBy = this._context.Accounts
                        .Where(acc => acc.AccountId == groupedData.Key.AccountId)
                        .Select(acc => new AccountShortDto
                        {
                            AccountId = acc.AccountId,
                            Avatar = acc.Avatar,
                            Email = acc.Email,
                            Name = acc.Name,
                            PhoneNumber = acc.PhoneNumber
                        }).FirstOrDefault(),
                        // List Tables
                        Tables = groupedData.ToList()
                    }
                ).ToListAsync();

                return dataQuery;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> getDetailServingById(int servingId)
        {
            try
            {
                var dataQuery = await (
                    from tbServing in _context.Servings
                    join tbTableUsed in _context.TableUseds on tbServing.ServingId equals tbTableUsed.ServingId
                    join tbDiningTable in _context.DiningTables on tbTableUsed.TableId equals tbDiningTable.TableId
                    join tbAccount in _context.Accounts on tbServing.CreatedBy equals tbAccount.AccountId
                    // left join table Bill and check serving not have bill
                    join tbBill in _context.Bills on tbServing.ServingId equals tbBill.ServingId into billGroup
                    from tbBill in billGroup.DefaultIfEmpty()
                    where tbServing.ServingId == servingId

                    // group table by serving, account
                    group new DiningTableShortDto
                    {
                        TableId = tbDiningTable.TableId,
                        TableNumber = tbDiningTable.TableNumber,
                        Capacity = tbDiningTable.Capacity,
                        Status = tbDiningTable.Status,
                    } by new { ServingId = tbServing.ServingId, AccountId = tbAccount.AccountId } into groupedData

                    // select data
                    select new
                    {
                        // Serving information
                        Serving = this._context.Servings
                        .Where(sv => sv.ServingId == groupedData.Key.ServingId)
                        .Select(sv => new
                        {
                            ServingId = sv.ServingId,
                            TimeIn = sv.TimeIn,
                            NumberOfCutomer = sv.NumberOfCutomer
                        })
                        .FirstOrDefault(),
                        // Account Created Serving
                        CreatedBy = this._context.Accounts
                        .Where(acc => acc.AccountId == groupedData.Key.AccountId)
                        .Select(acc => new AccountShortDto
                        {
                            AccountId = acc.AccountId,
                            Avatar = acc.Avatar,
                            Email = acc.Email,
                            Name = acc.Name,
                            PhoneNumber = acc.PhoneNumber
                        }).FirstOrDefault(),
                        // FoodOrder
                        FoodOrder = this._context.FoodOrders
                        .Where(foodOrder => foodOrder.ServingId == groupedData.Key.ServingId)
                        .Select(foodOrder => new FoodOrderShortWithPriceDto
                        {
                            FoodId = foodOrder.FoodId,
                            FoodOrderId = foodOrder.FoodOrderId,
                            FoodName = foodOrder.Food.Name,
                            Image = foodOrder.Food.Image,
                            Quantity = foodOrder.Quantity,
                            Price = foodOrder.Price,
                            Status = foodOrder.Status,
                            Note = foodOrder.Note,
                            CreatedAt = foodOrder.CreatedAt,
                        })
                        .ToList(),
                        // List Tables
                        Tables = groupedData.ToList()
                    }
                ).FirstOrDefaultAsync();

                return dataQuery;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<float> CalcTotalPrice(int servingId)
        {
            try
            {
                float totalPrice = await this._context.FoodOrders
                    .Where(tf => tf.ServingId == servingId)
                    .SumAsync(tf => tf.Quantity * tf.Price);
                return totalPrice;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
