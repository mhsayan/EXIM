using Autofac;
using EXIM.DataImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public interface IExportService
    {
        DataSet Data(int id);
        List<ExcelFieldData> Row(int num);
        string FileNameGenerator(int id);
        void CreateExportStatus(int id);
    }
}
