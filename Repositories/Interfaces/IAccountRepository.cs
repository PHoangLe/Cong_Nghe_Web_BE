using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AccountDTO;

namespace Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> getAccountByEmail(string email);
        public Task<Account> SaveAccount(Account accountData);
        public Task<Account> UpdateAccount(Account accountData);
        public Task<List<AccountSuccinctDto>> findAllStaffAccount(string search);
        public Task<ResultAccountDTO> findAccountById(string accountId);
        public Task<Account> findAccountToUpdateById(string accountId);
        public Task BlockAccount(string accountId, Boolean isBlock);
        public Task DeleteAccount(string accountId);
    }
}
