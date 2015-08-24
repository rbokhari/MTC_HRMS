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
            var stockAdd = await _ctx.ItemStockAdds
                    .FirstOrDefaultAsync(g => g.ItemStockAddId == _ctx.ItemStockSerials.FirstOrDefault(h => h.ItemStockSerialId == id).ItemStockAddId);

            var stockSerial = await _ctx.ItemStockSerials
                    .Include(a=>a.ItemStockStatusDetail).FirstOrDefaultAsync(g => g.ItemStockSerialId == id);

            return await Task.Run(() =>
                _ctx.Distributions
                    .Include(c => c.DistributionItems.Select(d => d.ItemDetail))
                    .Include(c => c.DistributionItems.Select(e => e.LocationDetail))
                    .Where(c => c.DistributionItems.Any(f=>f.ItemStockSerialId == id))
                    .Select(x => new ItemDistributionSerialModel
                    {
                        ItemId = x.DistributionItems.FirstOrDefault().ItemId,
                        ItemCode = x.DistributionItems.FirstOrDefault().ItemDetail.ItemCode,
                        ItemName = x.DistributionItems.FirstOrDefault().ItemDetail.ItemName,
                        StockComputerCode = stockAdd.ComputerCode,
                        StockReceiptNo = stockAdd.BillNo,
                        DeliveryDate = stockAdd.DeliveryDate,
                        WarrantyStateDAte = stockAdd.WarrantyStart,
                        WarrantyEndDate = stockAdd.WarrantyEnd,
                        Status =  stockSerial.ItemStockStatusDetail.NameEn,
                        StockSerialNo = stockSerial.SerialNo,
                        ItemPicture = x.DistributionItems.FirstOrDefault().ItemDetail.ItemPicture
                    }).FirstOrDefaultAsync()
                );
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
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }
    }


}
