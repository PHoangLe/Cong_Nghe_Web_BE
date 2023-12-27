using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AccountDTO;
using Microsoft.Extensions.Logging;
using Repositories.Implementations;
using Repositories.Interfaces;
using Services.Exceptions;

namespace Services
{
    public class MeService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<MeService> _logger;
        private readonly IAccountRepository _accountRepository;

        public MeService (IAccountRepository accountRepository, IMapper mapper, ILogger<MeService> logger)
        {
            this._accountRepository = accountRepository;
            this._mapper = mapper;
            this._mapper = mapper;
        }

        public async Task updatePassword(string AccountId, UpdatePasswordDto PassInvo)
        {
            try
            {
                string oldPassword = PassInvo.password;
                string newPassword = PassInvo.newPassword;

                // get exist account
                Account? existAccount = await this._accountRepository.findAccountToUpdateById(AccountId);
                if (existAccount != null)
                {
                    if(existAccount.Password != oldPassword) {
                        throw new UnauthorizedException("Password not matching!");
                    }
                    else
                    {
                        existAccount.Password = newPassword;
                        await this._accountRepository.UpdateAccount(existAccount);
                    }
                }
            }
            catch (UnauthorizedException ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex.ToString());
                throw new Exception(ex.Message);
            }
        }
    }
}
