using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;

namespace BusinessObjects.Mapper
{
    public class DiningTableMapper : Profile
    {
        public DiningTableMapper()
        {
            CreateMap<DiningTable, ResultDiningTableDto>();
        }
    }
}
