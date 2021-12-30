using EXIM.Data;
using EXIM.DataImporter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Repositories
{
    public interface IExcelDataRepository : IRepository<ExcelData, int>
    {
        //If necessary
    }
}
