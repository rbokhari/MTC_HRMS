using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.GlobalVariables;
using MTCHRMS.DC.Implementation;
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
                    //.Include(f => f.TechnicianType)
                    //.Include(g => g.ItemDepartments.Select(i => i.DepartmentDetail))
                    //.Include(h => h.ItemYears.Select(j => j.YearDetail))
                    //.Include(j=>j.ItemManufacturers.Select(x=>x.ManufacturerDetail))
                    //.Include(q=>q.ItemSuppliers.Select(i=>i.SupplierDetail))
                    );
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
                .Include(q => q.ItemSuppliers.Select(i => i.SupplierDetail))
                .Include(c=>c.ItemAttachments)
                .Include(c => c.ItemStockAdds.Select(i=>i.ItemStockSerials.Select(j=>j.ItemStockStatusDetail)))
                .Include(c => c.ItemStockAdds.Select(i => i.ItemStockSerials.Select(j => j.DistributionItem)))
                .Include(c => c.ItemStockAdds.Select(i => i.SupplierDetail))
                .Include(j => j.ItemManufacturers.Select(x => x.ManufacturerDetail).Select(o=>o.CountryDetail));

        }

        public Item GetItem(int id)
        {
            return _ctx.Items.Single(r => r.ItemId == id);
        }

        public bool Save()
        {
           return _ctx.SaveChanges() > 0;
        }

        public Int32 CheckItemDuplicate(Item newItem)
        {
            Item item;
            if (newItem.ItemId == 0)
            {
                item = _ctx.Items.SingleOrDefault(r => String.Equals(r.ItemCode.ToLower(), newItem.ItemCode.ToLower()) || String.Equals(r.SerialNo.ToLower(), newItem.SerialNo.ToLower()));
            }
            else
            {
                item =
                    _ctx.Items.SingleOrDefault(
                        r => r.ItemId != newItem.ItemId && (String.Equals(r.ItemCode.ToLower(), newItem.ItemCode.ToLower()) || String.Equals(r.SerialNo.ToLower(), newItem.SerialNo.ToLower())));
            }

            if (item != null) return item.ItemId;

            return 0;
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

                //for (int i = 0; i < 1; i++)
                //{
                //    newItem.ItemDepartments.Add(new ItemDepartment(){DepartmentId = 6, CreatedBy = 1, CreatedOn = DateTime.Now});
                //}


                //newItem.ItemDepartments.Add(itemDept);
                //newItem.ItemDepartments.Add(itemDept1);

                //if (CheckItemDuplicate())


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
                var itemFetch = GetItem(updateItem.ItemId);
                var attachedItem = _ctx.Entry(itemFetch);
                attachedItem.CurrentValues.SetValues(updateItem);
                //_ctx.Entry(updateItem).State = EntityState.Modified;
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

        public async Task<ItemAttachment> GetAttachment(int attachmentId)
        {
            return await _ctx.ItemAttachments.SingleOrDefaultAsync(c => c.AttachmentId == attachmentId);
        }


        public bool AddItemAttachment(ref ItemAttachment newItemAttachment)
        {
            try
            {
                newItemAttachment.StorageId =
                    _ctx.StoragePaths.SingleOrDefault(
                        c => c.ModuleId == (Int32) ApplicationPreferences.Modules.INVENTORY).StorageId;

                _ctx.ItemAttachments.Add(newItemAttachment);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }

        }


        public bool DeleteItemAttachment(int id)
        {
            try
            {
                var attachment = _ctx.ItemAttachments.Single(i => i.AttachmentId == id);

                _ctx.ItemAttachments.Remove(attachment);
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


        public bool CheckItemDepartmentDuplicate(int itemId, int itemDepartmentId, int departmentId)
        {
            if (itemDepartmentId == 0)
            {
                return _ctx.ItemDepartments.Any(r => r.ItemId == itemId && r.DepartmentId == departmentId);
            }

            return
                _ctx.ItemDepartments.Any(
                    r => r.ItemId == itemId && r.ItemDepartmentId == itemDepartmentId && r.DepartmentId == departmentId);
        }

        public bool CheckItemYearDuplicate(int itemId, int itemYearId, int yearId)
        {
            if (itemYearId == 0)
            {
                return _ctx.ItemYears.Any(r => r.ItemId == itemId && r.YearId == yearId);
            }

            return
                _ctx.ItemYears.Any(
                    r => r.ItemId == itemId && r.ItemYearId == itemYearId && r.YearId == yearId);
        }

        public bool CheckItemSupplierDuplicate(int itemId, int itemSupplierId, int supplierId)
        {
            if (itemSupplierId== 0)
            {
                return _ctx.ItemSuppliers.Any(r => r.ItemId == itemId && r.SupplierId== supplierId);
            }

            return
                _ctx.ItemSuppliers.Any(
                    r => r.ItemId == itemId && r.ItemSupplierId == itemSupplierId && r.SupplierId == supplierId);

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


        public bool AddItemManufacturer(ItemManufacturer newItemManufacturer)
        {
            try
            {
                _ctx.ItemManufacturers.Add(newItemManufacturer);
                return true;
            }
            catch (Exception)
            {
                //TODO log this error
                return false;
            }
        }

        public bool UpdateItemManufacturer(ItemManufacturer updateItemManufacturer)
        {
            try
            {
                updateItemManufacturer.ItemId = GetItem(updateItemManufacturer.ItemId).ItemId;

                _ctx.Entry(updateItemManufacturer).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool DeleteItemManufacturer(ItemManufacturer deleteItemManufacturer)
        {
            try
            {
                var manufacturer = _ctx.ItemManufacturers.Single(i => i.ItemManufacturerId == deleteItemManufacturer.ItemManufacturerId);
                _ctx.ItemManufacturers.Remove(manufacturer);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }


        public IQueryable<ItemManufacturer> GetItemManufactuer(int itemManufacturerId)
        {
            return
                _ctx.ItemManufacturers
                    .Where(r => r.ItemManufacturerId == itemManufacturerId)
                    .Include(b => b.ManufacturerDetail.CountryDetail);
        }

        public bool CheckItemManufacturerDuplicate(int itemId, int itemManufacturerId, int manufacturerId)
        {
            if (itemManufacturerId == 0)
            {
                return _ctx.ItemManufacturers.Any(r => r.ItemId == itemId && r.ManufacturerId == manufacturerId);
            }

            return
                _ctx.ItemManufacturers.Any(
                    r =>
                        r.ItemId == itemId && r.ItemManufacturerId == itemManufacturerId &&
                        r.ManufacturerId == manufacturerId);

        }



        public async Task<List<Item>> GetItemSearch(Item item)
        {
            try
            {
                var items = _ctx.Items
                    .Include(c => c.TypeDetail)
                    .Include(d => d.CategoryDetail)
                    .Include(e => e.StoreLocation)
                    .Include(f => f.TechnicianType)
                    .Include(f=>f.ItemDepartments);



                if (item.Condition == 0)
                {
                    items = items
                        .Where(c =>
                            c.ItemCode.ToLower().Contains(item.ItemCode.ToLower()) ||
                            c.ItemName.ToLower().Contains(item.ItemName.ToLower()) ||
                            c.TypeId == item.TypeId ||
                            c.CategoryId == item.CategoryId ||
                            c.StoreId == item.StoreId ||
                            c.ItemDepartments.Any(b => b.DepartmentId == item.DepartmentId) ||
                            c.ItemSuppliers.Any(b => b.SupplierId == item.SupplierId) ||
                            c.ItemManufacturers.Any(b => b.ManufacturerId == item.ManufacturerId));

                }
                else
                {
                    if (!string.IsNullOrEmpty(item.ItemCode))
                    {
                        items = items.Where(c => c.ItemCode.ToLower().Contains(item.ItemCode.ToLower()));
                    }

                    if (!string.IsNullOrEmpty(item.ItemName))
                    {
                        items = items.Where(c => c.ItemName.ToLower().Contains(item.ItemName.ToLower()));
                    }

                    if (item.TypeId != 0)
                    {
                        items = items.Where(c => c.TypeId == item.TypeId);
                    }

                    if (item.CategoryId != 0)
                    {
                        items = items.Where(c => c.CategoryId == item.CategoryId);
                    }

                    if (item.StoreId != 0)
                    {
                        items = items.Where(c => c.StoreId == item.StoreId);
                    }

                    if (item.DepartmentId != 0)
                    {
                        items = items.Where(c => c.ItemDepartments.Any(b => b.DepartmentId == item.DepartmentId));
                    }

                    if (item.SupplierId != 0)
                    {
                        items = items.Where(c => c.ItemSuppliers.Any(b => b.SupplierId == item.SupplierId));
                    }

                    if (item.ManufacturerId != 0)
                    {
                        items = items.Where(c => c.ItemManufacturers.Any(b => b.ManufacturerId == item.ManufacturerId));
                    }
                }

                return await items.ToListAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public IQueryable<ItemDepartment> GetItemDepartments()
        {
            return _ctx.ItemDepartments;
        }

        public IQueryable<ItemYear> GetItemYears()
        {
            return _ctx.ItemYears;
        }

        public IQueryable<ItemSupplier> GetItemSuppliers()
        {
            return _ctx.ItemSuppliers;
        }

        public IQueryable<ItemManufacturer> GetItemManufactuers()
        {
            return _ctx.ItemManufacturers;
        }



        public bool AddItemStock(ItemStockAdd newItemStock)
        {
            try
            {
                newItemStock.ComputerCode = new MTCRepository(_ctx).SetAutoNumberFormat((int)ApplicationPreferences.ScreenId.Inventory_Stock_Add);

                _ctx.ItemStockAdds.Add(newItemStock);

                var item = GetItem(newItemStock.ItemId);

                item.ItemStock += newItemStock.Stock;

                _ctx.Entry(item).State = EntityState.Modified;

                if (_ctx.SaveChanges() > 0)
                {
                    var stockSerial = new ItemStockSerial()
                    {
                        ItemStockAddId = newItemStock.ItemStockAddId,
                        ItemId = newItemStock.ItemId,
                        SerialNo = string.Empty,
                        StatusId = (Int32)ApplicationPreferences.Validation_Details.ITEM_STOCK_STATUS_STORE,
                        CreatedBy = newItemStock.CreatedBy,
                        CreatedOn = DateTime.Now
                    };

                    //IList<ItemStockSerial> serials = new List<ItemStockSerial>(newItemStock.Stock);

                    for (int i = 0; i < newItemStock.Stock; i++)
                    {
                        //stockSerial.StatusId = i;
                        //serials.Add(stockSerial);
                        //_ctx.ItemStockSerials.Add(stockSerial);
                        _ctx.ItemStockSerials.Add(stockSerial);
                        if (_ctx.SaveChanges() > 0) { }
                    }

                    //_ctx.ItemStockSerials.AddRange(serials);

                    //return _ctx.SaveChanges() > 0;
                }

                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }


        public bool UpdateItemSerial(ItemStockSerial newItemSerials)
        {
            try
            {
                _ctx.Entry(newItemSerials).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateItemSerials(List<ItemStockSerial> newItemSerials)
        {
            try
            {
                _ctx.Entry(newItemSerials).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ItemStockSerial GetItemStockSerialsBySerialId(int serialId)
        {
            return _ctx.ItemStockSerials.SingleOrDefault(c => c.ItemStockSerialId == serialId);
        }

        public async Task<IQueryable<ItemStockSerial>> GetItemStockSerialsByItemId(int itemId)
        {
            return await Task.Run(() => _ctx.ItemStockSerials.Where(c => c.ItemId == itemId).Include(c => c.ItemStockStatusDetail));
        }

        public async Task<IQueryable<ItemStockSerial>> GetItemStockSerialsByStockAddId(int stockAddId)
        {
            return await Task.Run(() => _ctx.ItemStockSerials.Where(c => c.ItemStockAddId == stockAddId));
        }


        public bool AddItemStockSerials(List<ItemStockSerial> itemStockSerials)
        {
            try
            {
                foreach (var itemStockSerial in itemStockSerials)
                {
                    _ctx.Entry(itemStockSerial).State = EntityState.Modified;
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }





        public async Task<ItemStockAdd> GetItemStock(int id)
        {
            return await Task.Run(() =>
                _ctx
                    .ItemStockAdds
                    .SingleOrDefaultAsync(c => c.ItemStockAddId == id));
        }


        public bool UpdateItemStock(ItemStockAdd updateItemStock)
        {
            try
            {
                _ctx.Entry(updateItemStock).State = EntityState.Modified;

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        public async Task<IQueryable<Supplier>> GetSuppliersByItemId(int id)
        {
            return await Task.Run(() => 
                _ctx.ItemSuppliers
                .Where(c => c.ItemId == id)
                .Include(c => c.SupplierDetail)
                .Select(x => x.SupplierDetail)
                .OrderBy(x => x.SupplierName));
        }
    }
}

