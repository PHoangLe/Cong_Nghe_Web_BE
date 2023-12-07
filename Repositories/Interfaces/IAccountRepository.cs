using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;

namespace Repositories.Interfaces
{
    public interface IAccountRepository
    {
        public Task<Account> getAccountByEmail(string email);
        // public Task SaveAccount(Account account);
    }
}
