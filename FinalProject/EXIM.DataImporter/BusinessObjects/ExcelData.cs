using EXIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.BusinessObjects
{
    public class ExcelData : IEntity<int>
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public Group Group { get; set; }
        public List<ExcelFieldData> ExcelFieldData { get; set; }
    }
}
