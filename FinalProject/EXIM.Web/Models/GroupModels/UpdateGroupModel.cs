using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using EXIM.DataImporter.Services;
using BO = EXIM.DataImporter.BusinessObjects;
using Microsoft.AspNetCore.Http;
using EXIM.Common.Utilities;

namespace EXIM.Web.Models.GroupModels
{
    public class UpdateGroupModel
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }

        [Required, MaxLength(100, ErrorMessage = "Name should be less than 100 characters")]
        public string Name { get; set; }

        private IGroupService _groupService;
        private IMapper _mapper;
        private IHttpContextAccessor _httpContextAccessor;

        public UpdateGroupModel()
        {
        }

        public UpdateGroupModel(IGroupService groupService, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _groupService = groupService;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
        }

        public void ResolveDependency(ILifetimeScope scope)
        {
            //By this way we need to Resolve this.
            _groupService = scope.Resolve<IGroupService>();
            _mapper = scope.Resolve<IMapper>();
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
        }

        public void LoadModelData(int id)
        {
            var group = _groupService.GetGroup(id);
            _mapper.Map(group, this);
        }

        internal void Update()
        {
            //var group = _mapper.Map<BO.Group>(this);
            UserId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<Guid>();

            var group = new BO.Group
            {
                Id = Id,
                ApplicationUserId = UserId,
                Name = Name
            };

            _groupService.UpdateGroup(group);
        }
    }
}
