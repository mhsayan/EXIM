using AutoMapper;
using EXIM.DataImporter.BusinessObjects;
using EXIM.DataImporter.Exceptions;
using EXIM.DataImporter.UnitOfWorks;
using EXIM.Membership.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Services
{
    public class GroupService : IGroupService
    {
        private readonly IDataImporterUnitOfWork _dataImporterUnitOfWork;
        private readonly IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public GroupService(IDataImporterUnitOfWork dataImporterUnitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<ApplicationUser> userManager)
        {
            _dataImporterUnitOfWork = dataImporterUnitOfWork;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        private bool IsTitleAlreadyUsed(string name, Guid userId) =>
            _dataImporterUnitOfWork.Groups.GetCount(x => x.Name == name && x.ApplicationUserId != userId) > 0;

        public void CreateGroup(Group group)
        {
            if (group == null)
                throw new InvalidParameterException("Group was not provided");

            if (IsTitleAlreadyUsed(group.Name, group.ApplicationUserId))
                throw new DuplicateTitleException("Group Name already exists");

            _dataImporterUnitOfWork.Groups.Add(
                _mapper.Map<Entities.Group>(group)
            );

            _dataImporterUnitOfWork.Save();
        }

        public (IList<Group> records, int total, int totalDisplay) GetGroups
            (int pageIndex, int pageSize, string searchText, string sortText)
        {
            var groupData = _dataImporterUnitOfWork.Groups.GetDynamic(
                string.IsNullOrWhiteSpace(searchText) ? null : x => x.Name.Contains(searchText),
                sortText, string.Empty, pageIndex, pageSize);

            var resultData = (from groups in groupData.data
                              select _mapper.Map<Group>(groups)).ToList();

            return (resultData, groupData.total, groupData.totalDisplay);
        }

        public IList<Group> GetGroupList()
        {
            var groupEntity = _dataImporterUnitOfWork.Groups.GetAll();

            var groups = new List<Group>();

            foreach (var entity in groupEntity)
            {
                var group = _mapper.Map<Group>(entity);
                groups.Add(group);
            }
            return groups;         
        }

        public Group GetGroup(int id)
        {
            var group = _dataImporterUnitOfWork.Groups.GetById(id);

            if (group == null) return null;

            return _mapper.Map<Group>(group);
        }

        public void UpdateGroup(Group group)
        {
            if (group == null)
                throw new InvalidOperationException("Group is missing");

            if (IsTitleAlreadyUsed(group.Name, group.ApplicationUserId))
                throw new DuplicateTitleException("Group name already used in other group.");

            var groupEntity = _dataImporterUnitOfWork.Groups.GetById(group.Id);

            if (groupEntity != null)
            {
                _mapper.Map(group, groupEntity);
                _dataImporterUnitOfWork.Save();
            }
            else
                throw new InvalidOperationException("Couldn't find group");
        }

        public void DeleteGroup(int id)
        {
            _dataImporterUnitOfWork.Groups.Remove(id);
            _dataImporterUnitOfWork.Save();
        }
    }

}
