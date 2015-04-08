using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Security;

namespace MTCHRMS.DC
{
    public class AccountRepository : IAccountRepository
    {
        private DbEntityContext _ctx;

        public AccountRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<AC_User>> GetUsers()
        {
            return await Task.Run(() => _ctx.Users);
        }

        public AC_User GetUser(int id)
        {
            return _ctx.Users.Single(r => r.EmployeeId == id);
        }

        public bool AddUser(AC_User newUser)
        {
            try
            {
                newUser.CreatedBy = 1;
                newUser.CreatedOn = DateTime.UtcNow;
                _ctx.Users.Add(newUser);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateUser(AC_User updateUser)
        {
            throw new NotImplementedException();
        }

        public async Task<IQueryable<AC_Module>> GetModules()
        {
            return await Task.Run(() => _ctx.Modules);
        }

        public AC_Module GetModule(int id)
        {
            return _ctx.Modules.Single(r => r.ModuleId == id);
        }

        public bool AddModule(AC_Module newModule)
        {
            try
            {
                newModule.CreatedBy = 1;
                newModule.CreatedOn = DateTime.UtcNow;
                _ctx.Modules.Add(newModule);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public async Task<IQueryable<AC_Role>> GetRoles()
        {
            return await Task.Run(() => _ctx.Roles);
        }

        public AC_Role GetRole(int id)
        {
            return _ctx.Roles.Single(r => r.RoleId == id);
        }

        public bool AddRole(AC_Role newRole)
        {
            try
            {
                newRole.CreatedBy = 1;
                newRole.CreatedOn = DateTime.UtcNow;
                _ctx.Roles.Add(newRole);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public bool UpdateRole(AC_Role updateRole)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        
    }
}
