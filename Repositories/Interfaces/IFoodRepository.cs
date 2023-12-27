using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFoodRepository
    {
        public Task<List<ResultFoodDto>> GetAllFoodsForCustomer();
        public Task<List<ResultFoodDto>> GetAllFoodsForAdmin();
        public Task<Dictionary<string, List<ResultFoodDto>>> GetAllFoodsForCustomerGroupedByCategory();
        public Task SaveFood(Food food);
        public Task<ResultFoodDto> FindFoodById(int foodId);
        public Task<Food> FindFoodEntityById(int foodId);
        public Task<List<Food>> FindFoodBelongIds(List<int> Ids);
        public Task UpdateFood(Food food);
        public Task DeleteFood(Food food);
    }
}
