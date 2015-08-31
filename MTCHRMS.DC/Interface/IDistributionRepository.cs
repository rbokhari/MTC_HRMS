using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.Models.Inventory;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC.Interface
{
    public interface IDistributionRepository
    {
        Task<IQueryable<ItemDistributionSerialModel>> GetDistributions();

        Task<ItemDistributionSerialModel> GetDistribution(int id);

        Task<IQueryable<ItemDistributionSerialModel>> GetDistributionBySerial(string serialNo);

        Task<List<ItemDistributionSerialDetailModel>> GetDistributionHierarchy(int id);

        bool Save();

        bool AddDistribution(Distribution distribution, ICollection<DistributionItem> items);

    }
}
