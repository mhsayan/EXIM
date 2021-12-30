using Autofac;
using EXIM.Common.Utilities;
using EXIM.DataImporter.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Models
{
    public class SummaryModel
    {
        private ISummaryService _summaryService;
        private IHttpContextAccessor _httpContextAccessor;

        public SummaryModel()
        {
            //_summaryService = Startup.AutofacContainer.Resolve<ISummaryService>();
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _summaryService = scope.Resolve<ISummaryService>();
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
        }

        public SummaryModel(ISummaryService summaryService, IHttpContextAccessor httpContextAccessor)
        {
            _summaryService = summaryService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetImports(DataTablesAjaxRequestModel tableModel)
        {
            var data = _summaryService.GetImports(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "FileName", "GroupName", "Date", "Status" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.FileName,
                            record.GroupName,
                            record.Date.ToString(),
                            record.Status,
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
        internal object GetExports(DataTablesAjaxRequestModel tableModel)
        {
            var data = _summaryService.GetExports(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] {"GroupName", "Email", "Date" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.GroupName,
                            record.Email,
                            record.Date.ToString()
                        }
                    ).ToArray()
            };
        }
    }
}
