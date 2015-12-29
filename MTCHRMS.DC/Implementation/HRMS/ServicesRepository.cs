using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.GlobalVariables;
using MTC.Models.Common;
using MTCHRMS.DC.Interface.HRMS;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC.Implementation.HRMS
{
    public class ServicesRepository : IServicesRepository
    {
        DbEntityContext _ctx;

        public ServicesRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public bool AddEmployeeApplyLeave(EmployeeLeave newLeave)
        {
            try
            {
                var empLeaveYear = _ctx.EmployeeLeaveYears.FirstOrDefault(c => c.LeaveYearId == newLeave.LeaveYearId);
                if (newLeave.EmployeeLeaveId == 0)
                {
                    if (empLeaveYear != null)
                    {
                        if ((empLeaveYear.LeaveDays + empLeaveYear.TransferDays - empLeaveYear.DeductDays - empLeaveYear.AvailedDays) >= newLeave.LeaveDays)
                        {
                            _ctx.EmployeeLeaves.Add(newLeave);
                            return true;
                        }
                    }
                }else
                {
                    var empleaves = _ctx.EmployeeLeaves.FirstOrDefault(c => c.EmployeeLeaveId == newLeave.EmployeeLeaveId);
                    if (empleaves == null) return false;
                    switch (empleaves.StatusId)
                    {
                        // Approval from Alternate Employee
                        case (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_APPLIED:
                            empleaves.StatusId = (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_1;
                            empleaves.AlternateEmployeeComments = newLeave.AlternateEmployeeComments;
                            empleaves.AlternateEmployeeDecision = newLeave.AlternateEmployeeDecision;
                            empleaves.AlternateEmployeeDate = DateTime.Now;
                            break;
                        // Approval from Line Manager Employee
                        case (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_1:
                            empleaves.StatusId = (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_2;
                            empleaves.LineManagerComments = newLeave.LineManagerComments;
                            empleaves.LineManagerDecision = newLeave.LineManagerDecision;
                            empleaves.LineManagerDate = DateTime.Now;

                            empleaves.DepartmentManagerId = newLeave.DepartmentManagerId;
                            break;
                        // Approval from Department Manager Employee
                        case (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_2:
                            empleaves.StatusId = (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_3;
                            empleaves.DepartmentManagerComments = newLeave.DepartmentManagerComments;
                            empleaves.DepartmentManagerDecision = newLeave.DepartmentManagerDecision;
                            empleaves.DepartmentManagerDate = DateTime.Now;
                            break;
                        // Approval from HR Employee
                        case (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_3:
                            empleaves.StatusId = (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_4;
                            empleaves.HREmployeeId = newLeave.HREmployeeId;
                            empleaves.HREmployeeComments = newLeave.HREmployeeComments;
                            empleaves.HREmployeeDecision = newLeave.HREmployeeDecision;
                            empleaves.HREmployeeDate = DateTime.Now;

                            if (empLeaveYear != null)
                            {
                                empLeaveYear.AvailedDays += newLeave.LeaveDays;
                                _ctx.Entry(empLeaveYear).State = EntityState.Modified;
                            }
                            break;
                        default:
                            break;
                    }
                    _ctx.Entry(empleaves).State = EntityState.Modified;
                    return true;
                }
                return false;                
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
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

        public async Task<EmployeeLeave> GetEmployeeLeave(int id)
        {
            return await Task.Run(() =>
                _ctx.EmployeeLeaves
                    .Include(x=>x.EmployeeDetail)
                    .Include(x=>x.LeaveTypeDetail)
                    .Include(x=>x.AlternateEmployeeDetail)
                    .Include(x=>x.SupervisorEmployeeDetail)
                    .SingleOrDefaultAsync(x=>x.EmployeeLeaveId == id)
                    
            );
        }


        /// <summary>
        /// For Notification Services
        /// </summary>
        /// <param name="id">Current Login user Id</param>
        /// <returns></returns>
        public async Task<IList<NotificationModel>> GetEmployeeNotification(int id)
        {
            var userRepo = new AccountRepository(_ctx).GetUser(id);

            if (userRepo?.ModuleId == (Int32)ApplicationPreferences.Modules.HRMS && 
                userRepo.RoleId == (Int32)ApplicationPreferences.Account_Roles.HRMS_ADMIN_EXPATRIATE)
            {

                return await
                        _ctx.EmployeeLeaves
                            .Where(x => (x.AlternateEmployeeId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_APPLIED) ||
                                        (x.LineManagerId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_1) ||
                                        (x.DepartmentManagerId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_2) ||
                                        (x.EmployeeDetail.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI) && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_3)
                            .Select(x => new NotificationModel
                            {
                                Id = x.EmployeeLeaveId,
                                Title = x.LeaveTypeDetail.NameEn,
                                Message = x.EmployeeDetail.EmployeeName,
                                Avatar = x.EmployeeDetail.EmpPicture,
                                CreatedOn = x.CreatedOn
                            }).ToListAsync();

            }
            else if (userRepo?.ModuleId == (Int32)ApplicationPreferences.Modules.HRMS && 
                        userRepo?.RoleId == (Int32)ApplicationPreferences.Account_Roles.HRMS_ADMIN_LOCAL)
            {
                return await
                        _ctx.EmployeeLeaves
                            .Where(x => (x.AlternateEmployeeId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_APPLIED) ||
                                        (x.LineManagerId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_1) ||
                                        (x.DepartmentManagerId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_2) ||
                                        (x.EmployeeDetail.NationalityId == (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI) && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_3)
                            .Select(x => new NotificationModel
                            {
                                Id = x.EmployeeLeaveId,
                                Title = x.LeaveTypeDetail.NameEn,
                                Message = x.EmployeeDetail.EmployeeName,
                                Avatar = x.EmployeeDetail.EmpPicture,
                                CreatedOn = x.CreatedOn
                            }).ToListAsync();

            }
            else
            {
                return await
                        _ctx.EmployeeLeaves
                            .Where(x => (x.AlternateEmployeeId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_APPLIED) ||
                                        (x.LineManagerId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_1) ||
                                        (x.DepartmentManagerId == id && x.StatusId == (Int32)ApplicationPreferences.Validation_Details.LEAVE_APPLICATION_STEP_2))
                            .Select(x => new NotificationModel
                            {
                                Id = x.EmployeeLeaveId,
                                Title = x.LeaveTypeDetail.NameEn,
                                Message = x.EmployeeDetail.EmployeeName,
                                Avatar = x.EmployeeDetail.EmpPicture,
                                CreatedOn = x.CreatedOn
                            }).ToListAsync();

            }

        }
    }
}
