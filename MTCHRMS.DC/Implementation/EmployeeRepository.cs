using System.Data.Entity.Core.Objects;
using System.IO;
using MTCHRMS.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.HRMS;
using System.Data.Entity;

namespace MTCHRMS.DC
{
    public class EmployeeRepository : IEmployeesRepository, IDisposable
    {
        DbEntityContext _ctx;

        public EmployeeRepository(DbEntityContext ctx)
        {
            _ctx = ctx;
        }

        public IQueryable<EmployeeDef> GetEmployees()
        {
            //from x in _ctx.EmployeeDefs
            return _ctx.EmployeeDefs
                .Include(c => c.DepartmentId)
                .Include(v => v.ValidationDetailId)
                .Include(c=>c.GenderDetail);
        }

        public IQueryable<EmployeePassport> GetEmployeePassports(int id)
        {
            return _ctx.EmployeePassports.Where(r => r.EmployeeDefId == id);
        }

        public IQueryable<EmployeeVisa> GetEmployeeVisas(int id)
        {
            return _ctx.EmployeeVisas.Where(r => r.EmployeeDefId == id);
        }
        public EmployeeDef GetEmployee(int id)
        {
            return _ctx.EmployeeDefs.Single(r => r.Id == id);
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

        public bool UpdateEmployee(EmployeeDef employee)
        {
            try
            {
                var dep = new DepartmentRepository(_ctx);
                var department = dep.GetDepartment(employee.PostedTo);
                employee.DepartmentId = department;

                var validation = new ValidationRepository(_ctx);
                var valid = validation.GetValidationDetail(employee.NationalityId);
                employee.ValidationDetailId = valid;

                //_ctx.EmployeeDefs.Attach(_ctx.EmployeeDefs.Single(r => r.Id == employee.Id));
                _ctx.Entry(employee).State = EntityState.Modified;
                return true;

            }
            catch (Exception ex)
            {
                // TODO log this error    
                return false;
            }
            
        }


        public IQueryable<EmployeeDef> GetEmployeeDetail(int id)
        {
            try
            {
                //return _ctx.EmployeeDefs.Where(r => r.Id == id)
                //    .Include(c => c.Childrens.Select(g => g.ChildGenderDetail))
                //    .Include("EmployeePassports")
                //    .Include("EmployeeVisas")
                //    .Include("PreviousEmployments")
                //    .Include("EmployeeMarital")
                //    .Include("EmployeeKin")
                //    .Include("EmployeeQualifications")
                //    .Include("DepartmentId")
                //    .Include("ValidationDetailId");

                return _ctx.EmployeeDefs.Where(r => r.Id == id)
                    .Include(c => c.Childrens.Select(g => g.ChildGenderDetail))
                    .Include(p => p.EmployeePassports)
                    .Include(v => v.EmployeeVisas)
                    .Include(e => e.PreviousEmployments)
                    .Include(m => m.EmployeeMarital)
                    .Include(k => k.EmployeeKin)
                    .Include(q => q.EmployeeQualifications.Select(g=> g.QualificationLevel))
                    .Include(d => d.DepartmentId)
                    .Include(v => v.ValidationDetailId);

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

        public bool UpdateEmployeePassport(EmployeePassport updatePasssport)
        {
            try
            {
                updatePasssport.EmployeeDefId = GetEmployee(updatePasssport.EmployeeDefId).Id;

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

        public bool UpdateEmployeeVisa(EmployeeVisa updateVisa)
        {
            try
            {
                updateVisa.EmployeeDefId = GetEmployee(updateVisa.EmployeeDefId).Id;

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
                    .Where(d => d.EmployeePassports.Any(e => e.StatusId == 1 && (DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) >= -30 && DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) <= 30)))
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
                    .Where(d => d.EmployeeVisas.Any(e=>e.StatusId == 1 && (DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) >= -30 && DbFunctions.DiffDays(DateTime.Now, e.ExpiryDate) <= 30)))
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
                    .Where(d => DbFunctions.DiffDays(DateTime.Now, d.ServiceEndDate) <= 120)   // 4 months
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
                    .Where(d=>DbFunctions.DiffDays(d.Appointment, DateTime.Now) >=150 && DbFunctions.DiffDays(d.Appointment, DateTime.Now) <=180)
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
                    .Where(d => DbFunctions.DiffDays(d.Appointment, DateTime.Now) % 365 > 325 )
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

    }
}

