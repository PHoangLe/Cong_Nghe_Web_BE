using BusinessObjects.DataModels;
using BusinessObjects.DTO.FeedBackDTO;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class FeedBackService
    {
        private readonly IFeedBackRepository _feedBackRepository;
        private readonly ILogger<FeedBackService> _logger;

        public FeedBackService(ILogger<FeedBackService> _logger, IFeedBackRepository _feedBackRepository)
        {
            this._logger = _logger;
            this._feedBackRepository = _feedBackRepository;
        }

        public async Task<FeedBack> FindEntityById(int feedBackId)
        {
            return await _feedBackRepository.FindEntityById(feedBackId);
        }

        public async Task<ResultFeedBackDto> FindById(int feedBackId)
        {
            return await _feedBackRepository.FindyById(feedBackId);
        }

        public async Task<List<ResultFeedBackDto>> GetFeedBacksByDateRange(DateTime? fromDate, DateTime? toDate)
        {
            return await _feedBackRepository.GetFeedBacksByDateRange(fromDate, toDate);
        }

        public async Task InsertFeedBack(CreateFeedBackDto createFeedBackDto)
        {
            try
            {
                FeedBack newFeedBack = new FeedBack();

                newFeedBack.ServingId = createFeedBackDto.ServingId;
                newFeedBack.Rating = Math.Max(Math.Min(createFeedBackDto.Rating, 5), 0);
                newFeedBack.Message = createFeedBackDto.Message;
                newFeedBack.CreatedAt = DateTime.Now;
                newFeedBack.UpdatedAt = DateTime.Now;

                await _feedBackRepository.InsertFeedBack(newFeedBack);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task UpdateFeedBack(CreateFeedBackDto createFeedBackDto, int feedBackId)
        {
            try
            {
                FeedBack existFeedBack = await FindEntityById(feedBackId);

                if (existFeedBack == null)
                {
                    throw new Exception($"Cannot find feedback with id: {feedBackId}");

                }

                existFeedBack.ServingId = createFeedBackDto.ServingId;
                existFeedBack.Rating = createFeedBackDto.Rating;
                existFeedBack.Message = createFeedBackDto.Message;
                existFeedBack.CreatedAt = DateTime.Now;
                existFeedBack.UpdatedAt = DateTime.Now;

                await _feedBackRepository.UpdateFeedBack(existFeedBack);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }
    }
}
