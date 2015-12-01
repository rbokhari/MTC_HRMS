using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.Models.Common;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC.Interface.HRMS
{
    public interface IServicesRepository
    {
        bool Save();

        bool AddEmployeeApplyLeave(EmployeeLeave newLeave);

        Task<EmployeeLeave> GetEmployeeLeave(int id);

        Task<IList<NotificationModel>> GetEmployeeNotification(int id);
    }
}
