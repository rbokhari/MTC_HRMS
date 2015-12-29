using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MTCHRMS.DC;
using MTCHRMS.DC.Implementation;
using MTCHRMS.DC.Interface;
using MTCHRMS.EntityFramework;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.DC.Implementation.HRMS;
using MTCHRMS.EntityFramework.Inventory;
using Ninject;
using MTCHRMS.DC.Interface.HRTR;
using MTCHRMS.DC.Implementation.HRTR;

namespace MTCHRMS.App_Start
{
    public static class NinjectConfig
    {
        public static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            //Create the bindings
            //kernel.Bind<IProductsRepository>().To<ProductRepository>();
            kernel.Bind<DbEntityContext>().To<DbEntityContext>();
            kernel.Bind<IDepartmentsRepository>().To<DepartmentRepository>();
            kernel.Bind<IStoragePathRepository>().To<StoragePathRepository>();
            kernel.Bind<IEmployeesRepository>().To<EmployeeRepository>();
            kernel.Bind<IValidationRepository>().To<ValidationRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();

            kernel.Bind<ISuppliersRepository>().To<SupplierRepository>();
            kernel.Bind<IStoreLocationRepository>().To<StoreLocationRepository>();
            kernel.Bind<IItemsRepository>().To<ItemsRepository>();
            kernel.Bind<IManufacturersRepository>().To<ManufactuerRepository>();
            kernel.Bind<IDistributionRepository>().To<DistributionRepository>();

            kernel.Bind<ITicketDefRepository>().To<TicketDefRepository>();
            kernel.Bind<ILeavesDefRepository>().To<LeaveDefRepository>();

            kernel.Bind<IServicesRepository>().To<ServicesRepository>();

            kernel.Bind<ICourseRepository>().To<CourseRepository>();

            return kernel;
        }
    }
}