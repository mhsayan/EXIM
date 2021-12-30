using EXIM.DataImporter.Entities;
using EXIM.Membership.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EXIM.DataImporter.Contexts
{
    public class DataImporterDbContext : DbContext, IDataImporterDbContext
    {
        private readonly string _connectionString;
        private readonly string _migrationAssemblyName;

        public DataImporterDbContext(string connectionString, string migrationAssemblyName)
        {
            _connectionString = connectionString;
            _migrationAssemblyName = migrationAssemblyName;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
        {
            if (!dbContextOptionsBuilder.IsConfigured)
            {
                dbContextOptionsBuilder.UseSqlServer(
                    _connectionString,
                    m => m.MigrationsAssembly(_migrationAssemblyName));
            }

            base.OnConfiguring(dbContextOptionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Group>()
                .HasKey(g => g.Id);
            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers", t => t.ExcludeFromMigrations())
                .HasMany<Group>()
                .WithOne(u => u.ApplicationUser); 

            modelBuilder.Entity<Import>()
                .HasKey(fs => fs.Id);
            modelBuilder.Entity<Import>()
               .HasOne(ed => ed.Group)
               .WithMany(g => g.Imports)
               .HasForeignKey(ed => ed.GroupId);

            modelBuilder.Entity<ExcelData>()
                .HasKey(ed => ed.Id);
            modelBuilder.Entity<ExcelData>()
                .HasOne(ed => ed.Group)
                .WithMany(g => g.ExcelDatas)
                .HasForeignKey(ed => ed.GroupId);

            modelBuilder.Entity<ExcelFieldData>()
                .HasKey(efd => efd.Id);
            modelBuilder.Entity<ExcelFieldData>()
                .HasOne(efd => efd.ExcelData)
                .WithMany(ed => ed.ExcelFieldData)
                .HasForeignKey(efd => efd.ExcelDataId);


            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Group> Groups { get; set; }
        public DbSet<ExcelData> ExcelData { get; set; }
        public DbSet<ExcelFieldData> ExcelFieldData { get; set; }
        public DbSet<Import> Imports { get; set; }
        public DbSet<Export> Exports { get; set; }
    }
}