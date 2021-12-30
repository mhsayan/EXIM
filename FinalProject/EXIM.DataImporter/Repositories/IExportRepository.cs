using EXIM.Data;
using EXIM.DataImporter.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Repositories
{
    public interface IExportRepository : IRepository<Export, int>
    {
        //If necessary
    }
}
