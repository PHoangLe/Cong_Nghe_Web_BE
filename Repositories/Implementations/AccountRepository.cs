using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
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
            Account account = await this._context.Accounts.FirstOrDefaultAsync(e => e.Email == email);
            return account;
        }
    }
}
