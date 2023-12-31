using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;
using BusinessObjects.DTO.BillDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public class BillMapper : Profile
    {
        public BillMapper()
        {
            CreateMap<Bill, BillResultDto>();
        }
    }
}
