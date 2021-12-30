using AutoMapper;
using EXIM.DataImporter.BusinessObjects;
using EXIM.Web.Models;
using EXIM.Web.Models.GroupModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Profiles
{
    public class WebProfile : Profile
    {
        public WebProfile()
        {
            CreateMap<CreateGroupModel, Group>().ReverseMap();
            CreateMap<UpdateGroupModel, Group>().ReverseMap();
            CreateMap<ImportModel, Group>().ReverseMap();
        }
    }
}
