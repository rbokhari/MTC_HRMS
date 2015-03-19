using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.Inventory;
using MTCHRMS.EntityFramework;

namespace MTCHRMS.DC
{
    public class ItemsRepository : IItemsRepository
    {
        DbEntityContext _ctx;

        public ItemsRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<IQueryable<Item>> GetItems()
        {
            return await Task.Run(() => 
                _ctx.Items
                .Include(c=>c.TypeDetail)
                .Include(d=>d.CategoryDetail)
                .Include(e=>e.StoreLocation));
        }

        public IQueryable<Item> GetItem(int id)
        {
            return _ctx.Items.Where(r => r.ItemId == id)
                .Include(c => c.TypeDetail)
                .Include(d => d.CategoryDetail)
                .Include(e => e.StoreLocation);

        }

        public bool Save()
        {
            return _ctx.SaveChanges() > 0;
        }

        public bool AddItem(Item newItem)
        {
            try
            {
                //var itemDept = new ItemDepartment()
                //{
                //    DepartmentId = 5,
                //    CreatedBy = newItem.CreatedBy,
                //    CreatedOn = DateTime.Now
                //};

                //var itemDept1 = new ItemDepartment()
                //{
                //    DepartmentId = 6,
                //    CreatedBy = newItem.CreatedBy,
                //    CreatedOn = DateTime.Now
                //};

                for (int i = 0; i < 1; i++)
                {
                    newItem.ItemDepartments.Add(new ItemDepartment(){DepartmentId = 6, CreatedBy = 1, CreatedOn = DateTime.Now});
                }


                //newItem.ItemDepartments.Add(itemDept);
                //newItem.ItemDepartments.Add(itemDept1);


                _ctx.Items.Add(newItem);


                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }

        }

        public bool UpdateItem(Item updateItem)
        {
            try
            {
                //_ctx.Departments.Attach(_ctx.Departments.Single(r => r.Id == department.Id));
                _ctx.Entry(updateItem).State = EntityState.Modified;
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
