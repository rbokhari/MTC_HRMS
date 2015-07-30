using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.GlobalVariables;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.DC.Interface
{
    public interface IStoragePathRepository
    {
        Task<StoragePath> GetActiveStoragePathByModuleId(ApplicationPreferences.Modules module);
    }
}
