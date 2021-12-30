using Autofac;
using EXIM.DataImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public interface IImportService
    {
        void ResolveDependency(ILifetimeScope scope);
        void ImportExceData(string path, string groupName, int id);
        void AddFieldData(List<ExcelFieldData> excelFieldData);
        void AddTableRow(string groupName, int id);
        void GetImportFiles();
        void UpdateStatus(Entities.Import import);
    }
}
