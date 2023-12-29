using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AccountDTO;
using BusinessObjects.DTO.FeedBackDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.Mapper
{
    public class FeedBackMapper : Profile
    {
        public FeedBackMapper()
        {
            CreateMap<FeedBack, ResultFeedBackDto>();
        }
    }
}
