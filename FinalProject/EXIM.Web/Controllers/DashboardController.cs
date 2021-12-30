using Autofac;
using EXIM.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class DashboardController : Controller
    {
        private readonly ILifetimeScope _scope;
        public DashboardController(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public IActionResult Index()
        {
            var model = _scope.Resolve<DashboardModel>();
            ViewBag.import = model.totalImports();
            ViewBag.export = model.totalExports();
            ViewBag.group = model.totalGroups();

            return View();
        }
    }
}
