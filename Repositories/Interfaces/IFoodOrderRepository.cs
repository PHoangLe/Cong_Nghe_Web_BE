using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.FoodOrderDTO;

namespace Repositories.Interfaces
{
    public interface IFoodOrderRepository
    {
        public Task InsertBulk(List<FoodOrder> datas);
        public Task <List<FoodOrderShortDto>> FindFoodOrderWithStatus(string status);
        public Task UpdateFoodOrder(FoodOrder foodOrder);
        public Task<FoodOrder> FindById(int foodOrderId);
    }
}
