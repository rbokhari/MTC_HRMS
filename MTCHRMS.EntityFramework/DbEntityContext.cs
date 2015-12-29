using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;
using MTCHRMS.EntityFramework.HRMS;
using MTCHRMS.EntityFramework.Inventory;
using MTCHRMS.EntityFramework.Security;
using MTCHRMS.EntityFramework.Training;

namespace MTCHRMS.EntityFramework
{
    public class DbEntityContext : DbContext
    {
        public DbEntityContext()
            : base("HRMSConnection")
        {
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
            Database.SetInitializer(
                new MigrateDatabaseToLatestVersion<DbEntityContext, HRMSMigrationConfiguration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<EmployeeDef>()
            //    .HasRequired(a => a.EmployeeMarital)
            //    .WithMany().HasForeignKey(a => a.Id);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<EmployeeDef> EmployeeDefs { get; set; }
        public DbSet<EmployeePassport> EmployeePassports { get; set; }
        public DbSet<EmployeeVisa> EmployeeVisas { get; set; }
        public DbSet<Validation> Validations { get; set; }
        public DbSet<ValidationDetail> ValidationDetails { get; set; }

        public DbSet<EmployeeQualification> EmployeeQualifications { get; set; }

        public DbSet<EmployeePreviousEmployment> PreviousEmployments { get; set; }

        public DbSet<EmployeeMarital> EmployeeMaritals { get; set; }

        public DbSet<EmployeeChildren> EmployeeChildrens { get; set; }

        public DbSet<EmployeeKin> EmployeeKins { get; set; }

        public DbSet<EmployeeContract> EmployeeContracts { get; set; }

        public DbSet<AC_Module> Modules { get; set; }

        public DbSet<AC_Role> Roles { get; set; }

        public DbSet<AC_User> Users { get; set; }

        public DbSet<AutoSetting> AutoSettings { get; set; }

        public DbSet<Manufacturer> Manufacturers { get; set; }

        public DbSet<ManufacturerContactPerson> ManufacturerContactPersons { get; set; }

        public DbSet<ManufacturerContract> ManufacturerContracts { get; set; }

        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<SupplierContactPerson> SupplierContactPersons { get; set; }
        public DbSet<SupplierContract> SupplierContracts { get; set; }

        public DbSet<Inventory.StoreLocation> StoreLocations { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemDepartment> ItemDepartments { get; set; }

        public DbSet<ItemYear> ItemYears { get; set; }

        public DbSet<ItemSupplier> ItemSuppliers { get; set; }

        public DbSet<ItemManufacturer> ItemManufacturers { get; set; }

        public DbSet<ItemProvision> ItemProvisions { get; set; }

        public DbSet<ItemStockAdd> ItemStockAdds { get; set; }

        public DbSet<ItemStockSerial> ItemStockSerials { get; set; }

        public DbSet<StoragePath> StoragePaths { get; set; }

        public DbSet<ItemAttachment> ItemAttachments { get; set; }

        public DbSet<Distribution> Distributions { get; set; }

        public DbSet<DistributionItem> DistributionItems { get; set; }

        public DbSet<TicketDef> Tickets { get; set; }

        public DbSet<LeaveDef> Leaves { get; set; }

        public DbSet<EmployeeLeaveCategory> EmployeeLeaveCategories { get; set; }

        public DbSet<EmployeeLeaveYear> EmployeeLeaveYears { get; set; }

        public DbSet<EmployeeTicketCategory> EmployeeTicketCategories { get; set; }

        public DbSet<EmployeeTicketYear> EmployeeTicketYears { get; set; }

        public DbSet<EmployeeLeave> EmployeeLeaves { get; set; }

        public DbSet<CourseDef> Courses { get; set; }

    }
}
