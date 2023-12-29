using BusinessObjects.DataModels;
using BusinessObjects.DTO.FeedBackDTO;
using BusinessObjects.DTO.FoodOrderDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interfaces
{
    public interface IFeedBackRepository
    {
        public Task InsertFeedBack(FeedBack feedBack);
        public Task<List<ResultFeedBackDto>> GetFeedBacksByDateRange(DateTime? fromDate, DateTime? toDate);
        public Task UpdateFeedBack(FeedBack feedBack);
        public Task<FeedBack> FindEntityById(int feedBackId);
        public Task<ResultFeedBackDto> FindyById(int feedBackId);
    }
}
