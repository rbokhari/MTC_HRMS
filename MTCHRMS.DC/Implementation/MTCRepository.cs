using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.DC.Interface;
using MTCHRMS.EntityFramework;

namespace MTCHRMS.DC.Implementation
{
    public class MTCRepository : IMTCRepository
    {
        DbEntityContext _ctx;

        public MTCRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }



        public string SetAutoNumberFormat(int screenId)
        {
            var autoSetting = _ctx.AutoSettings.SingleOrDefault(c => c.ScreenId == screenId);
            string autoNumber;

            if (autoSetting != null)
            {
                autoNumber = autoSetting.CurrentValue.ToString();
                short autoLength = autoSetting.CodeLength;

                autoNumber = new string('0', autoLength - autoNumber.Length) + autoNumber;

                autoNumber = string.Format("{0}{1}", autoSetting.CodePrefix, autoNumber);

                autoSetting.CurrentValue += 1;

                _ctx.Entry(autoSetting).State = EntityState.Modified;
                if (_ctx.SaveChanges()>0)
                {
                    
                }

                return autoNumber;
            }
            return string.Empty;
        }
    }
}
