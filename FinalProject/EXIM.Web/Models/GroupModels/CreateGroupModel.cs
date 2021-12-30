using Autofac;
using BO = EXIM.DataImporter.BusinessObjects;
using EXIM.DataImporter.Services;
using EXIM.Membership.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EXIM.Common.Utilities;

namespace EXIM.Web.Models.GroupModels
{
    public class CreateGroupModel
    {
        [Required, MaxLength(100, ErrorMessage = "Name should be less than 100 characters")]
        public string Name { get; set; }

        [Required]
        public Guid UserId { get; set; }

        private IGroupService _groupService;
        private IHttpContextAccessor _httpContextAccessor;

        public CreateGroupModel()
        {
        }
        public void ResolveDependency(ILifetimeScope scope)
        {
            //By this way we need to Resolve this.
            _groupService = scope.Resolve<IGroupService>();
            _httpContextAccessor = scope.Resolve<IHttpContextAccessor>();
        }

        public CreateGroupModel(IGroupService groupService)
        {
            _groupService = groupService;
        }

        internal void CreateGroup()
        {
            UserId = _httpContextAccessor.HttpContext.User.GetLoggedInUserId<Guid>();
            var group = new BO.Group
            { 
                Name = Name,
                ApplicationUserId = UserId
            };

            _groupService.CreateGroup(group);
        }
    }
}
