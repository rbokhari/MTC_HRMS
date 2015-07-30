using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.GlobalVariables;
using MTCHRMS.DC.Interface;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.DC.Implementation
{

    public class StoragePathRepository : IStoragePathRepository
    {

        DbEntityContext _ctx;

        public StoragePathRepository(DbEntityContext context)
        {
            _ctx = context;
        }

        public async Task<StoragePath> GetActiveStoragePathByModuleId(ApplicationPreferences.Modules module)
        {
            return await _ctx.StoragePaths.SingleOrDefaultAsync(c => c.ModuleId == (Int32) module);
        }
    }
}
