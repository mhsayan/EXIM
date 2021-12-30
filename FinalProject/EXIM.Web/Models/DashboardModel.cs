using Autofac;
using EXIM.DataImporter.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web.Models
{
    public class DashboardModel
    {
        private IDashboardService _dashboardService;

        public DashboardModel()
        {
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            _dashboardService = scope.Resolve<IDashboardService>();
        }

        public DashboardModel(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        public int totalImports()
        {
            return _dashboardService.getTotalImports(); 
        }
        public int totalGroups()
        {
            return _dashboardService.getTotalGroups();
        }
        public int totalExports()
        {
            return _dashboardService.getTotalExports();
        }


    }
}
