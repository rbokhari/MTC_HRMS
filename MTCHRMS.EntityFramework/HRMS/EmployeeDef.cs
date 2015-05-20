using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Dynamic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
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
        public DateTime DateOfBirth { get; set; }

        public string Address { get; set; }

        public string PermanentAddress { get; set; }

        public DateTime ServiceEndDate { get; set; }

        public DateTime ProbationEndDate { get; set; }

        [MaxLength(100)]
        public string ManpowerNo { get; set; }

        [MaxLength(100)]
        public string IdCardNo { get; set; }

        [ForeignKey("StatusDetail")]
        public int? StatusId { get; set; }

        [Column(TypeName = "image")]
        public byte[] EmpPicture { get; set; }

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

}
