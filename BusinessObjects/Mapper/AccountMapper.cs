using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.AuthDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public class AccountMapper : Profile
    {
        public AccountMapper()
        {
            CreateMap<Account, ResultAccountDTO>()
                .ForMember(dto => dto.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.HasValue 
                ? src.DateOfBirth.Value.ToDateTime(new TimeOnly(0, 0, 0)) 
                : default(DateTime)));

        }
    }
}
