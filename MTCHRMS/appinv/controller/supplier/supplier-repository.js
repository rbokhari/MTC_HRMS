
'use strict';

invModule.factory('supplierRepository', ['$resource', '$http', function ($resource, $http) {

    console.log("supplier repository ");


    var _getAllSuppliers = function () {
        return $resource('/api/supplier').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getSupplierById = function (id) {
        return $resource('/api/supplier/' + id).get();
    };

    var _addSupplier = function (supplier) {
        return $resource('/api/supplier').save(supplier);
    };

    var _editSupplier = function (supplier) {
        return $http.put('/api/supplier/' + supplier.supplierId, supplier);
    };

    return {
        getAllSuppliers: _getAllSuppliers,
        getSupplierById: _getSupplierById,
        addSupplier: _addSupplier,
        editSupplier: _editSupplier
    };

}]);
