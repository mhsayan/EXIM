using EXIM.DataImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public interface ISummaryService
    {
        IList<Import> GetImportList();
        (IList<Import> records, int total, int totalDisplay) GetImports(int pageIndex, int pageSize,
            string searchText, string sortText);
        (IList<Export> records, int total, int totalDisplay) GetExports(int pageIndex, int pageSize,
            string searchText, string sortText);        
        IList<Export> GetExportList();
    }
}
