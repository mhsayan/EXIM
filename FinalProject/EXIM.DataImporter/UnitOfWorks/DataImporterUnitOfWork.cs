using EXIM.Data;
using EXIM.DataImporter.Contexts;
using EXIM.DataImporter.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.UnitOfWorks
{
    public class DataImporterUnitOfWork : UnitOfWork, IDataImporterUnitOfWork
    {
        public IGroupRepository Groups { get; private set; }
        public IImportRepository Imports { get; private set; }
        public IExportRepository Exports { get; private set; }
        public IExcelDataRepository ExcelData { get; private set; }
        public IExcelFieldDataRepository ExcelFieldData { get; private set; }

        public DataImporterUnitOfWork(IDataImporterDbContext context,
                                  IGroupRepository groups,
                                  IImportRepository imports,
                                  IExportRepository exports,
                                  IExcelDataRepository excelData,
                                  IExcelFieldDataRepository excelFieldData
                                 ) : base((DbContext)context)
        {
            Groups = groups;
            Imports = imports;
            Exports = exports;
            ExcelData = excelData;
            ExcelFieldData = excelFieldData;
        }
    }
}