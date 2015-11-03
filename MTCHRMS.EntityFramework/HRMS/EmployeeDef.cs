using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MTCHRMS.EntityFramework.General;

namespace MTCHRMS.EntityFramework.HRMS
{
    public class EmployeeDef : TableStrutcture
    {
        [Key]
        public Int32 Id { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please enter Employee Code !")]
        public string EmployeeCode { get; set; }

        [Required(ErrorMessage = "Please enter Employee Name !")]
        public string EmployeeName { get; set; }

        public string EmployeeNameAr { get; set; }

        [Required(ErrorMessage = "Please enter Rank !")]
        public string Rank { get; set; }

        [Required(ErrorMessage = "Please enter Appointment Date !")]
        public DateTime Appointment { get; set; }
        
        public string ParentRegiment { get; set; }

        [Required(ErrorMessage = "Please enter Posted To !")]
        [ForeignKey("DepartmentId")]
        public int PostedTo { get; set; }

        [ForeignKey("GenderDetail")]
        public int? EmployeeGenderId { get; set; }

        public string Mobile { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [MaxLength(300)]
        public string Designation { get; set; }

        [MaxLength(300)]
        public string DesignationAr { get; set; }   

        public Int32 ManagerId { get; set; }

        [Required(ErrorMessage = "Please enter Nationality !")]
        [ForeignKey("ValidationDetailId")]
        public int NationalityId { get; set; }

        public string BloodGroup { get; set; }

        public string Religion { get; set; }

        [Required(ErrorMessage = "Please enter Place of birth !")]
        public string PlaceOfBirth { get; set; }

        [Required(ErrorMessage = "Please enter Date of birth !")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string PermanentAddress { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ServiceEndDate { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime ProbationEndDate { get; set; }

        [MaxLength(100)]
        public string ManpowerNo { get; set; }

        [MaxLength(100)]
        public string IdCardNo { get; set; }

        [ForeignKey("StatusDetail")]
        public int? StatusId { get; set; }

        [Column(TypeName = "image")]
        public byte[] EmpPicture { get; set; }

        //---- search purpose fields ---- start

        [NotMapped]
        public DateTime JoiningStartDate { get; set; }

        [NotMapped]
        public DateTime JoiningEndDate { get; set; }

        [NotMapped]
        public DateTime AgeStartDate { get; set; }

        [NotMapped]
        public DateTime AgeEndDate { get; set; }

        [NotMapped]
        public int Condition { get; set; }

        //---- search purpose fields ---- end

        public virtual Department DepartmentId { get; set; }

        public virtual ValidationDetail ValidationDetailId { get; set; }

        public virtual ValidationDetail GenderDetail { get; set; }

        public virtual ValidationDetail StatusDetail { get; set; }

        public virtual ICollection<EmployeePassport> EmployeePassports { get; set; }

        public virtual ICollection<EmployeeVisa> EmployeeVisas { get; set; }

        public virtual ICollection<EmployeeQualification> EmployeeQualifications { get; set; }

        public virtual ICollection<EmployeePreviousEmployment> PreviousEmployments { get; set; }

        public virtual ICollection<EmployeeMarital> EmployeeMarital { get; set; }

        public virtual ICollection<EmployeeChildren> Childrens  { get; set; }

        public virtual ICollection<EmployeeKin> EmployeeKin { get; set; }

        public virtual ICollection<EmployeeContract> Contract { get; set; }

    }

    public class EmployeePassport : TableStrutcture
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeDefId { get; set; }

        [Required(ErrorMessage = "Please enter Passport No !")]
        public string PassportNo { get; set; }

        [Required(ErrorMessage = "Please enter Issue Date !")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Please enter Expiry Date !")]
        public DateTime ExpiryDate { get; set; }

        [Required(ErrorMessage = "Please enter Issue Place !")]
        public string IssuePlace { get; set; }

        public int StatusId { get; set; }
    }

    public class EmployeeVisa : TableStrutcture
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeDefId { get; set; }

        [Required(ErrorMessage = "Please enter Visa Number !")]
        public string VisaNumber { get; set; }

        [Required(ErrorMessage = "Please enter Visa Issue Date !")]
        public DateTime IssueDate { get; set; }

        [Required(ErrorMessage = "Please enter Visa Expiry Date !")]
        public DateTime ExpiryDate { get; set; }

        public int StatusId { get; set; }

        //public  EmployeeDef EmployeeDefDetail { get; set; }
    }

    public class EmployeePreviousEmployment : TableStrutcture
    {
        public int Id { get; set; }

        public int EmployeeDefId { get; set; }

        [Required]
        public string CompanyName { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

    }

    public class EmployeeMarital : TableStrutcture
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeDefId { get; set; }

        public int MaritalStatusId { get; set; }

        public string SpouseName { get; set; }

        public int NationalityId { get; set; }

        public DateTime? BirthDate { get; set; }

        public string BirthPlace { get; set; }

        public DateTime? MarriageDate { get; set; }

        public string MarriagePlace { get; set; }

    
    }

    public class EmployeeChildren : TableStrutcture
    {
        public int Id { get; set; }

        public int EmployeeDefId { get; set; }

        public string ChildrenName { get; set; }

        public DateTime BirthDate { get; set; }

        [ForeignKey("ChildGenderDetail")]
        public int GenderId { get; set; }

        public virtual ValidationDetail ChildGenderDetail { get; set; }

    }

    public class EmployeeKin : TableStrutcture
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeDefId { get; set; }

        public string FullName1 { get; set; }
        public string Relationship1 { get; set; }

        public string Address1 { get; set; }

        public string Telephone1 { get; set; }

        public int CountryId1 { get; set; }

        public string Airport1 { get; set; }

        public string FullName2 { get; set; }
        public string Relationship2 { get; set; }

        public string Address2 { get; set; }

        public string Telephone2 { get; set; }

        public int CountryId2 { get; set; }

        public string Airport2 { get; set; }

        public string Notes { get; set; }
    }


    public class EmployeeQualification : TableStrutcture
    {
        public int Id { get; set; }

        public int EmployeeDefId { get; set; }

        public string EducationSchool { get; set; }

        public string EducationDegree { get; set; }

        [ForeignKey("QualificationLevel")]
        public int? LevelId { get; set; }

        public virtual ValidationDetail QualificationLevel { get; set; }

    }

    public class EmployeeContract : TableStrutcture
    {
        [Key]
        public int ContractId { get; set; }

        public int EmployeeDefId { get; set; }

        public int EmploymentTypeId { get; set; }     // e.g. permanent, contract

        public short TotalYears { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [MaxLength(500)]
        public String Notes { get; set; }

        public short StatusId { get; set; }

        public virtual ICollection<EmployeeLeaveCategory> LeaveCategory { get; set; }

        public virtual ICollection<EmployeeTicketCategory> TicketCategory { get; set; }
    }

    public class EmployeeLeaveCategory : TableStrutcture
    {
        [Key]
        public int LeaveCategoryId { get; set; }

        public int EmployeeDefId { get; set; }

        public int ContractId { get; set; }

        [ForeignKey("LeaveDetail")]
        public int LeaveId { get; set; }

        public short TotalLeaves { get; set; }

        [MaxLength(500)]
        public String Notes { get; set; }

        public short IsActive { get; set; }

        public virtual LeaveDef LeaveDetail { get; set; }

        public virtual ICollection<EmployeeLeaveYear> LeaveYears { get; set; }

    }

    public class EmployeeLeaveYear : TableStrutcture
    {
        [Key]
        public int LeaveYearId { get; set; }

        public int LeaveCategoryId { get; set; }

        public int EmployeeDefId { get; set; }

        public int ContractId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public short LeaveDays { get; set; }

        public short TransferDays { get; set; }

        public short DeductDays { get; set; }

        public short AvailedDays { get; set; }

        public virtual ICollection<EmployeeLeave> EmployeeLeaves { get; set; }
    }

    /// <summary>
    /// For Employee Leave 
    /// </summary>
    public class EmployeeLeave : TableStrutcture
    {
        [Key]
        public int EmployeeLeaveId { get; set; }

        public int LeaveYearId { get; set; }

        public int EmployeeDefId { get; set; }

        public short LeaveTypeId { get; set; }

        public short LeaveDays { get; set; }

        public DateTime LeaveStart { get; set; }

        public DateTime LeaveEnd { get; set; }

        [MaxLength(255)]
        public String AlternateAddress { get; set; }

        [MaxLength(50)]
        public String AlternatePhone { get; set; }

        public int AlternateEmployeeId { get; set; }

        public byte IsTicket { get; set; }

        public int? LineMangerId { get; set; }

        public DateTime? LineMangerDate { get; set; }

        public int? DepartmentMangerId { get; set; }

        public DateTime? DepartmentManagerDate { get; set; }

        public int? HREmployeeId { get; set; }

        public DateTime? HREmployeeDate { get; set; }

        public int StatusId { get; set; }
    }

    public class EmployeeTicketCategory : TableStrutcture
    {
        [Key]
        public int TicketCategoryId { get; set; }

        public int EmployeeDefId { get; set; }

        public int ContractId { get; set; }

        [ForeignKey("TicketDetail")]
        public int TicketId { get; set; }

        [MaxLength(500)]
        public String Notes { get; set; }

        public short IsActive { get; set; }

        public virtual TicketDef TicketDetail { get; set; }

        public virtual ICollection<EmployeeTicketYear> TicketsYears { get; set; }

    }

    public class EmployeeTicketYear : TableStrutcture
    {
        [Key]
        public int TicketYearId { get; set; }

        public int TicketCategoryId { get; set; }

        public int EmployeeDefId { get; set; }

        public int ContractId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public short Tickets { get; set; }

        public short Reimbursement { get; set; }

    }

}
