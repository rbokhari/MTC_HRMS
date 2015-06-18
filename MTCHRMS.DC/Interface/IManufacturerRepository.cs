using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC
{
    public interface IManufacturersRepository
    {
        Task<IQueryable<Manufacturer>> GetManufacturers();

        //IQueryable<Department> GetDepartments();
        Manufacturer GetManufacturer(int id);

        IQueryable<Manufacturer> GetManufacturerDetail(int id);

        bool Save();

        bool AddManufacturer(Manufacturer newManufacturer);
        bool UpdateManufacturer(Manufacturer updateManufacturer);

        bool AddManufacturerContact(ManufacturerContactPerson newContactPerson);

        bool UpdateManufacturerContact(ManufacturerContactPerson updateContactPerson);

        bool AddManufacturerContract(ManufacturerContract newContract);

        bool UpdateManufacturerContract(ManufacturerContract updateContract);


    }
}
