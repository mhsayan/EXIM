using EXIM.Data;
using EXIM.Membership.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Entities
{
    public class Group : IEntity<int>
    {
        public int Id { get; set; }
        public Guid ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
        public string Name { get; set; }
        public List<ExcelData> ExcelDatas { get; set; }
        public List<Import> Imports { get; set; }
    }
}
