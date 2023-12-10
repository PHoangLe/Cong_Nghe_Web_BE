using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.Enum;
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
    public class PermitRepository : IPermitRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<PermitRepository> _logger;

        public PermitRepository(Menu_minder_dbContext context, IMapper mapper, ILogger<PermitRepository> logger)
        {
            this._context = context;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task DeletePermitByUserId(Guid userId)
        {
            List<Permit> permits = await GetPermitsByUserId(userId);
            _context.Permits.RemoveRange(permits);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Permit>> GetPermitsByUserId(Guid userId)
        {
            List<Permit> data = new List<Permit>();
            try
            {
                data = await _context.Permits
                .Where(permit => permit.AccountId == userId)
                .ToListAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                throw;
            }
            return data;

        }

        public async Task SavePermit(Permit permit)
        {
            try
            {
                this._context.Permits.Add(permit);
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
