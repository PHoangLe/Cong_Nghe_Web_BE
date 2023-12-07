using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.Enum;
using BusinessObjects.Mapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class FoodRepository : IFoodRepository
    {

        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FoodRepository> _logger;

        public FoodRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<FoodRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task DeleteFood(Food food)
        {
            try
            {
                food.Status = EnumFoodStatus.DELETED.ToString();
                await UpdateFood(food);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<ResultFoodDto> FindFoodById(int foodId)
        {
            try
            {
                Food food = await FindFoodEntityById(foodId);

                if (food != null)
                {
                    ResultFoodDto data = _mapper.Map<ResultFoodDto>(food);
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

        public async Task<Food> FindFoodEntityById(int foodId)
        {
            try
            {
                return await _context.Foods.FirstOrDefaultAsync(food => food.FoodId == foodId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<List<ResultFoodDto>> GetAllFoodsForAdmin()
        {
            List<ResultFoodDto> data = new List<ResultFoodDto>();
            try
            {
                data = await _context.Foods
                    .Where(food => food.Status != EnumFoodStatus.DELETED.ToString())
                    .Select(food => _mapper.Map<ResultFoodDto>(food)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task<List<ResultFoodDto>> GetAllFoodsForCustomer()
        {
            List<ResultFoodDto> data = new List<ResultFoodDto>();
            try
            {
                data = await _context.Foods
                    .Where(food => food.Status == EnumFoodStatus.AVAILABLE.ToString())
                    .Select(food => _mapper.Map<ResultFoodDto>(food)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task<Dictionary<string, List<ResultFoodDto>>> GetAllFoodsForCustomerGroupedByCategory()
        {
            try
            {
                var categories = await _context.Categories.ToListAsync();

                var foods = await _context.Foods
                    .Where(food => food.Status == EnumFoodStatus.AVAILABLE.ToString())
                    .ToListAsync();

                var groupedData = categories
                    .GroupJoin(
                        foods,
                        category => category.CategoryId,
                        food => food.CategoryId,
                        (category, categoryFoods) => new
                        {
                            CategoryName = category.CategoryName,
                            Foods = categoryFoods.Select(food => _mapper.Map<ResultFoodDto>(food)).ToList()
                        })
                    .ToDictionary(group => group.CategoryName, group => group.Foods);

                return groupedData;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task SaveFood(Food food)
        {
            try
            {
                this._context.Foods.Add(food);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task UpdateFood(Food food)
        {
            try
            {
                this._context.Foods.Update(food);
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
