using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.CategoryDTO;
using BusinessObjects.DTO.FeedBackDTO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementations
{
    public class FeedBackRepository : IFeedBackRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<FeedBackRepository> _logger;

        public FeedBackRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<FeedBackRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<FeedBack> FindEntityById(int feedBackId)
        {
            try
            {
                return await _context.FeedBacks.FirstOrDefaultAsync(feedBack => feedBack.FeedBackId == feedBackId);
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task<ResultFeedBackDto> FindyById(int feedBackId)
        {
            try
            {
                FeedBack feedBack = await FindEntityById(feedBackId);

                if (feedBack != null)
                {
                    ResultFeedBackDto data = _mapper.Map<ResultFeedBackDto>(feedBack);
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

        public async Task<List<ResultFeedBackDto>> GetFeedBacksByDateRange(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                List<ResultFeedBackDto> data = null;

                if (!fromDate.HasValue || !toDate.HasValue)
                {
                    data = await this._context.FeedBacks.OrderByDescending(r => r.CreatedAt)
                        .Select(food => _mapper.Map<ResultFeedBackDto>(food))
                        .ToListAsync();

                    return data;
                }

                data = await this._context.FeedBacks
                    .Where(f => f.CreatedAt >= fromDate && f.CreatedAt <= toDate) 
                    .OrderByDescending(r => r.CreatedAt)
                    .Select(food => _mapper.Map<ResultFeedBackDto>(food))
                    .ToListAsync();
                return data;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public async Task InsertFeedBack(FeedBack feedBack)
        {
            try
            {
                this._context.FeedBacks.Add(feedBack);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
        }

        public async Task UpdateFeedBack(FeedBack feedBack)
        {
            try
            {
                this._context.FeedBacks.Update(feedBack);
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
