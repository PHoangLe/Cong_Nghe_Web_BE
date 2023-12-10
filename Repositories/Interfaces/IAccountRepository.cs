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
        public Task<List<ResultAccountDTO>> getAllAccounts();
        public Task<Account> getAccountEntityByEmail(string email);
        public Task<ResultAccountDTO> getAccountByEmail(string email);
        public Task SaveAccount(Account account);
        public Task UpdateAccount(Account account);
    }
}
