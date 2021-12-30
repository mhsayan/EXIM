using Autofac;
using EXIM.Web.Models;
using EXIM.Web.Models.Account;
using EXIM.Web.Models.GroupModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EXIM.Web
{
    public class WebModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<GroupListModel>().AsSelf();
            builder.RegisterType<UpdateGroupModel>().AsSelf();
            builder.RegisterType<CreateGroupModel>().AsSelf();
            builder.RegisterType<ImportModel>().AsSelf();
            builder.RegisterType<SummaryModel>().AsSelf();
            builder.RegisterType<DashboardModel>().AsSelf();
            builder.RegisterType<ExportModel>().AsSelf();
            builder.RegisterType<ConfirmEmailModel>().AsSelf();
            builder.RegisterType<RegisterConfirmationModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<GoogleConfigModel>().AsSelf();

            base.Load(builder);
        }
    }
}
