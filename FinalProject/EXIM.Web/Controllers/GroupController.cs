using EXIM.Common.Utilities;
using EXIM.Web.Models.GroupModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Claims;
using Autofac;
using Microsoft.AspNetCore.Http;

namespace EXIM.Web.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class GroupController : Controller
    {
        private readonly ILogger<GroupController> _logger;
        private readonly ILifetimeScope _scope;
        public GroupController(ILifetimeScope scope, ILogger<GroupController> logger)
        {
            _scope = scope;
            _logger = logger;
        }
        public IActionResult Index()
        {
            var model = _scope.Resolve<GroupListModel>();
            return View(model);
        }

        public JsonResult GetGroupData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<GroupListModel>();
            var data = model.GetGroups(dataTablesModel);
            return Json(data);
        }

        public IActionResult Create()
        {
            //var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = User.GetLoggedInUserId<Guid>();
            var model = _scope.Resolve<CreateGroupModel>();
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    model.ResolveDependency(_scope);
                    model.CreateGroup();
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Failed to create group");
                    _logger.LogError(ex, "Create Group Failed");
                }
            }
            return View(model);
        }
        public IActionResult Update(int id)
        {
            var model = _scope.Resolve<UpdateGroupModel>();
            model.LoadModelData(id);

            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Update(UpdateGroupModel model)
        {
            if (ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                model.Update();
            }

            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            var model = _scope.Resolve<GroupListModel>();
            model.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}