using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MTCHRMS.DC;
using MTCHRMS.EntityFramework;
using Ninject;

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
            kernel.Bind<IEmployeesRepository>().To<EmployeeRepository>();
            kernel.Bind<IValidationRepository>().To<ValidationRepository>();
            kernel.Bind<IAccountRepository>().To<AccountRepository>();

            return kernel;
        }
    }
}