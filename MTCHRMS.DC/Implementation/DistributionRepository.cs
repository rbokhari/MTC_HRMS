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
            return await Task.Run(() =>
                _ctx.DistributionItems
                    .Include(c=>c.ItemDetail)
                    
                    .Where(c=>c.ItemStockSerialId == id)
                    .Select(x => new ItemDistributionSerialModel
                    {
                        ItemId = x.ItemId,
                        ItemCode = x.ItemDetail.ItemCode,
                        ItemName = x.ItemDetail.ItemName,
                        ItemPicture = x.ItemDetail.ItemPicture,
                        StockSerialNo = ""
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
