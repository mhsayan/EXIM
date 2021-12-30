using EXIM.DataImporter.UnitOfWorks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDataImporterUnitOfWork _dataImporterUnitOfWork;

        public DashboardService(IDataImporterUnitOfWork dataImporterUnitOfWork)
        {
            _dataImporterUnitOfWork = dataImporterUnitOfWork;
        }

        public int getTotalImports()
        {
            return _dataImporterUnitOfWork.Imports.GetAll().Count;
        }
        public int getTotalExports()
        {
            return _dataImporterUnitOfWork.Exports.GetAll().Count;
        }
        public int getTotalGroups()
        {
            return _dataImporterUnitOfWork.Groups.GetAll().Count;
        }

    }
}
