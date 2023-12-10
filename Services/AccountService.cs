using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AccountDTO;
using Microsoft.Extensions.Logging;
using Repositories.Interfaces;
using Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AccountService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly IPermitRepository _permitRepository;

        public AccountService(ILogger<AuthService> logger, IMapper mapper, JwtServices jwtService, IAccountRepository accountRepository, IPermitRepository permitRepository)
        {
            this._logger = logger;
            this._mapper = mapper;
            this._accountRepository = accountRepository;
            this._permitRepository = permitRepository;
        }

        public async Task<List<Permit>> PermissionsToPermits(List<int> permissionIds , Account account)
        {
            await _permitRepository.DeletePermitByUserId(account.AccountId);

            List<Permit> result = new List<Permit>();

            foreach (int permissionId in permissionIds)
            {
                Permit permit = new Permit();
                permit.PermissionId = permissionId;
                permit.AccountId = account.AccountId;

                await _permitRepository.SavePermit(permit);

                result.Add(permit);
            }

            return result;
        }

        public async Task<ResultAccountDTO> GetAccountByEmail(string email)
        {
            return await _accountRepository.getAccountByEmail(email);
        }
        
        public async Task<Account> GetAccountEntityByEmail(string email)
        {
            return await _accountRepository.getAccountEntityByEmail(email);
        }

        public async Task CreateNewAccount(CreateAccountDTO createAccountDTO)
        {
            try
            {
                Account existedAccount = await _accountRepository.getAccountEntityByEmail(createAccountDTO.Email);
                if (existedAccount != null)
                {
                    throw new Exception("Account existed!");
                }

                Account createAccount = new Account();

                createAccount.Avatar = createAccountDTO.Avatar;
                createAccount.PhoneNumber = createAccountDTO.PhoneNumber;
                createAccount.Name = createAccountDTO.Name;
                createAccount.DateOfBirth = DateOnly.FromDateTime(createAccountDTO.DateOfBirth);
                createAccount.CreatedAt = DateTime.Now;
                createAccount.UpdatedAt = DateTime.Now;
                createAccount.IsBlock = false;
                createAccount.Email = createAccountDTO.Email;
                createAccount.Password = createAccountDTO.Password;
                createAccount.Role = createAccountDTO.Role;

                await _accountRepository.SaveAccount(createAccount);
                List<Permit> permits = await PermissionsToPermits(createAccountDTO.PermissionsIds, createAccount);
                createAccount.Permits = permits;

                await _accountRepository.UpdateAccount(createAccount);
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                throw;
            }
        }

        public async Task<List<ResultAccountDTO>> GetAllAccounts()
        {
            return await _accountRepository.getAllAccounts();
        }

        public async Task UpdateAccount(string email, CreateAccountDTO updateAccountDTO)
        {
            Account updateAccount = await _accountRepository.getAccountEntityByEmail(email);
            if (updateAccount == null)
            {
                throw new Exception("Account does not exist!");
            }

            updateAccount.Avatar = updateAccountDTO.Avatar;
            updateAccount.PhoneNumber = updateAccountDTO.PhoneNumber;
            updateAccount.Name = updateAccountDTO.Name;
            updateAccount.DateOfBirth = DateOnly.FromDateTime(updateAccountDTO.DateOfBirth);
            updateAccount.UpdatedAt = DateTime.Now;
            updateAccount.Password = updateAccountDTO.Password;
            updateAccount.Role = updateAccountDTO.Role;

            await _accountRepository.SaveAccount(updateAccount);
            List<Permit> permits = await PermissionsToPermits(updateAccountDTO.PermissionsIds, updateAccount);
            updateAccount.Permits = permits;

            await _accountRepository.UpdateAccount(updateAccount);
        }

        public async Task DeleteAccount(string email)
        {
            Account account = await this.GetAccountEntityByEmail(email);

            if (account == null)
            {
                throw new Exception("Account does not exist");
            }

            account.IsBlock = true;

            await _accountRepository.UpdateAccount(account);
        }
    }
}
