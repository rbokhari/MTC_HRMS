using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.Inventory;

namespace MTCHRMS.DC.Interface
{
    public interface IDistributionRepository
    {
        Task<IQueryable<Distribution>> GetDistributions();

        Task<Distribution> GetDistribution(int id);

        bool Save();

        bool AddDistribution(Distribution distribution, ICollection<DistributionItem> items);

    }
}
