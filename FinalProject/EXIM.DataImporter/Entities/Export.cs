using EXIM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Entities
{
    public class Export : IEntity<int>
    {
        public int Id { get; set; }
        public string GroupName { get; set; }
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
