using EXIM.Data;
using EXIM.DataImporter.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.UnitOfWorks
{
    public interface IDataImporterUnitOfWork : IUnitOfWork
    {
        IGroupRepository Groups { get; }
        IImportRepository Imports { get; }
        IExportRepository Exports { get; }
        IExcelDataRepository ExcelData { get; }
        IExcelFieldDataRepository ExcelFieldData { get; }
    }
}
