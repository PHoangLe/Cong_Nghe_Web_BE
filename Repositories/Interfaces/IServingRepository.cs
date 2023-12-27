using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.ServingDTO;

namespace Repositories.Interfaces
{
    public interface IServingRepository
    {
        public Task<Serving> InsertServing(Serving servingCreate);
        public Task<object> GetAllUnpaidServing();
        public Task<object> getDetailServingById(int servingId);
        public Task<float> CalcTotalPrice(int servingId);
    }
}