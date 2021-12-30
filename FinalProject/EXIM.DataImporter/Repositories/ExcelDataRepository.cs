using EXIM.Data;
using EXIM.DataImporter.Contexts;
using EXIM.DataImporter.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Repositories
{
    public class ExcelDataRepository : Repository<ExcelData, int>,
        IExcelDataRepository
    {
        public ExcelDataRepository(IDataImporterDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
