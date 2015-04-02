using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC

{
    public class SupplierRepository: ISuppliersRepository
    {
        DbEntityContext _ctx;

        public SupplierRepository(DbEntityContext ctx)
        {
            // here we can pass this as ref object, so that it can use everytime, like, DbEntityContext ctxContext
            // use then ninject to create constructor instance auto and reference it if available.

            //_ctx = new DbEntityContext();
            _ctx = ctx;
        }

        //public IQueryable<Department> GetDepartments()
        //{
        //    //System.Threading.Thread.Sleep(100);
        //    return _ctx.Departments;
        //}

        public async Task<IQueryable<Supplier>> GetSuppliers()
        {
            //System.Threading.Thread.Sleep(100);
            return await Task.Run(() => _ctx.Suppliers.Include(a=>a.CountryDetail));
        }

        public Supplier GetSupplier(int id)
        {
            return _ctx.Suppliers.Single(r=>r.SupplierId == id);
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // TODO log this error
                return false;
            }
        }

        public bool AddSupplier(Supplier newSupplier)
        {
            try
            {
                //newDepartment.CreatedBy = 1;
                //newDepartment.CreatedOn = DateTime.UtcNow;
                _ctx.Suppliers.Add(newSupplier);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateSupplier(Supplier updateSupplier)
        {
            try
            {
                //_ctx.Departments.Attach(_ctx.Departments.Single(r => r.Id == department.Id));
                _ctx.Entry(updateSupplier).State = EntityState.Modified;
                return true;

            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }


        public bool AddSupplierContact(SupplierContactPerson newContactPerson)
        {
            try
            {
                _ctx.SupplierContactPersons.Add(newContactPerson);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }

        }

        public bool AddSupplierContract(SupplierContract newContract)
        {
            try
            {
                _ctx.SupplierContracts.Add(newContract);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }

        }


        public bool UpdateSupplierContact(SupplierContactPerson updateContactPerson)
        {
            try
            {
                updateContactPerson.SupplierId = GetSupplier(updateContactPerson.SupplierId).SupplierId;
                _ctx.Entry(updateContactPerson).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateSupplierContract(SupplierContract updateContract)
        {
            try
            {
                updateContract.SupplierId = GetSupplier(updateContract.SupplierId).SupplierId;
                _ctx.Entry(updateContract).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public IQueryable<Supplier> GetSupplierDetail(int id)
        {
            try
            {
                return _ctx.Suppliers.Where(r => r.SupplierId == id)
                    .Include(p => p.SupplierContactPersons)
                    .Include(v => v.SupplierContracts);

            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
