/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('employeeRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllEmployees = function () {
        return $resource('/api/employee').query();
    };

    var _getEmployeeDetailById = function (id) {
        return $resource('/api/employee/' + id).query();
    };

    var _getEmployeeDetailByUserName = function (userName) {
        return $resource('/api/employee/GetEmployeeByUserName/?userName=' + userName).get();
    };

    var _addEmployee = function(employeeDef) {
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

    var _addEmployeeQualification = function(employeeQualification) {
        return $resource('/api/employee/' + employeeQualification.employeeDefId + '/PostEmployeePassport').save(employeeQualification);
    };

    var _addEmployeePreviousEmployment = function(employeePreviousEmployment) {
        return $resource('/api/employee/' + employeePreviousEmployment.employeeDefId + '/PostEmployeePreviousEmployment').save(employeePreviousEmployment);
    };

    var _addEmployeeMarital = function(employeeMaritals) {
        return $resource('/api/employee/' + employeeMaritals.employeeDefId + '/PostEmployeeMarital').save(employeeMaritals);
    };

    var _addEmployeeChild = function (employeeChildrens) {
        
        return $resource('/api/employee/' + employeeChildrens.employeeDefId + '/PostEmployeeChild').save(employeeChildrens);
    };

    var _addEmployeeKin = function(employeeKins) {
        return $resource('/api/employee/' + employeeKins.employeeDefId + '/PostEmployeeKin').save(employeeKins);
    };

    var _addEmployeeImage = function (employee) {
        return $resource('/api/employee/upload').save(employee);
    };

    return {
        getAllEmployees: _getAllEmployees,
        getEmployeeDetailById: _getEmployeeDetailById,
        getEmployeeDetailByUserName: _getEmployeeDetailByUserName,
        addEmployee: _addEmployee,
        editEmployee: _editEmployee,
        addEmployeePassport: _addEmployeePassport,
        deleteEmployeePassport: _deleteEmployeePassport,
        addEmployeeVisa: _addEmployeeVisa,
        deleteEmployeeVisa: _deleteEmployeeVisa,
        addEmployeeQualification: _addEmployeeQualification,
        addEmployeePreviousEmployment: _addEmployeePreviousEmployment,
        addEmployeeMarital: _addEmployeeMarital,
        addEmployeeChild: _addEmployeeChild,
        addEmployeeKin: _addEmployeeKin,
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

