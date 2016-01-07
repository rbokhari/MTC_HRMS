using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTC.GlobalVariables
{
    public static class ApplicationPreferences
    {

        public const string DATE_FORMAT = "dd/MM/yyyy";


        public enum Modules : int
        {
            HRMS = 1,
            INVENTORY  = 2
        }

        public enum ScreenId : int
        {
            Inventory_Stock_Add = 405,
            Inventory_Distribution_Add = 406
        }

        public enum Account_Roles
        {
            ADMIN = 1,
            HRMS_ADMIN_EXPATRIATE = 2,
            HRMS_USER_EXPATRIATE = 3,
            INV_ADMIN = 4,
            INV_USER = 5,
            HRMS_ADMIN_LOCAL = 6,
            HRMS_USER_LOCAL = 7
        }

        public enum Validations
        {
            NATIONALITY = 2,
            COUNTRY = 3,
            MARITAL_STATUS = 4,
            GENDER = 5,
            QUALIFICATION_LEVEL  = 6,
            ITEM_TYPE  = 7,
            ITEM_CATEGORY = 8,
            ITEM_TECHNICIAN = 9,
            ITEM_YEARS = 10,
            EMPLOYEE_STATUS = 11,
            MAINTENANCE_TYPE = 12,
            ITEM_STOCK_STATUS = 13,
            LEAVE_TYPE = 14,
            LEAVE_SCHEDULE = 15,
            TICKET_ELIGIBILITY = 16,
            TRAINING_CATEGORY = 17,
            TRAINING_ORGANIZERS = 18,
            TRAINING_YEARS = 19
        }

        public enum Validation_Details
        {
            NATIONALITY_OMANI = 1,


            EMPLOYEE_STATUS_PROBATION = 42,
            EMPLOYEE_STATUS_CONTRACTOR = 45,
            EMPLOYEE_STATUS_PERMANENT = 46,
            EMPLOYEE_STATUS_RETIRED = 47,


            ITEM_STOCK_STATUS_STORE = 5108,
            ITEM_STOCK_STATUS_ASSIGNED = 5109,
            ITEM_STOCK_STATUS_MAINTENANCE = 5110,


            LEAVE_APPLICATION_APPLIED = 5200,
            LEAVE_APPLICATION_STEP_1 = 5201,
            LEAVE_APPLICATION_STEP_2 = 5202,
            LEAVE_APPLICATION_STEP_3 = 5203,
            LEAVE_APPLICATION_STEP_4 = 5204,
            LEAVE_APPLICATION_COMPLETE = 5205

        }

    }
}
