using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public interface IDashboardService
    {
        int getTotalImports();
        int getTotalExports();
        int getTotalGroups();
    }
}
