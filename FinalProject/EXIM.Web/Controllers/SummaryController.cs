using Autofac;
using EXIM.Common.Utilities;
using EXIM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class SummaryController : Controller
    {
        private readonly ILogger<SummaryController> _logger;
        private readonly ILifetimeScope _scope;
        public SummaryController(ILifetimeScope scope, ILogger<SummaryController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Imports()
        {
            return View();
        }

        public JsonResult GetImportData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<SummaryModel>();
            var data = model.GetImports(dataTablesModel);
            return Json(data);
        }
        public IActionResult Exports()
        {
            return View();
        }

        public JsonResult GetExportData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<SummaryModel>();
            var data = model.GetExports(dataTablesModel);
            return Json(data);
        }
    }
}
