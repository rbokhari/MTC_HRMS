using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MTCHRMS.EntityFramework.HRMS;


namespace MTC.Models.HRMS
{
    public class EmployeeModel
    {
        public int Id { get; set; }

        public String UserName { get; set; }

        public String Code { get; set; }

        public String NameEn { get; set; }

        public String NameAr { get; set; }

        public String Rank { get; set; }

        public int DepartmentId { get; set; }

        public String DepartmentName { get; set; }

        public String DesignationEn { get; set; }

        public String DesignationAr { get; set; }

        public int NationalityId { get; set; }

        public String Nationality { get; set; }

        public byte[] Picture { get; set; }

        public int ContractId { get; set; }

    }

    public class EmployeeLeaveTicketModel
    {
        public int Id { get; set; }

        public String UserName { get; set; }

        public String Code { get; set; }

        public String NameEn { get; set; }

        public String NameAr { get; set; }

        public String Rank { get; set; }

        public String Regiment { get; set; }

        public int DepartmentId { get; set; }

        public String DepartmentName { get; set; }

        public String DesignationEn { get; set; }

        public String DesignationAr { get; set; }

        public String Nationality { get; set; }

        public byte[] Picture { get; set; }

        public int ContractId { get; set; }

        public int LeaveCategoryId { get; set; }

        public int LeaveYearCurrentId { get; set; }

        public int TicketCategoryId { get; set; }

        public DateTime CurrentContractStart { get; set; }

        public DateTime? CurrentContractEnd { get; set; }

        public DateTime CurrentContractYearStart { get; set; }

        public DateTime CurrentContractYearEnd { get; set; }

        public int TotalDays { get; set; }

        public int TransferDays { get; set; }

        public int DeductDays { get; set; }

        public int AvailedDays { get; set; }

        public short TotalTickets { get; set; }

        public short ReimbursedTickets { get; set; }

        public IOrderedQueryable<EmployeeContract> Contracts { get; set; }

    }

}
