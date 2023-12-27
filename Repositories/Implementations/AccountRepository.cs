using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
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

        public async Task<Account> getAccountByEmail(string email)
        {
            Account? account = await this._context.Accounts.FirstOrDefaultAsync(e => e.Email == email);
            return account;
        }

        public async Task<Account> SaveAccount(Account accountData)
        {
            this._context.Accounts.Add(accountData);
            await this._context.SaveChangesAsync();
            return accountData;
        }

        public async Task<Account> UpdateAccount(Account accountData)
        {
            try
            {
                this._context.Accounts.Update(accountData);
                var data = await this._context.SaveChangesAsync();
                return accountData;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<AccountSuccinctDto>> findAllStaffAccount(string search = "")
        {
            List<AccountSuccinctDto> data = new List<AccountSuccinctDto>();
            try
            {
                data = await _context.Accounts
                    .Where(account => account.Name.Contains(search) || account.Email.Contains(search) || account.PhoneNumber.Contains(search))
                    .Select(account => _mapper.Map<AccountSuccinctDto>(account)).ToListAsync();

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return data;
        }

        public async Task<ResultAccountDTO> findAccountById(string accountId)
        {
            ResultAccountDTO? data = new ResultAccountDTO();
            try
            {
                data = await this._context.Accounts
                    .Where(account => account.AccountId.ToString() == accountId)
                    .Select(account => _mapper.Map<ResultAccountDTO>(account))
                    .FirstOrDefaultAsync(); 
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
            return data;
        }

        public async Task<Account> findAccountToUpdateById(string accountId)
        {
            Account? data = new Account();
            try
            {
                data = await this._context.Accounts
                    .Where(account => account.AccountId.ToString() == accountId)
                    .FirstOrDefaultAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return data;
        }

        public async Task BlockAccount(string accountId, Boolean isBlock)
        {
            try
            {
                Account accountBlock = await this._context.Accounts.FirstOrDefaultAsync(a => a.AccountId.ToString() == accountId);
                if(accountBlock != null)
                {
                    accountBlock.IsBlock = isBlock;
                    this._context.Accounts.Update(accountBlock);
                    await this._context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task DeleteAccount(string accountId)
        {
            try
            {
                Account accountDelete = await this._context.Accounts.FirstOrDefaultAsync(a => a.AccountId.ToString() == accountId);
                if(accountDelete != null)
                {
                    this._context.Accounts.Remove(accountDelete);
                    await this._context.SaveChangesAsync();
                }
            }
            catch  (Exception ex) 
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
