using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC
{
    public interface IStoreLocationRepository
    {
        Task<IQueryable<StoreLocation>> GetLocations();

        //IQueryable<Department> GetDepartments();
        StoreLocation GetLocation(int id);

        bool Save();

        bool AddLocation(StoreLocation newLocation);
        bool UpdateLocation(StoreLocation updateLocation);
    }
}
