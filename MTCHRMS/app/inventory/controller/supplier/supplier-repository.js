
'use strict';

invModule.factory('supplierRepository', ['$resource', '$http', function ($resource, $http) {

    console.log("supplier repository ");


    var _getAllSuppliers = function () {
        return $resource('/api/supplier').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getSupplierById = function (id) {
        return $resource('/api/supplier/' + id).query();
    };

    var _addSupplier = function (supplier) {
        return $resource('/api/supplier').save(supplier);
    };

    var _editSupplier = function (supplier) {
        return $http.put('/api/supplier/' + supplier.supplierId, supplier);
    };

    var _addSupplierContact = function (supplierContact) {
        return $resource('/api/supplier/' + supplierContact.supplierId + '/PostSupplierContact').save(supplierContact);
    };

    var _addSupplierContract = function (supplierContract) {
        console.log(supplierContract);
        return $resource('/api/supplier/' + supplierContract.supplierId + '/PostSupplierContract').save(supplierContract);
    };

    return {
        getAllSuppliers: _getAllSuppliers,
        getSupplierById: _getSupplierById,
        addSupplier: _addSupplier,
        editSupplier: _editSupplier,
        addSupplierContact: _addSupplierContact,
        addSupplierContract: _addSupplierContract
    };

}]);
