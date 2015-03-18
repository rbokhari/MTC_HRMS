using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC
{
    public interface ISuppliersRepository
    {
        Task<IQueryable<Supplier>> GetSuppliers();

        //IQueryable<Department> GetDepartments();
        Supplier GetSupplier(int id);

        IQueryable<Supplier> GetSupplierDetail(int id);

        bool Save();

        bool AddSupplier(Supplier newSupplier);
        bool UpdateSupplier(Supplier updateSupplier);

        bool AddSupplierContact(SupplierContactPerson newContactPerson);

        bool UpdateSupplierContact(SupplierContactPerson updateContactPerson);

        bool AddSupplierContract(SupplierContract newContract);

        bool UpdateSupplierContract(SupplierContract updateContract);

    }
}
