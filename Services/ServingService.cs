using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.FoodOrderDTO;
using BusinessObjects.DTO.ServingDTO;
using BusinessObjects.Enum;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Services.Exceptions;

namespace Services
{
    public class ServingService
    {
        private readonly IServingRepository _servingRepository;
        private readonly IDiningTableRepository _diningTableRepository;
        private readonly IFoodOrderRepository _foodOrderRepository;
        private readonly IFoodRepository _foodRepository;
        private readonly ITableUsedRepository _tableUsedRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServingService> _logger;


        public ServingService(IMapper mapper, ILogger<ServingService> logger, IServingRepository servingRepository, IDiningTableRepository diningTableRepository, ITableUsedRepository tableUsedRepository, IFoodOrderRepository foodOrderRepository, IFoodRepository foodRepository)
        {
            this._servingRepository = servingRepository;
            this._diningTableRepository = diningTableRepository;
            this._tableUsedRepository = tableUsedRepository;
            this._foodOrderRepository = foodOrderRepository;
            this._foodRepository = foodRepository;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<Serving> createServing(CreateServingDTO dataInvo)
        {
            try
            {
                Serving servingCreate = new Serving();
                servingCreate.CreatedBy = dataInvo.CreatedBy;
                servingCreate.NumberOfCutomer = dataInvo.NumberOfCutomer;
                servingCreate.TimeIn = DateTime.Parse(dataInvo.TimeIn.ToString());
                servingCreate.TimeOut = null;

                // Check table availble
                List<int> idTableInvos = dataInvo.DiningTableIds;
                List<DiningTable> tableExists = await this._diningTableRepository.GetListTableWithIds(idTableInvos);

                // Check table not exist
                var idTableExists = tableExists.Where(tb => tb.Status == EnumTableStatus.AVAILABLE.ToString()).Select(tb => tb.TableId).ToList();
                foreach (var id in idTableInvos)
                {
                    if (!idTableExists.Contains(id))
                    {
                        throw new BadRequestException($"DiningTable with ID = {id} is not Availble");
                    }
                }

                // create Serving
                Serving servingResult = await this._servingRepository.InsertServing(servingCreate);
                // create bulk TableUsed
                List<TableUsed> tableUsedCreate = idTableInvos.Select(tableId => new TableUsed { ServingId = servingResult.ServingId, TableId = tableId }).ToList();
                await this._tableUsedRepository.InsertBulk(tableUsedCreate);
                // update DiningTable status
                await this._diningTableRepository.UpdateBulkAvailbleByIds(idTableInvos);

                return servingResult;
            }
            catch (BadRequestException ex)
            {
                throw ex;
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
                object results = await this._servingRepository.GetAllUnpaidServing();
                return results;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task OrderFood(int servingId, List<FoodOrderAddDTO> foods)
        {
            try
            {
                // check Food Available in Menu
                List<int> foodIdInvos = foods.Select(f => f.FoodId).ToList();
                List<Food> foodExists = await this._foodRepository.FindFoodBelongIds(foodIdInvos);
                foodExists.ForEach(food =>
                {
                    if (!foodIdInvos.Contains(food.FoodId) || food.Status != EnumFoodStatus.AVAILABLE.ToString())
                    {
                        throw new BadRequestException($"Food {food.Name} with ID = {food.FoodId} is not Available");
                    }
                });

                // Add FoodOrder
                List<FoodOrder> foodOrderCreate = foods.Select(food =>
                new FoodOrder
                {
                    ServingId = servingId,
                    FoodId = food.FoodId,
                    Note = food.Note,
                    Price = food.Price,
                    Status = EnumFoodOrderStatus.PENDING.ToString(),
                    Quantity = food.Quantity,
                })
                .ToList();

                await this._foodOrderRepository.InsertBulk(foodOrderCreate);
            }
            catch (BadRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }

        public async Task<object> GetDetailServing(int servingId)
        {
            try
            {
                // get serving Detail
                var servingResult = await this._servingRepository.getDetailServingById(servingId);

                // calc Total Price
                var totalPrice = await this._servingRepository.CalcTotalPrice(servingId);

                return new { totalPrice, servingResult };
                }
            catch (BadRequestException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
