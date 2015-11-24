using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using MTC.GlobalVariables;
using MTCHRMS.EntityFramework.General;
//using MTCHRMS.EntityFramework.Security;
using MTCHRMS.EntityFramework.Security;

namespace MTCHRMS.EntityFramework
{
    public class HRMSMigrationConfiguration : DbMigrationsConfiguration<DbEntityContext>
    {
        public HRMSMigrationConfiguration()
        {
            try
            {
                this.AutomaticMigrationDataLossAllowed = true;  //Dangerous using carefully, read on internet
                this.AutomaticMigrationsEnabled = true;

            }
            catch (Exception exception)
            {
                
            }
        }

        protected override void Seed(DbEntityContext context)
        {
            base.Seed(context);

#if DEBUG
            if (!context.Departments.Any())
            {

                var dept = new Department()
                {
                    DepartmentCode = "001",
                    DepartmentName = "Humain Resource",
                    StatusId = 1,
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    context.Departments.Add(dept);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }


            //------------- Module Defaults Starts --------------------
            if (!context.Modules.Any())
            {
                var module = new AC_Module()
                {
                    ModuleName = "HRMS",
                    ModuleNameAr = "شوؤن الموظفين",
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    context.Modules.Add(module);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }

            //------------- Module Defaults Ends --------------------


            //------------- Role Defaults Starts --------------------
            if (!context.Roles.Any())
            {
                var role = new AC_Role()
                {
                    RoleName = "Admin",
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    context.Roles.Add(role);
                }
                catch (Exception exception)
                {
                    var msg = exception.Message;
                }
            }
            //------------- Module Defaults Ends --------------------


            //------------- Users Defaults Starts --------------------
            if (!context.Users.Any())
            {
                var user = new AC_User()
                {
                    UserName = "syed.rahman",
                    RoleId = 1,
                    ModuleId = 1,
                    DepartmentId = 1,
                    CurrLang = "en-us",
                    CreatedBy = 1,
                    CreatedOn = DateTime.Now
                };

                try
                {
                    context.Users.Add(user);
                }
                catch (Exception exception)
                {
                    var msg = exception.Message;
                }
            }
            //------------- Users Defaults Ends --------------------

            if (!context.AutoSettings.Any())
            {
                var auto = new AutoSetting()
                {
                    ModuleId = (int) ApplicationPreferences.Modules.INVENTORY,
                    ScreenId = (int) ApplicationPreferences.ScreenId.Inventory_Stock_Add,
                    CodePrefix = "INS",
                    CodeLength = 6,
                    IsAutoNumber = 1,
                    IsEditable = 0,
                    StartValue = 1,
                    CurrentValue = 1
                };

                try
                {
                    context.AutoSettings.Add(auto);
                }
                catch (Exception ex)
                {
                    var msg = ex.Message;
                }
            }
#endif

        }
    }
}
