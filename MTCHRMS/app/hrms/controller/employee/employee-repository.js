/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('employeeRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllEmployees = function () {
        return $resource('/api/employee').query();
    };

    var _getSingleEmployee = function (id) {
        return $resource('/api/employee/GetSingleEmployee/?id=' + id).query();
    };

    var _getEmployeePicture = function (id) {
        return $resource('/api/employee/GetEmployeePicture/' + id).get();
    };

    var _getEmployeeDetailById = function (id) {
        return $resource('/api/employee/' + id).query();
    };

    var _getEmployeeLeaveTicketDetailById = function (id) {
        return $resource('/api/employee/GetEmployeeLeaveTicketDetail/' + id).get();
    };

    var _getEmployeeDetailByUserName = function (userName) {
        return $resource('/api/employee/GetEmployeeByUserName/?userName=' + userName).get();
    };

    var _getEmployeesPassportExpiry = function () {
        return $resource('/api/employee/GetPassportExpiryList').query();
    };

    var _getEmployeesVisaExpiry = function () {
        return $resource('/api/employee/GetVisaExpiryList').query();
    };

    var _getEmployeesContractExpiry = function () {
        return $resource('/api/employee/GetContractExpiryList').query();
    };

    var _getEmployeesProbationExpiry = function () {
        return $resource('/api/employee/GetProbationExpiryList').query();
    };

    var _getEmployeesContactNoList = function () {
        return $resource('/api/employee/GetEmployeeContactNoList').query();
    };

    var _getEmployeesAppraisalList = function () {
        return $resource('/api/employee/GetAppraisalList').query();
    };

    var _getEmployeesByDepartmentId = function (id) {
        return $resource('/api/employee/GetEmployeeByDepartmentId/' + id).query();
    };


    var _getEmployeesSearchList = function (employee) {
        return $resource('/api/employee/GetEmployeeSearch').query(employee);
    };


    var _addEmployee = function (employeeDef) {
        return $resource('/api/employee').save(employeeDef);
    };

    var _editEmployee = function (employeeDef) {
        return $http.put('/api/employee/' + employeeDef.id, employeeDef);
    };

    var _addEmployeePassport = function (employeePassport) {
        return $resource('/api/employee/' + employeePassport.employeeDefId + '/PostEmployeePassport').save(employeePassport);
    };

    var _deleteEmployeePassport = function (employeePassport) {
        return $resource('/api/employee/' + employeePassport.employeeDefId + '/DeleteEmployeePassport').save(employeePassport);
    };

    var _addEmployeeVisa = function (employeeVisa) {
        return $resource('/api/employee/' + employeeVisa.employeeDefId + '/PostEmployeeVisa').save(employeeVisa);
    };

    var _deleteEmployeeVisa = function (employeeVisa) {
        return $resource('/api/employee/' + employeeVisa.employeeDefId + '/DeleteEmployeeVisa').save(employeeVisa);
    };

    var _addEmployeePreviousEmployment = function(employeePreviousEmployment) {
        return $resource('/api/employee/' + employeePreviousEmployment.employeeDefId + '/PostEmployeePreviousEmployment').save(employeePreviousEmployment);
    };

    var _deleteEmployeePreviousEmployement = function (previousEmployement) {
        return $resource('/api/employee/' + previousEmployement.employeeDefId + '/DeletePreviousEmployement').save(previousEmployement);
    };


    var _addEmployeeMarital = function(employeeMaritals) {
        return $resource('/api/employee/' + employeeMaritals.employeeDefId + '/PostEmployeeMarital').save(employeeMaritals);
    };

    var _addEmployeeQualification = function(employeeQualification) {
        return $resource('/api/employee/' + employeeQualification.employeeDefId + '/PostEmployeeQualification').save(employeeQualification);
    };

    var _deleteEmployeeQualification = function (employeeQualification) {
        return $resource('/api/employee/' + employeeQualification.employeeDefId + '/DeleteEmployeeQualification').save(employeeQualification);
    };

    var _addEmployeeChild = function (employeeChildrens) {
        
        return $resource('/api/employee/' + employeeChildrens.employeeDefId + '/PostEmployeeChild').save(employeeChildrens);
    };

    var _deleteEmployeeChild = function (employeeChild) {
        return $resource('/api/employee/' + employeeChild.employeeDefId + '/DeleteEmployeeChild').save(employeeChild);
    };

    var _addEmployeeKin = function(employeeKins) {
        return $resource('/api/employee/' + employeeKins.employeeDefId + '/PostEmployeeKin').save(employeeKins);
    };

    var _addEmployeeContract = function (employeeContract) {
        return $resource('/api/employee/' + employeeContract.employeeDefId + '/PostEmployeeContract').save(employeeContract);
    };

    var _getEmployeeContract = function(id) {
        return $resource('/api/employee/GetEmployeeContract/' + id).get();
    }

    var _addEmployeeLeaveCategory = function (employeeLeave) {
        return $resource('/api/employee/' + employeeLeave.employeeDefId + '/PostEmployeeLeaveCategory').save(employeeLeave);
    };
    var _addEmployeeTicketCategory = function (employeeTicket) {
        return $resource('/api/employee/' + employeeTicket.employeeDefId + '/PostEmployeeTicketCategory').save(employeeTicket);
    };

    var _addEmployeeImage = function (employee) {
        return $resource('/api/employee/upload').save(employee);
    };

    return {
        getAllEmployees: _getAllEmployees,
        getSingleEmployee: _getSingleEmployee,
        getEmployeePicture:_getEmployeePicture,
        getEmployeeDetailById: _getEmployeeDetailById,
        getEmployeeLeaveTicketDetailById:_getEmployeeLeaveTicketDetailById,
        getEmployeeDetailByUserName: _getEmployeeDetailByUserName,
        getEmployeesPassportExpiry: _getEmployeesPassportExpiry,
        getEmployeesVisaExpiry: _getEmployeesVisaExpiry,
        getEmployeesContractExpiry: _getEmployeesContractExpiry,
        getEmployeesProbationExpiry: _getEmployeesProbationExpiry,
        getEmployeesContactNoList: _getEmployeesContactNoList,
        getEmployeesAppraisalList: _getEmployeesAppraisalList,
        getEmployeesSearchList: _getEmployeesSearchList,
        getEmployeesByDepartmentId:_getEmployeesByDepartmentId,
        addEmployee: _addEmployee,
        editEmployee: _editEmployee,
        addEmployeePassport: _addEmployeePassport,
        deleteEmployeePassport: _deleteEmployeePassport,
        addEmployeeVisa: _addEmployeeVisa,
        deleteEmployeeVisa: _deleteEmployeeVisa,
        addEmployeeQualification: _addEmployeeQualification,
        deleteEmployeeQualification: _deleteEmployeeQualification,
        addEmployeePreviousEmployment: _addEmployeePreviousEmployment,
        deleteEmployeePreviousEmployement: _deleteEmployeePreviousEmployement,
        addEmployeeMarital: _addEmployeeMarital,
        addEmployeeChild: _addEmployeeChild,
        deleteEmployeeChild: _deleteEmployeeChild,
        addEmployeeKin: _addEmployeeKin,
        addEmployeeContract: _addEmployeeContract,
        getEmployeeContract:_getEmployeeContract,
        addEmployeeLeaveCategory: _addEmployeeLeaveCategory,
        addEmployeeTicketCategory:_addEmployeeTicketCategory,
        addEmployeeImage: _addEmployeeImage
        
    };

}]);


//hrmsModule.factory('employeeRepository', ['$resource', function ($resource) {
//    return {
//        get: function () {
//            return $resource('/api/employee').query();
//        }
//    };
//}]);


//hrmsModule.factory('employeeSingleRepository', ['$resource', function ($resource) {
//    return {
//        get: function (id) {
//            return $resource('/api/employee/' + id).query();
//        }
//    };

//}]);

//hrmsModule.factory('employeeAddRepository', ['$resource', function ($resource) {

//    return {
//        save: function (employeeDef) {
//            console.log("employee add respository");
//            console.log("Name : " + employeeDef.employeeName);

//            return $resource('/api/employee').save(employeeDef);
//        }
//    };

//}]);

//hrmsModule.factory('employeeEditRepository', ['$http', function ($http) {

//    return {
//        put: function (id, employee) {
//            return $http.put('/api/employee/' + id, employee);
//        }
//    };

//}]);

 