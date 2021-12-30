using EXIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Entities
{
    public class Import : IEntity<int>
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public DateTime Date { get; set; }
        public string Status { get; set; }
    }
}
