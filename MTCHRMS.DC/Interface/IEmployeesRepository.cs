using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTC.Models.HRMS;
using MTCHRMS.EntityFramework;
using MTCHRMS.EntityFramework.HRMS;

namespace MTCHRMS.DC
{
    public interface IEmployeesRepository
    {
        IQueryable<EmployeeModel> GetEmployees(int roleId);

        Task<EmployeeModel> GetEmployeePicture(int id);

        IQueryable<EmployeeDef> GetEmployeeDetail(int id, int roleId);

        EmployeeLeaveTicketModel GetEmployeeLeaveTicketDetail(int id, int roleId);

        IQueryable<EmployeePassport> GetEmployeePassports(int id);

        IQueryable<EmployeeVisa> GetEmployeeVisas(int id);

        EmployeeDef GetEmployee(int id, int roleId);

        EmployeeDef GetEmployeeDetail(string userName);

        Task<List<EmployeeDef>> GetEmployeesPassportExpiry();

        Task<List<EmployeeDef>> GetEmployeesVisaExpiry();

        Task<List<EmployeeDef>> GetEmployeesContractExpiry();

        Task<List<EmployeeDef>> GetEmployeesProbationExpiry();

        Task<List<EmployeeDef>> GetEmployeesAppraisalList();

        Task<List<EmployeeDef>> GetEmployeeSearch(EmployeeDef employee, int roleId);

        Task<List<EmployeeDef>> GetEmployeeByDepartmentId(int departmentId);

        IQueryable GetEmployeesContactNo();

        bool Save();

        bool AddEmployee(EmployeeDef newEmployee);
        bool UpdateEmployee(EmployeeDef newEmployee, int roleId);

        bool UpdateEmployeeImage(ref EmployeeDef employeeDef);

        bool AddEmployeePassport(EmployeePassport newPassport);
        bool UpdateEmployeePassport(EmployeePassport updatePasssport, int roleId);

        bool DeleteEmployeePassport(EmployeePassport deletePasssport);

        bool AddEmployeeVisa(EmployeeVisa newVisa);

        bool UpdateEmployeeVisa(EmployeeVisa updateVisa, int roleId);

        bool DeleteEmployeeVisa(EmployeeVisa newVisa);

        bool AddPreviousExperience(EmployeePreviousEmployment newPreviousEmployement);
        bool UpdatePreviousExperience(EmployeePreviousEmployment updatePreviousEmployement);

        bool DeletePreviousExperience(EmployeePreviousEmployment deletePreviousEmployment);

        bool AddEmployeeMaritals(EmployeeMarital newMarital);
        bool UpdateEmployeeMaritals(EmployeeMarital updateMarital);

        bool AddEmployeeQualification(EmployeeQualification newQualification);
        bool UpdateEmployeeQualification(EmployeeQualification updateQualification);

        bool DeleteEmployeeQualification(EmployeeQualification deleteQualification);

        bool AddEmployeeChild(EmployeeChildren newChild);
        bool UpdateEmployeeChild(EmployeeChildren updateChild);

        bool DeleteEmployeeChild(EmployeeChildren deleteChildren);

        bool AddEmployeeKin(EmployeeKin newKin);
        bool UpdateEmployeeKin(EmployeeKin updateKin);


        bool AddEmployeeContract(EmployeeContract newContract);
        bool UpdateEmployeeContract(EmployeeContract updateContract);
        Task<EmployeeContract> LoadEmployeeContract(int employeeId);


        bool AddEmployeeLeaveCategory(EmployeeLeaveCategory newLeaveCategory);
        bool UpdateEmployeeLeaveCategory(EmployeeLeaveCategory updateLeaveCategory);


        bool AddEmployeeTicketCategory(EmployeeTicketCategory newTicketCategory);
        bool UpdateEmployeeTicketCategory(EmployeeTicketCategory updateTicketCategory);

        bool AddEmployeeLeave(EmployeeLeave newLeave);

    }
}
