using Autofac;
using EXIM.Common.Utilities;
using EXIM.DataImporter.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Models.GroupModels
{
    public class GroupListModel
    {
        private IGroupService _groupService;
        private IHttpContextAccessor _httpContextAccessor;

        public GroupListModel()
        {
            //_groupService = Startup.AutofacContainer.Resolve<IGroupService>();
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            //By this way we need to Resolve this.
            _groupService = scope.Resolve<IGroupService>();
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
        }

        public GroupListModel(IGroupService groupService, IHttpContextAccessor httpContextAccessor)
        {
            _groupService = groupService;
            _httpContextAccessor = httpContextAccessor;
        }

        internal object GetGroups(DataTablesAjaxRequestModel tableModel)
        {
            var data = _groupService.GetGroups(
                tableModel.PageIndex,
                tableModel.PageSize,
                tableModel.SearchText,
                tableModel.GetSortText(new string[] { "Name" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Name,
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }

        internal void Delete(int id)
        {
            //var userId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<Guid>();
            _groupService.DeleteGroup(id);
        }
    }
}
