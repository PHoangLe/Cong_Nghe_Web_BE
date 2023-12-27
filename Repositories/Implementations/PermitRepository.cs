using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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

        public async Task createBulkPermits(Permit[] permits)
        {
            if (permits.Length > 0)
            {
                foreach (Permit permit in permits)
                {
                    this._context.Permits.Add(permit);
                }
                await this._context.SaveChangesAsync();
            }
        }

        public async Task<List<int>> FindPermissions(string accountID)
        {
            var data = await this._context.Permits.Where(p => p.AccountId.ToString() == accountID)
                .Select(p => new
                {
                    p.Permission.PermissionId
                }).ToListAsync();

            List<int> permissionIds = data.Select(permission =>  permission.PermissionId).ToList();
            return permissionIds;
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
        public async Task deleteManyPermits(string accountId, List<int> permissionId)
        {
            try
            {
                var permissionDeletes = await this._context.Permits.Where(p =>
                p.AccountId == Guid.Parse(accountId)
                && permissionId.Contains(p.PermissionId)
                ).ToListAsync();

                this._context.RemoveRange(permissionDeletes);
                await this._context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<List<int>> getPermitIdsOfAccount(string accountId)
        {
            try
            {
                List<int> permitIds = await this._context.Permits.Where(p => p.AccountId.ToString() == accountId)
                    .Select(p => p.Permission.PermissionId)
                    .ToListAsync();
                return permitIds;
            }
            catch (Exception e)
            {
                this._logger.LogError(e.Message);
                throw;
            }
        }
    }
}
