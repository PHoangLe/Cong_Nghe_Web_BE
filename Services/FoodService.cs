using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.Enum;
using Repositories;
using Repositories.Implementations;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FoodService
    {

        private readonly FoodRepository _foodRepository;

        public FoodService(FoodRepository foodRepository)
        {
            this._foodRepository = foodRepository;
        }

        public async Task<List<ResultFoodDto>> GetAllFoodsForCustomer()
        {
            List<ResultFoodDto> foodResults = await this._foodRepository.GetAllFoodsForCustomer();
            return foodResults;
        }

        public async Task<List<ResultFoodDto>> GetAllFoodsForAdmin()
        {
            List<ResultFoodDto> foodResults = await this._foodRepository.GetAllFoodsForAdmin();
            return foodResults;
        }

        public async Task<Dictionary<string, List<ResultFoodDto>>> GetAllFoodsForCustomerGroupedByCategory()
        {
            Dictionary<string, List<ResultFoodDto>> foodResults = await this._foodRepository.GetAllFoodsForCustomerGroupedByCategory();
            return foodResults;
        }

        public async Task<Food> FindFoodEntityById(int foodId)
        {
            return await _foodRepository.FindFoodEntityById(foodId);
        }

        public async Task<ResultFoodDto> FindFoodById(int foodId)
        {
            return await _foodRepository.FindFoodById(foodId);
        }

        public async Task CreateFood(CreateFoodDto dataInvo)
        {
            Food FoodCreate = new Food();
            // create data
            FoodCreate.Price = dataInvo.Price;
            FoodCreate.Status = EnumFoodStatus.PENDING.ToString();
            FoodCreate.Name = dataInvo.Name;
            FoodCreate.CategoryId = dataInvo.CategoryId;
            FoodCreate.Image = dataInvo.Image;
            FoodCreate.Recipe = dataInvo.Recipe;
            // save data
            await _foodRepository.SaveFood(FoodCreate);
        }

        public async Task UpdateFood(CreateFoodDto dataInvo, int foodId)
        {
            Food FoodUpdate = await FindFoodEntityById(foodId);

            if (FoodUpdate == null)
            {
                throw new Exception($"Cannot find food with id: {foodId}");

            }

            // set data
            FoodUpdate.Price = dataInvo.Price;
            FoodUpdate.Name = dataInvo.Name;
            FoodUpdate.CategoryId = dataInvo.CategoryId;
            FoodUpdate.Image = dataInvo.Image;
            FoodUpdate.Recipe = dataInvo.Recipe;
            FoodUpdate.Status = dataInvo.Status.ToString();
            // update data
            await _foodRepository.UpdateFood(FoodUpdate);
        }

        public async Task UpdateFoodStatus(int foodId, EnumFoodStatus foodStatus)
        {
            Food FoodUpdate = await FindFoodEntityById(foodId);

            if (FoodUpdate == null)
            {
                throw new Exception($"Cannot find food with id: {foodId}");

            }

            // set data
            FoodUpdate.Status = foodStatus.ToString();
            // update data
            await _foodRepository.UpdateFood(FoodUpdate);
        }

        public async Task DeleteFoodById(int foodId)
        {
            Food Food = await FindFoodEntityById(foodId);

            if (Food == null)
            {
                throw new Exception($"Cannot find food with id: {foodId}");
            }

            await _foodRepository.DeleteFood(Food);
        }
    }
}
