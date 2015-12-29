using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.GlobalVariables;
using MTCHRMS.DC.Interface;
using MTCHRMS.EntityFramework.Inventory;
using MTCHRMS.EntityFramework;
using MTC.Models.Inventory;

namespace MTCHRMS.DC.Implementation
{
    public class DistributionRepository : IDistributionRepository
    {
        DbEntityContext _ctx;

        public DistributionRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }


        public async Task<IQueryable<ItemDistributionSerialModel>> GetDistributions()
        {
            return await Task.Run(() => 
                _ctx.Items
                    .Select(x=> new ItemDistributionSerialModel
                    {
                        ItemId = x.ItemId,
                        ItemCode = x.ItemCode,
                        ItemName = x.ItemName,
                        ItemPicture = x.ItemPicture
                    })
                );
        }

        public async Task<ItemDistributionSerialModel> GetDistribution(int id)
        {
            var stockAdd = await _ctx.ItemStockSerials
                    .SingleOrDefaultAsync(h => h.ItemStockSerialId == id);

            var item = await _ctx.Items.FirstOrDefaultAsync(c => c.ItemId == stockAdd.ItemId);

            return await Task.Run(() =>
                _ctx.ItemStockAdds
                    //.Include(c => c.DistributionItems.Select(d => d.ItemDetail))
                    //.Include(c => c.DistributionItems.Select(e => e.LocationDetail))
                    //.SingleOrDefault(c => c.ItemStockSerialId == id)
                    .Where(c=>c.ItemStockSerials.Any(b=>b.ItemStockSerialId == id))
                    .Select(x => new ItemDistributionSerialModel
                    {
                        ItemId = x.ItemStockSerials.FirstOrDefault().ItemId,
                        ItemCode = item.ItemCode, // x.ItemStockSerials.FirstOrDefault().item //x.DistributionItems.FirstOrDefault().ItemDetail.ItemCode,
                        ItemName = item.ItemName,
                        StockComputerCode = x.ComputerCode,
                        StockReceiptNo = x.BillNo,
                        DeliveryDate = x.DeliveryDate,
                        WarrantyStateDate = x.WarrantyStart,
                        WarrantyEndDate = x.WarrantyEnd,
                        Status = x.ItemStockSerials.FirstOrDefault().ItemStockStatusDetail.NameEn, //  stockSerial.ItemStockStatusDetail.NameEn,
                        StockSerialNo = x.ItemStockSerials.FirstOrDefault().SerialNo,
                        ItemPicture = item.ItemPicture
                    }).FirstOrDefault()
                );
        }

        public async Task<List<ItemDistributionSerialDetailModel>> GetDistributionHierarchy(int id)
        {
            var distribution = new List<ItemDistributionSerialDetailModel>();

            var itemDist = await GetDistribution(id);

            distribution.Add(new ItemDistributionSerialDetailModel
            {
                SerialDetailId = -101,
                DepartmentId = 1,
                DepartmentName = "1",
                EmployeeId= 1,
                EmployeeName= "Warranty Start Date",
                AssignedDate = itemDist.WarrantyStateDate ?? new DateTime(),
                ContentClass = "start"

            });

            distribution.AddRange(await _ctx.Distributions
                    .Where(c => c.DistributionItems.Any(b => b.ItemStockSerialId == id))
                    .Select(x => new ItemDistributionSerialDetailModel
                    {
                        SerialDetailId = x.DistributionId,
                        DepartmentId = x.DepartmentId,
                        DepartmentName = x.DepartmentDetail.DepartmentName,
                        EmployeeId = x.EmployeeId,
                        EmployeeName = x.EmployeeDetail.EmployeeName,
                        EmployeePicture = x.EmployeeDetail.EmpPicture,
                        AssignedDate = x.DistributionDate,
                        LocationId = x.DistributionItems.FirstOrDefault().LocationId,
                        LocationName = x.DistributionItems.FirstOrDefault().LocationDetail.StoreName,
                        ContentClass = "assign"
                    }).ToListAsync<ItemDistributionSerialDetailModel>());


            distribution.Add(new ItemDistributionSerialDetailModel
            {
                SerialDetailId = -102,
                DepartmentId = 1,
                DepartmentName = "1",
                EmployeeId = 1,
                EmployeeName = "Warranty End Date",
                AssignedDate = itemDist.WarrantyEndDate ?? new DateTime(),
                ContentClass = "end"

            });

            return await Task.Run(()=> distribution );
        }

        public bool Save()
        {
            try
            {
                return _ctx.SaveChanges() > 0;
            }
            catch (Exception)
            {
                // TODO log this error
                return false;
            }
        }

        public bool AddDistribution(Distribution newDistribution, ICollection<DistributionItem> newDistributionItems)
        {
            try
            {
                newDistribution.ComputerCode = new MTCRepository(_ctx).SetAutoNumberFormat((int)ApplicationPreferences.ScreenId.Inventory_Distribution_Add);

                //newDepartment.CreatedBy = 1;
                //newDepartment.CreatedOn = DateTime.UtcNow;
                _ctx.Distributions.Add(newDistribution);

                if (_ctx.SaveChanges() > 0)
                {
                    foreach (var dItems in newDistributionItems)
                    {
                        dItems.DistributionId = newDistribution.DistributionId;

                        // Setting item serial to Assigned status
                        var itemSerial = new ItemsRepository(_ctx).GetItemStockSerialsBySerialId(dItems.ItemStockSerialId);
                        itemSerial.StatusId = (Int32)ApplicationPreferences.Validation_Details.ITEM_STOCK_STATUS_ASSIGNED;
                        _ctx.Entry(itemSerial).State = EntityState.Modified;
                        //---------------------------------------

                        _ctx.DistributionItems.Add(dItems);
                    }

                    if (_ctx.SaveChanges() > 0) { }
                }
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public async Task<IQueryable<ItemDistributionSerialModel>> GetDistributionBySerial(string serialNo)
        {
            return await Task.Run(() =>

                _ctx.ItemStockSerials
                    .Where(g=>g.SerialNo.Contains(serialNo))
                    .Join(_ctx.Items, 
                        c=>c.ItemId, 
                        e=>e.ItemId, 
                        (c, e)=> new ItemDistributionSerialModel
                    {
                        ItemId = c.ItemId,
                        StockSerialId = c.ItemStockSerialId,
                        StockAddId = c.ItemStockAddId,
                        ItemCode = e.ItemCode,
                        ItemName = e.ItemName,
                        StockSerialNo = c.SerialNo,
                        StockComputerCode = e.NatoNo,
                        ItemPicture = e.ItemPicture
                        
                        
                    })
                    
            );
        }
    }


}
