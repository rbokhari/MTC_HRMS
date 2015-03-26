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
                    .Include(c => c.TypeDetail)
                    .Include(d => d.CategoryDetail)
                    .Include(e => e.StoreLocation)
                    .Include(f => f.TechnicianType)
                    .Include(g => g.ItemDepartments.Select(i => i.DepartmentDetail))
                    .Include(h => h.ItemYears.Select(j => j.YearDetail))
                    .Include(q=>q.ItemSuppliers.Select(i=>i.SupplierDetail)));
        }

        public IQueryable<Item> GetItemDetail(int id)
        {
            return _ctx.Items.Where(r => r.ItemId == id)
                .Include(c => c.TypeDetail)
                .Include(d => d.CategoryDetail)
                .Include(e => e.StoreLocation)
                .Include(f => f.TechnicianType)
                .Include(g => g.ItemDepartments.Select(i => i.DepartmentDetail))
                .Include(h => h.ItemYears.Select(j => j.YearDetail))
                .Include(q => q.ItemSuppliers.Select(i => i.SupplierDetail));

        }

        public Item GetItem(int id)
        {
            return _ctx.Items.Single(r => r.ItemId == id);
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

        public bool UpdateItemImage(ref Item item)
        {
            //var employee = GetEmployee(id);

            //employee.EmpPicture = ms.ToArray();
            //employee.EmpPicture = imgFile.ToArray();
            _ctx.Entry(item).State = EntityState.Modified;

            return true;

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


        public bool AddItemDepartment(ItemDepartment newItemDepartment)
        {
            try
            {
                _ctx.ItemDepartments.Add(newItemDepartment);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }

        }

        public bool UpdateItemDepartment(ItemDepartment updateItemDepartment)
        {
            try
            {
                updateItemDepartment.ItemId = GetItem(updateItemDepartment.ItemId).ItemId;

                _ctx.Entry(updateItemDepartment).State = EntityState.Modified;
                return true;

            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool DeleteItemDepartment(ItemDepartment deleteItemDepartment)
        {
            try
            {
                var department = _ctx.ItemDepartments.Single(i => i.ItemDepartmentId == deleteItemDepartment.ItemDepartmentId);

                _ctx.ItemDepartments.Remove(department);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }

        }

        public bool AddItemYear(ItemYear newItemYear)
        {
            try
            {
                _ctx.ItemYears.Add(newItemYear);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }

        }

        public bool UpdateItemYear(ItemYear updateItemYear)
        {
            try
            {
                updateItemYear.ItemId = GetItem(updateItemYear.ItemId).ItemId;

                _ctx.Entry(updateItemYear).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool DeleteItemYear(ItemYear deleteItemYear)
        {
            try
            {
                var year = _ctx.ItemYears.Single(i => i.ItemYearId == deleteItemYear.ItemYearId);
                _ctx.ItemYears.Remove(year);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public IQueryable<ItemDepartment> GetItemDepartment(int itemDepartmentId)
        {
            return 
                _ctx.ItemDepartments
                .Where(r => r.ItemDepartmentId == itemDepartmentId)
                .Include(b=>b.DepartmentDetail);
        }

        public IQueryable<ItemYear> GetItemYear(int itemYearId)
        {
            return _ctx.ItemYears
                .Where(r => r.ItemYearId == itemYearId)
                .Include(b=>b.YearDetail);
        }


        public bool CheckItemDepartmentDuplicate(int itemDepartmentId, int departmentId)
        {
            if (itemDepartmentId == 0)
            {
                return _ctx.ItemDepartments.Any(r => r.DepartmentId == departmentId);
            }

            return
                _ctx.ItemDepartments.Any(
                    r => r.ItemDepartmentId == itemDepartmentId && r.DepartmentId == departmentId);
        }

        public bool CheckItemYearDuplicate(int itemYearId, int yearId)
        {
            if (itemYearId == 0)
            {
                return _ctx.ItemYears.Any(r => r.YearId== yearId);
            }

            return
                _ctx.ItemYears.Any(
                    r => r.ItemYearId == itemYearId && r.YearId == yearId);
        }




        public bool CheckItemSupplierDuplicate(int itemSupplierId, int supplierId)
        {
            if (itemSupplierId== 0)
            {
                return _ctx.ItemSuppliers.Any(r => r.SupplierId== supplierId);
            }

            return
                _ctx.ItemSuppliers.Any(
                    r => r.ItemSupplierId == itemSupplierId && r.SupplierId == supplierId);

        }

        public bool AddItemSupplier(ItemSupplier newItemSupplier)
        {
            try
            {
                _ctx.ItemSuppliers.Add(newItemSupplier);
                return true;
            }
            catch (Exception)
            {
                //TODO log this error
                return false;
            }
        }

        public bool UpdateItemSupplier(ItemSupplier updateItemSupplier)
        {
            try
            {
                updateItemSupplier.ItemId = GetItem(updateItemSupplier.ItemId).ItemId;

                _ctx.Entry(updateItemSupplier).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool DeleteItemSupplier(ItemSupplier deleteItemSupplier)
        {
            try
            {
                var supplier = _ctx.ItemSuppliers.Single(i => i.ItemSupplierId == deleteItemSupplier.ItemSupplierId);
                _ctx.ItemSuppliers.Remove(supplier);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }


        public IQueryable<ItemSupplier> GetItemSupplier(int itemSupplierId)
        {
            return
                _ctx.ItemSuppliers
                    .Where(r => r.ItemSupplierId == itemSupplierId)
                    .Include(b => b.SupplierDetail);
        }
    }
}

