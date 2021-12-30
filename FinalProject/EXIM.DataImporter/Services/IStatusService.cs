using EXIM.DataImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public interface IStatusService
    {
        IList<Import> GetAllImports();
        void CreateImportStatus(Import import);
        (IList<Import> records, int total, int totalDisplay) GetImports(int pageIndex, int pageSize,
            string searchText, string sortText);
        Import GetImport(int id);
        void UpdateImportStatus(Import import);
    }
}
