using Autofac;
using EXIM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
    public class ContactController : Controller
    {
        private readonly ILogger<ContactController> _logger;
        private readonly ILifetimeScope _scope;
        private IWebHostEnvironment _hostEnvironment;
        public ContactController(ILifetimeScope scope, ILogger<ContactController> logger,
            IWebHostEnvironment hostEnvironment)
        {
            _scope = scope;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Import(int id)
        {
            var model = _scope.Resolve<ImportModel>();
            model.LoadModelData(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Import(ImportModel model)
        {            
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    model.CreateFileStatus();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to Import File");
                    _logger.LogError(ex, "Same File Exists, Failed");
                }
                return RedirectToAction(actionName: "Imports", controllerName: "Summary");
            }
            else
            {
                IFormFile file = Request.Form.Files[0];
                model.FileUrl = Path.Combine(_hostEnvironment.WebRootPath, "Upload");
                var Tabledata = model.TableHtmlData(file, model.FileUrl);
                return this.Content(Tabledata);
            }
        }
        public IActionResult Export(int id)
        {
            var model = _scope.Resolve<ExportModel>();
            var dataSet = model.ExcelDataSet(id);
            var memoryStream = new MemoryStream();

            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using var package = new ExcelPackage(memoryStream);
            var workSheet = package.Workbook.Worksheets.Add("Sheet1");
            workSheet.Cells.LoadFromDataTable(dataSet.Tables[0], true);
            package.Save();

            memoryStream.Position = 0;
            var fileName = model.FileName(id);

            model.CreateExportStatus(id);

            return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
        }
    }
}
