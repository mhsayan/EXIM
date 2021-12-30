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

namespace EXIM.Web.Models
{
    public class ImportModel
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


        private IStatusService _statusService;
        private IGroupService _groupService;
        private IMapper _mapper;
        private IDateTimeUtility _dateTimeUtility;
        private IHttpContextAccessor _httpContextAccessor;
        private IWebHostEnvironment _hostEnvironment;

        public ImportModel()
        {            
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _groupService = scope.Resolve<IGroupService>();
            _mapper = scope.Resolve<IMapper>();
            _dateTimeUtility = scope.Resolve<IDateTimeUtility>();
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
            _statusService = scope.Resolve<IStatusService>();
            _hostEnvironment = scope.Resolve<IWebHostEnvironment>();
        }

        public ImportModel(IGroupService groupService,
            IHttpContextAccessor httpContextAccessor,
            IDateTimeUtility dateTimeUtility,
            IMapper mapper,
            IStatusService statusService,
            IWebHostEnvironment hostEnvironment)
        {
            _groupService = groupService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _dateTimeUtility = dateTimeUtility;
            _statusService = statusService;
            _hostEnvironment = hostEnvironment;
        }

        public IList<Group> GetGroupList()
        {
            var groups = _groupService.GetGroupList();
            var count = groups.Count();
            return groups;            
        }
        public IList<string> GetGroupLists()
        {
            var groups = _groupService.GetGroupList();

            foreach (var item in groups)
            {
                GroupList.Add(item.Name);
            }

            return GroupList;
        }

        public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            Id = (int)(group?.Id);
            GroupName = group?.Name;
        }
        private string MakeUrl(string fileName)
        {
            string webRootPath = Path.Combine(_hostEnvironment.WebRootPath, "Upload"); 
            var path = Path.Combine(webRootPath, fileName);
            return path;
        }
        internal void CreateFileStatus()
        {
            var import = new Import 
            {
                GroupId = Id,
                GroupName = GroupName,
                Date = _dateTimeUtility.Now,
                Status = "Processing",
                FileName = FormFile.FileName                
            };
            import.FilePath = MakeUrl(import.FileName);

            _statusService.CreateImportStatus(import);
        }

        public string TableHtmlData(IFormFile formFile, string filePath)
        {
            var _previewExcel = new PreviewExcel();
            var Tabledata = _previewExcel.ExcelDataToTable(formFile, filePath);
            return Tabledata;
        }
    }
}