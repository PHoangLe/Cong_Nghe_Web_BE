using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BusinessObjects.DataModels;
using BusinessObjects.DTO.AuthDTO;

namespace BusinessObjects.Mapper
{
    public class PermissonMapper: Profile
    {
        public PermissonMapper() {
            CreateMap<Permission, PermissonDto>();
        }
    }
}
