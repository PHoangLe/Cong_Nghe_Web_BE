using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using BusinessObjects.DTO.AuthDTO;

namespace BusinessObjects.Mapper
{
    public class AuthMapper : Profile
    {
        public AuthMapper()
        {
            CreateMap<Account, GetAuthAccountDto>();
            CreateMap<GetAuthAccountDto, ResultLoginDto>();
        }
    }
}
