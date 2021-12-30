using Autofac;
using EXIM.DataImporter.Contexts;
using EXIM.DataImporter.Repositories;
using EXIM.DataImporter.Services;
using EXIM.DataImporter.UnitOfWorks;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter
{
    public class DataImporterModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;
        private readonly IConfiguration _configuration;

        public DataImporterModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<DataImporterDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<DataImporterDbContext>().As<IDataImporterDbContext>()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<DataImporterDbContext>().AsSelf()
                .WithParameter("connectionString", _connectionString)
                .WithParameter("migrationAssemblyName", _migrationAssemblyName)
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupRepository>().As<IGroupRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImportRepository>().As<IImportRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelDataRepository>().As<IExcelDataRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExportRepository>().As<IExportRepository>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExcelFieldDataRepository>().As<IExcelFieldDataRepository>()
                .InstancePerLifetimeScope();

            builder.RegisterType<GroupService>().As<IGroupService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<StatusService>().As<IStatusService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<SummaryService>().As<ISummaryService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ImportService>().As<IImportService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<DashboardService>().As<IDashboardService>()
                .InstancePerLifetimeScope();
            builder.RegisterType<ExportService>().As<IExportService>()
                .InstancePerLifetimeScope();

            builder.RegisterType<DataImporterUnitOfWork>().As<IDataImporterUnitOfWork>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
