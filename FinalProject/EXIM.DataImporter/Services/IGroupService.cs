using EXIM.DataImporter.BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public interface IGroupService
    {
        void CreateGroup(Group group);
        (IList<Group> records, int total, int totalDisplay) GetGroups(int pageIndex, int pageSize,
            string searchText, string sortText);
        Group GetGroup(int id);
        void UpdateGroup(Group group);
        void DeleteGroup(int id);
        IList<Group> GetGroupList();
    }
}
