using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AuthDTO;
using Microsoft.Extensions.Logging;
using Repositories;
using Repositories.Interfaces;
using Services.Exceptions;
using Services.Helpers;


namespace Services
{
    public class AuthService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AuthService> _logger;
        private readonly IAccountRepository _accountRepository;
        private readonly JwtServices _jwtService;
        private readonly PermissionService _permissionService;

        public AuthService(ILogger<AuthService> logger, IMapper mapper, JwtServices jwtService, IAccountRepository accountRepository, PermissionService permissionService )
        {
            this._logger = logger;
            this._mapper = mapper;
            this._accountRepository = accountRepository;
            this._jwtService = jwtService;
            this._permissionService = permissionService;
        }

        public async Task<ResultLoginDto> loginWithEmailPassword(LoginDto dataLoginInvo)
        {
            try
            {
                // query exist account by email
                Account accountExists = await this._accountRepository.getAccountEntityByEmail(dataLoginInvo.Email);
                GetAuthAccountDto accountExistMap = this._mapper.Map<Account, GetAuthAccountDto>(accountExists);

                // check email exist
                if (accountExistMap == null)
                {
                    throw new BadRequestException("Email is not exists!");
                }

                // check is not blocked
                if (accountExistMap.IsBlock == true)
                {
                    throw new UnauthorizedException("Account is blocked");
                }

                // check password valid
                if (dataLoginInvo.Password != accountExistMap.Password)
                {
                    throw new UnauthorizedException("Password is invalid");
                }

                // generate AccessToken JWT
                string accessToken = _jwtService.GenerateToken(accountExistMap.AccountId.ToString(), accountExistMap.Email, accountExistMap.Role);

                // get user Permissions
                List<Permission> permissions = await _permissionService.GetPermissionsByUserId(accountExists.AccountId);

                ResultLoginDto resultLogin = this._mapper.Map<GetAuthAccountDto, ResultLoginDto>(accountExistMap);

                resultLogin.AccessToken = accessToken;
                resultLogin.Permissions = permissions;

                return resultLogin;
            }
            catch(BadRequestException ex)
            {
                throw ex;
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
