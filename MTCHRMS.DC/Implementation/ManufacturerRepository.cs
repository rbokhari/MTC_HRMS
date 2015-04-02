﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC

{
    public class ManufactuerRepository: IManufacturersRepository
    {
        DbEntityContext _ctx;

        public ManufactuerRepository(DbEntityContext ctx)
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

        public async Task<IQueryable<Manufacturer>> GetManufacturers()
        {
            //System.Threading.Thread.Sleep(100);
            return await Task.Run(() => _ctx.Manufacturers.Include(a=>a.CountryDetail));
        }

        public Manufacturer GetManufacturer(int id)
        {
            return _ctx.Manufacturers.Single(r=>r.ManufacturerId == id);
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

        public bool AddManufacturer(Manufacturer newManufacturer)
        {
            try
            {
                //newDepartment.CreatedBy = 1;
                //newDepartment.CreatedOn = DateTime.UtcNow;
                _ctx.Manufacturers.Add(newManufacturer);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateManufacturer(Manufacturer updateManufacturer)
        {
            try
            {
                //_ctx.Departments.Attach(_ctx.Departments.Single(r => r.Id == department.Id));
                _ctx.Entry(updateManufacturer).State = EntityState.Modified;
                return true;

            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }
    }
}
