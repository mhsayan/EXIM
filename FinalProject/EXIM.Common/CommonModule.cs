using Autofac;
using EXIM.Common.Utilities;

namespace EXIM.Common
{
    public class CommonModule : Module
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public CommonModule(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<EmailService>().As<IEmailService>()
                .WithParameter("host", "smtp.gmail.com")
                .WithParameter("port", 465)
                .WithParameter("username", "Email Address")
                .WithParameter("password", "Password")
                .WithParameter("useSSL", true)
                .WithParameter("from", "Email Address")
                .InstancePerLifetimeScope();

            builder.RegisterType<DateTimeUtility>().As<IDateTimeUtility>()
                .InstancePerLifetimeScope();

            base.Load(builder);
        }
    }
}
