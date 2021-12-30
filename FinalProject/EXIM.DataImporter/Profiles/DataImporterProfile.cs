using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EO = EXIM.DataImporter.Entities;
using BO = EXIM.DataImporter.BusinessObjects;

namespace EXIM.DataImporter.Profiles
{
    public class DataImporterProfile : Profile
    {
        public DataImporterProfile()
        {
            CreateMap<EO.Group, BO.Group>().ReverseMap();
            CreateMap<EO.Import, BO.Import>().ReverseMap();
            CreateMap<EO.Export, BO.Export>().ReverseMap();
            CreateMap<EO.ExcelFieldData, BO.ExcelFieldData>().ReverseMap();
            CreateMap<EO.ExcelData, BO.ExcelData>().ReverseMap();
        }
    }
}
