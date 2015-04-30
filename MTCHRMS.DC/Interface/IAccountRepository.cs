using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Security;

namespace MTCHRMS.DC
{
    public interface IAccountRepository
    {
        Task<IQueryable<AC_User>> GetUsers();
        AC_User GetUser(int id);

        AC_User GetUserByUserName(string userName);

        bool AddUser(AC_User newUser);
        bool UpdateUser(AC_User updateUser);


        Task<IQueryable<AC_Module>> GetModules();
        AC_Module GetModule(int id);
        bool AddModule(AC_Module newModule);


        Task<IQueryable<AC_Role>> GetRoles();
        AC_Role GetRole(int id);
        bool AddRole(AC_Role newRole);
        bool UpdateRole(AC_Role updateRole);


        bool Save();

    }
}
