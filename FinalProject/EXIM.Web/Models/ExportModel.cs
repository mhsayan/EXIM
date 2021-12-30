using Autofac;
using EXIM.DataImporter.Services;
using Microsoft.AspNetCore.Http;
using EXIM.DataImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EXIM.Common.Utilities;
using AutoMapper;
using System.Text;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Data;

namespace EXIM.Web.Models
{
    public class ExportModel
    {
        [Required]
        public int Id { get; set; }
        [Required, MaxLength(100, ErrorMessage = "Group Name should be less than 100 characters")]
        public string GroupName { get; set; }
        [Required]
        public IFormFile FormFile { get; set; }
        public string FileUrl { get; set; }
        public DateTime Date { get; set; }
        public List<string> GroupList { get; set; }
        public List<Group> Groups { get; set; }


        private IExportService _exportService;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostEnvironment;

        public ExportModel()
        {            
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _mapper = scope.Resolve<IMapper>();
            _dateTimeUtility = scope.Resolve<IDateTimeUtility>();
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
            _exportService = scope.Resolve<IExportService>();
            _hostEnvironment = scope.Resolve<IWebHostEnvironment>();
        }

        public ExportModel(IExportService exportService,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper,
            IWebHostEnvironment hostEnvironment)
        {
            _exportService = exportService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
            _hostEnvironment = hostEnvironment;
        }
        
        public DataSet ExcelDataSet(int id)
        {
            return _exportService.Data(id);
        }

        public string FileName(int id)
        {
            return _exportService.FileNameGenerator(id);
        }
        public void CreateExportStatus(int id)
        {
            _exportService.CreateExportStatus(id);
        }
    }
}