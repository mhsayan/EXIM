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
    public class GroupRepository : Repository<Group, int>,
        IGroupRepository
    {
        public GroupRepository(IDataImporterDbContext context)
            : base((DbContext)context)
        {
        }
    }
}
