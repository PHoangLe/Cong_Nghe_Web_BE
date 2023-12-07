using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public class FoodMapper : Profile
    {
        public FoodMapper()
        {
            CreateMap<Food, ResultFoodDto>();
        }
    }
}
