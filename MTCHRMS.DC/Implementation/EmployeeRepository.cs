using System.Data.Entity.Core.Objects;
using System.IO;
using MTCHRMS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.General;
using MTCHRMS.EntityFramework.HRMS;
using System.Data.Entity;
using MTC.GlobalVariables;

namespace MTCHRMS.DC
{
    public class EmployeeRepository : IEmployeesRepository, IDisposable
    {
        DbEntityContext _ctx;

        public EmployeeRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<EmployeeDef> GetEmployees(int roleId)
        {
            try
            {
                switch ((ApplicationPreferences.Account_Roles)roleId)
                {
                    case ApplicationPreferences.Account_Roles.ADMIN:
                        return _ctx.EmployeeDefs
                            .Include(c => c.DepartmentId)
                            .Include(v => v.ValidationDetailId)
                            .Include(c => c.GenderDetail)
                            .Include(c => c.StatusDetail)
                            .OrderBy(c=>c.EmployeeName);

                    case ApplicationPreferences.Account_Roles.HRMS_ADMIN_LOCAL:
                    case ApplicationPreferences.Account_Roles.HRMS_USER_LOCAL:

                        return _ctx.EmployeeDefs
                            .Where(c => c.NationalityId == (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI)
                            .Include(c => c.DepartmentId)
                            .Include(v => v.ValidationDetailId)
                            .Include(c => c.GenderDetail)
                            .Include(c => c.StatusDetail)
                            .OrderBy(c => c.EmployeeName); 

                    case ApplicationPreferences.Account_Roles.HRMS_ADMIN_EXPATRIATE:
                    case ApplicationPreferences.Account_Roles.HRMS_USER_EXPATRIATE:

                        return _ctx.EmployeeDefs
                            .Where(c => c.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI)
                            .Include(c => c.DepartmentId)
                            .Include(v => v.ValidationDetailId)
                            .Include(c => c.GenderDetail)
                            .Include(c => c.StatusDetail)
                            .OrderBy(c => c.EmployeeName); 
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IQueryable<EmployeePassport> GetEmployeePassports(int id)
        {
            return _ctx.EmployeePassports.Where(r => r.EmployeeDefId == id);
        }

        public IQueryable<EmployeeVisa> GetEmployeeVisas(int id)
        {
            return _ctx.EmployeeVisas.Where(r => r.EmployeeDefId == id);
        }
        public EmployeeDef GetEmployee(int id, int roleId)
        {
            switch ((ApplicationPreferences.Account_Roles) roleId)
            {
                case ApplicationPreferences.Account_Roles.ADMIN:
                    return _ctx.EmployeeDefs.Single(r => r.Id == id);

                case ApplicationPreferences.Account_Roles.HRMS_ADMIN_LOCAL:
                case ApplicationPreferences.Account_Roles.HRMS_USER_LOCAL:

                    return _ctx.EmployeeDefs.Single(r => r.Id == id && r.NationalityId == (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI);

                case ApplicationPreferences.Account_Roles.HRMS_ADMIN_EXPATRIATE:
                case ApplicationPreferences.Account_Roles.HRMS_USER_EXPATRIATE:

                    return _ctx.EmployeeDefs.Single(r => r.Id == id && r.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI);

            }

            return null;
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

        public bool AddEmployee(EmployeeDef newEmployee)
        {
            try
            {
                //newEmployee.CreatedBy = 1;
                //newEmployee.CreatedOn = DateTime.UtcNow;
                _ctx.EmployeeDefs.Add(newEmployee);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
            
        }

        public bool UpdateEmployee(EmployeeDef employee, int roleId)
        {
            try
            {

                //var dep = new DepartmentRepository(_ctx);
                //var department = dep.GetDepartment(employee.PostedTo);
                //employee.DepartmentId = department;

                //var validation = new ValidationRepository(_ctx);
                //var valid = validation.GetValidationDetail(employee.NationalityId);
                //_ctx.ValidationDetails.Attach(valid);
                //employee.ValidationDetailId = valid;

                //if (employee.EmployeeGenderId.HasValue)
                //{
                //    var validation1 = new ValidationRepository(_ctx);
                //    var gender = validation1.GetValidationDetail(employee.EmployeeGenderId.Value);
                //    _ctx.ValidationDetails.Attach(gender);
                //    employee.GenderDetail = gender;
                //}

                //if (employee.StatusId.HasValue)
                //{
                //    var statusValidation = new ValidationRepository(_ctx);
                //    var status = statusValidation.GetValidationDetail(employee.StatusId.Value);
                //    _ctx.ValidationDetails.Attach(status);
                //    employee.StatusDetail = status;
                //}

                //_ctx.Entry(employee.GenderDetail).State = EntityState.Modified;
                //_ctx.Entry(employee.ValidationDetailId).State = EntityState.Modified;
                //_ctx.Entry(employee.StatusDetail).State = EntityState.Modified;

                //_ctx.EmployeeDefs.Attach(employee);
                //employee.Id = GetEmployee(employee.Id).Id;

                var employeeFetch = GetEmployee(employee.Id, roleId);
                var attachedEmployee = _ctx.Entry(employeeFetch);
                attachedEmployee.CurrentValues.SetValues(employee);

                //employeeFetch = employee;

                //_ctx.EmployeeDefs.Attach(_ctx.EmployeeDefs.Single(r => r.Id == employee.Id));
                //_ctx.EmployeeDefs.Attach(employee);
                
                //_ctx.Entry(employee).State = EntityState.Modified;
                return true;

            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
            
        }


        public IQueryable<EmployeeDef> GetEmployeeDetail(int id, int roleId)
        {
            try
            {
                var emp = _ctx.EmployeeDefs.Where(r => r.Id == id)
                    .Include(c => c.Childrens.Select(g => g.ChildGenderDetail))
                    .Include(p => p.EmployeePassports)
                    .Include(v => v.EmployeeVisas)
                    .Include(e => e.PreviousEmployments)
                    .Include(m => m.EmployeeMarital)
                    .Include(k => k.EmployeeKin)
                    .Include(q => q.EmployeeQualifications.Select(g => g.QualificationLevel))
                    .Include(d => d.DepartmentId)
                    .Include(d => d.GenderDetail)
                    .Include(d => d.StatusDetail)
                    .Include(v => v.ValidationDetailId)
                    .Include(v => v.Contract.Select(b => b.LeaveCategory.Select(c=>c.LeaveDetail.TypeDetail)))
                    .Include(v => v.Contract.Select(b => b.LeaveCategory.Select(c => c.LeaveDetail.ScheduleDetail)))
                    .Include(v => v.Contract.Select(b => b.LeaveCategory.Select(c => c.LeaveYears)))
                    .Include(v => v.Contract.Select(b => b.TicketCategory.Select(c => c.TicketDetail.ScheduleDetail)))
                    .Include(v => v.Contract.Select(b => b.TicketCategory.Select(c => c.TicketDetail.EligibilityDetail)));


                switch ((ApplicationPreferences.Account_Roles)roleId)
                {
                    case ApplicationPreferences.Account_Roles.ADMIN:
                        //emp = emp.Where(r=>r.NationalityId);

                        break;

                    case ApplicationPreferences.Account_Roles.HRMS_ADMIN_LOCAL:
                    case ApplicationPreferences.Account_Roles.HRMS_USER_LOCAL:

                        emp = emp.Where(r => r.NationalityId == (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI);

                        break;

                    case ApplicationPreferences.Account_Roles.HRMS_ADMIN_EXPATRIATE:
                    case ApplicationPreferences.Account_Roles.HRMS_USER_EXPATRIATE:

                        emp = emp.Where(r => r.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI);

                        break;
                }

                return emp;

            }
            catch (Exception)
            {
                return null;
            }
        }


        public bool AddEmployeePassport(EmployeePassport newPassport)
        {
            try
            {
                _ctx.EmployeePassports.Add(newPassport);
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
            
        }

        public bool UpdateEmployeePassport(EmployeePassport updatePasssport, int roleId)
        {
            try
            {
                updatePasssport.EmployeeDefId = GetEmployee(updatePasssport.EmployeeDefId,roleId).Id;

                _ctx.Entry(updatePasssport).State = EntityState.Modified;
                return true;

            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteEmployeePassport(EmployeePassport deletePasssport)
        {
            try
            {
                var passport = _ctx.EmployeePassports.Single(i => i.Id == deletePasssport.Id);

                _ctx.EmployeePassports.Remove(passport);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }


        public bool AddEmployeeVisa(EmployeeVisa newVisa)
        {
            try
            {
                _ctx.EmployeeVisas.Add(newVisa);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeVisa(EmployeeVisa updateVisa, int roleId)
        {
            try
            {
                updateVisa.EmployeeDefId = GetEmployee(updateVisa.EmployeeDefId, roleId).Id;

                _ctx.Entry(updateVisa).State = EntityState.Modified;
                return true;

            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool DeleteEmployeeVisa(EmployeeVisa newVisa)
        {
            try
            {
                var visa = _ctx.EmployeeVisas.Single(i => i.Id == newVisa.Id);

                _ctx.EmployeeVisas.Remove(visa);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }



        public bool AddPreviousExperience(EmployeePreviousEmployment newPreviousEmployement)
        {
            try
            {
                _ctx.PreviousEmployments.Add(newPreviousEmployement);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdatePreviousExperience(EmployeePreviousEmployment updatePreviousEmployement)
        {
            try
            {
                _ctx.Entry(updatePreviousEmployement).State = EntityState.Modified;
                return true;
            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool AddEmployeeMaritals(EmployeeMarital newMarital)
        {
            try
            {
                _ctx.EmployeeMaritals.Add(newMarital);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeMaritals(EmployeeMarital updateMarital)
        {
            try
            {
                _ctx.Entry(updateMarital).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool DeleteEmployeeQualification(EmployeeQualification deleteQualification)
        {
            try
            {
                var qualification = _ctx.EmployeeQualifications.Single(i => i.Id == deleteQualification.Id);

                _ctx.EmployeeQualifications.Remove(qualification);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool AddEmployeeChild(EmployeeChildren newChild)
        {
            try
            {
                _ctx.EmployeeChildrens.Add(newChild);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeChild(EmployeeChildren updateChild)
        {
            try
            {
                _ctx.Entry(updateChild).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool AddEmployeeKin(EmployeeKin newKin)
        {
            try
            {
                _ctx.EmployeeKins.Add(newKin);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeKin(EmployeeKin updateKin)
        {
            try
            {
                _ctx.Entry(updateKin).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool AddEmployeeContract(EmployeeContract newContract)
        {
            try
            {
                _ctx.EmployeeContracts.Add(newContract);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeContract(EmployeeContract updateContract)
        {
            try
            {
                _ctx.Entry(updateContract).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public async Task<EmployeeContract> LoadEmployeeContract(int employeeId)
        {
            try
            {
                var contract = await _ctx.EmployeeContracts.SingleAsync(c => c.EmployeeDefId == employeeId && c.StatusId == 1);
                return contract;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool AddEmployeeLeaveCategory(EmployeeLeaveCategory newLeaveCategory)
        {
            try
            {
                _ctx.EmployeeLeaveCategories.Add(newLeaveCategory);
                _ctx.SaveChanges();

                var contract = _ctx.EmployeeContracts.SingleAsync(c => c.EmployeeDefId == newLeaveCategory.EmployeeDefId && c.StatusId == 1).Result;


                for (int i = 0; i < contract.TotalYears; i++)
                {
                    var leaveYear = new EmployeeLeaveYear
                    {
                        LeaveCategoryId = newLeaveCategory.LeaveCategoryId,
                        EmployeeDefId = newLeaveCategory.EmployeeDefId,
                        ContractId = newLeaveCategory.ContractId,
                        StartDate = contract.StartDate.AddMonths(12 * i),
                        EndDate = contract.StartDate.AddMonths(12*(i+1)).AddDays(-1),
                        LeaveDays = newLeaveCategory.TotalLeaves,
                        TransferDays = 0,
                        DeductDays = 0,
                        CreatedBy = newLeaveCategory.CreatedBy,
                        CreatedOn = newLeaveCategory.CreatedOn

                    };
                    _ctx.EmployeeLeaveYears.Add(leaveYear);
                }
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeLeaveCategory(EmployeeLeaveCategory updateLeaveCategory)
        {
            try
            {
                _ctx.Entry(updateLeaveCategory).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool AddEmployeeTicketCategory(EmployeeTicketCategory newTicketCategory)
        {
            try
            {
                _ctx.EmployeeTicketCategories.Add(newTicketCategory);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeTicketCategory(EmployeeTicketCategory updateTicketCategory)
        {
            try
            {
                _ctx.Entry(updateTicketCategory).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public void Dispose()
        {
            _ctx.Dispose();
            //base.Dispose(disposing);
        }


        public bool UpdateEmployeeImage(ref EmployeeDef employeeDef)
        {
            //var employee = GetEmployee(id);

            //employee.EmpPicture = ms.ToArray();
            //employee.EmpPicture = imgFile.ToArray();
            _ctx.Entry(employeeDef).State = EntityState.Modified;

            return true;

        }


        public EmployeeDef GetEmployeeDetail(string userName)
        {
            
            try
            {
                return _ctx.EmployeeDefs.FirstOrDefault(r => r.UserName.Trim().ToLower() == userName.Trim().ToLower());

                //return _ctx.EmployeeDefs.FirstOrDefault(r => String.Equals(r.UserName.Trim().ToLower(), userName.Trim().ToLower(), StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception)
            {
                // TODO log this error    
                //throw;
                return null;
            }
            
        }

        public bool AddEmployeeQualification(EmployeeQualification newQualification)
        {
            try
            {
                _ctx.EmployeeQualifications.Add(newQualification);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }

        public bool UpdateEmployeeQualification(EmployeeQualification updateQualification)
        {
            try
            {
                _ctx.Entry(updateQualification).State = EntityState.Modified;
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }
        }


        public bool DeletePreviousExperience(EmployeePreviousEmployment deletePreviousEmployment)
        {
            try
            {
                var employement = _ctx.PreviousEmployments.Single(i => i.Id == deletePreviousEmployment.Id);

                _ctx.PreviousEmployments.Remove(employement);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }

        }

        public bool DeleteEmployeeChild(EmployeeChildren deleteChildren)
        {
            try
            {
                var child = _ctx.EmployeeChildrens.Single(i => i.Id == deleteChildren.Id);

                _ctx.EmployeeChildrens.Remove(child);
                return true;
            }
            catch (Exception)
            {
                // TODO log this error    
                return false;
            }

        }


        public async Task<List<EmployeeDef>> GetEmployeesPassportExpiry()
        {
            try
            {
                return await _ctx.EmployeeDefs
                    .Include(c=>c.EmployeePassports)
                    .Include(c=>c.DepartmentId)
                    .Include(c=>c.ValidationDetailId)
                    .Where(d => d.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI && 
                        d.EmployeePassports.Any(e => e.StatusId != (Int32)ApplicationPreferences.Validation_Details.EMPLOYEE_STATUS_RETIRED && 
                            (DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) >= -30 && DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) <= 30)))
                    .ToListAsync();
            }
            catch (Exception)
            {
                // TODO log this error    
                return null;
            }
        }

        public async Task<List<EmployeeDef>> GetEmployeesVisaExpiry()
        {
            try
            {
                var results = await _ctx.EmployeeDefs
                    .Include(c => c.EmployeeVisas)
                    .Include(c=>c.DepartmentId)
                    .Include(c => c.ValidationDetailId)
                    .Where(d => d.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI &&
                        d.EmployeeVisas.Any(e => e.StatusId != (Int32)ApplicationPreferences.Validation_Details.EMPLOYEE_STATUS_RETIRED && 
                            (DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) >= -30 && DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) <= 30)))
                    .ToListAsync();

                return results;
            }
            catch (Exception ex)
            {
                // TODO log this error
                throw ex;
            }
        }

        public async Task<List<EmployeeDef>> GetEmployeesContractExpiry()
        {
            try
            {
                return await _ctx.EmployeeDefs
                    .Where(d => d.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI && 
                        DbFunctions.DiffDays(DateTime.Now, d.ServiceEndDate) <= 120)   // 4 months
                    .Include(e => e.DepartmentId)
                    .Include(e => e.ValidationDetailId).ToListAsync();

            }
            catch (Exception)
            {
                // TODO log this error    
                return null;
            }
        }

        public async Task<List<EmployeeDef>> GetEmployeesProbationExpiry()
        {
            try
            {
                return await _ctx.EmployeeDefs
                    .Where(d => d.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI && 
                        DbFunctions.DiffDays(d.Appointment, DateTime.Now) >= 150 && DbFunctions.DiffDays(d.Appointment, DateTime.Now) <= 180)
                    .Include(e => e.DepartmentId)
                    .Include(e => e.ValidationDetailId).ToListAsync();

            }
            catch (Exception)
            {
                // TODO log this error    
                return null;
            }
        }

        public async Task<List<EmployeeDef>> GetEmployeesAppraisalList()
        {
            try
            {
                return await _ctx.EmployeeDefs
                    .Where(d => d.NationalityId != (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI && 
                        DbFunctions.DiffDays(d.Appointment, DateTime.Now) % 365 > 325)
                    .Include(e => e.DepartmentId)
                    .Include(e => e.ValidationDetailId).ToListAsync();

            }
            catch (Exception)
            {
                // TODO log this error    
                return null;
            }
            
        }


        public IQueryable GetEmployeesContactNo()
        {
            try
            {
                return _ctx.EmployeeDefs
                    .Include(e => e.DepartmentId)
                    .Include(e => e.ValidationDetailId)
                    .Select(x=> new
                    {
                        empId = x.Id,
                        empCode = x.EmployeeCode,
                        empName =  x.EmployeeName,
                        empDepartment = x.DepartmentId.DepartmentName,
                        empDesignation = x.Designation,
                        empNationalityEn = x.ValidationDetailId.NameEn,
                        empNationalityAr = x.ValidationDetailId.NameAr,
                        empContactNo = x.Phone,
                        empMobile = x.Mobile,
                        nationalityId = x.NationalityId,
                        empPicture = x.EmpPicture
                    })
                    .OrderBy(x=>x.empDepartment);

            }
            catch (Exception)
            {
                // TODO log this error    
                return null;
            }
        }

        public async Task<List<EmployeeDef>> GetEmployeeByDepartmentId(int departmentId)
        {
            try
            {
                return await _ctx.EmployeeDefs
                            .Where(c => c.PostedTo == departmentId)
                            .Include(c => c.DepartmentId)
                            .OrderBy(c => c.EmployeeName)
                            .ToListAsync();
            }
            catch (Exception)
            {
                return null ;
            }
        }


        public async Task<List<EmployeeDef>> GetEmployeeSearch(EmployeeDef employee, int roleId)
        {
            try
            {
                var emp = _ctx.EmployeeDefs
                    .Include(c => c.DepartmentId);

                switch ((ApplicationPreferences.Account_Roles)roleId)
                {
                    case ApplicationPreferences.Account_Roles.HRMS_ADMIN_LOCAL:
                    case ApplicationPreferences.Account_Roles.HRMS_USER_LOCAL:

                        emp =
                            emp.Where(
                                c =>
                                    c.NationalityId ==
                                    (Int32) ApplicationPreferences.Validation_Details.NATIONALITY_OMANI);

                        break;
                    case ApplicationPreferences.Account_Roles.HRMS_ADMIN_EXPATRIATE:
                    case ApplicationPreferences.Account_Roles.HRMS_USER_EXPATRIATE:
                        emp =
                            emp.Where(
                                c =>
                                    c.NationalityId !=
                                    (Int32)ApplicationPreferences.Validation_Details.NATIONALITY_OMANI);

                        break;


                }


                if (employee.Condition == 0)
                {
                    emp = emp
                        .Where(c => c.EmployeeCode.ToLower().Contains(employee.EmployeeCode.ToLower()) ||
                                    c.EmployeeName.ToLower().Contains(employee.EmployeeName.ToLower()) ||
                                    c.PostedTo == employee.PostedTo || c.NationalityId == employee.NationalityId ||
                                    c.EmployeeGenderId == employee.EmployeeGenderId || c.StatusId == employee.StatusId ||
                                    (c.Appointment >= employee.JoiningStartDate && c.Appointment <= employee.JoiningEndDate) ||
                                    (c.DateOfBirth >= employee.AgeStartDate && c.DateOfBirth <= employee.AgeEndDate));

                }
                else
                {
                    if (!string.IsNullOrEmpty(employee.EmployeeCode))
                    {
                        emp = emp.Where(c => c.EmployeeCode.ToLower().Contains(employee.EmployeeCode.ToLower()));
                        //emp = emp.Where(c => c.EmployeeCode == employee.EmployeeCode);
                    }

                    if (!string.IsNullOrEmpty(employee.EmployeeName))
                    {
                        emp = emp.Where(c => c.EmployeeName.ToLower().Contains(employee.EmployeeName.ToLower()));
                    }

                    if (employee.PostedTo !=0)
                    {
                        emp = emp.Where(c => c.PostedTo == employee.PostedTo);
                    }

                    if (employee.NationalityId !=0)
                    {
                        emp = emp.Where(c => c.NationalityId == employee.NationalityId);
                    }

                    if (employee.EmployeeGenderId != 0)
                    {
                        emp = emp.Where(c => c.EmployeeGenderId == employee.EmployeeGenderId);
                    }

                    if (employee.StatusId != 0)
                    {
                        emp = emp.Where(c => c.StatusId == employee.StatusId);
                    }


                    if (employee.JoiningStartDate != null)
                    {
                        emp =
                            emp.Where(
                                c =>
                                    c.Appointment >= employee.JoiningStartDate &&
                                    c.Appointment <= employee.JoiningEndDate);
                    }

                    if (employee.AgeStartDate != null)
                    {
                        emp =
                            emp.Where(
                                c =>
                                    c.DateOfBirth >= employee.AgeStartDate &&
                                    c.DateOfBirth <= employee.AgeEndDate);
                    }

                }

                return await emp.ToListAsync();
            }
            catch (Exception)
            {
                // TODO log this error    
                return null;
            }

        }
    }
}

