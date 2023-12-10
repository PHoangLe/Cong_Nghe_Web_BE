using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.CategoryDTO;
using BusinessObjects.Enum;
using Microsoft.EntityFrameworkCore;
using Repositories.Interfaces;

namespace Repositories.Implementations
{
    public class AccountRepository : IAccountRepository
    {
        private readonly Menu_minder_dbContext _context;
        private readonly IMapper _mapper;

        public AccountRepository(Menu_minder_dbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        public async Task<ResultAccountDTO> getAccountByEmail(string email)
        {
            Account account = await this.getAccountEntityByEmail(email);

            ResultAccountDTO result = _mapper.Map<ResultAccountDTO>(account);

            return result;
        }

        public async Task<Account> getAccountEntityByEmail(string email)
        {
            Account account = await this._context.Accounts.FirstOrDefaultAsync(e => e.Email == email);
            return account;
        }

        public async Task<List<ResultAccountDTO>> getAllAccounts()
        {
            List<ResultAccountDTO> data = new List<ResultAccountDTO>();
            try
            {
                data = await _context.Accounts
                    .Select(account => _mapper.Map<ResultAccountDTO>(account)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task SaveAccount(Account account)
        {
            this._context.Add(account);
            await this._context.SaveChangesAsync();
        }

        public async Task UpdateAccount(Account account)
        {
            this._context.Update(account);
            await this._context.SaveChangesAsync();
        }
    }
}
