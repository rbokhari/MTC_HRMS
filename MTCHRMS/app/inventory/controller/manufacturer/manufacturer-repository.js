
'use strict';

invModule.factory('manufacturerRepository', ['$resource', '$http', function ($resource, $http) {

    console.log("manufacturer repository ");


    var _getAllManufacturers = function () {
        return $resource('/api/manufacturer').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getManufacturerById = function (id) {
        return $resource('/api/manufacturer/' + id).get();
    };

    var _addManufacturer = function (manufacturer) {
        return $resource('/api/manufacturer').save(manufacturer);
    };

    var _editManufacturer = function (manufacturer) {
        return $http.put('/api/manufacturer/' + manufacturer.manufacturerId, manufacturer);
    };


    return {
        getAllManufacturers: _getAllManufacturers,
        getManufacturerById: _getManufacturerById,
        addManufacturer: _addManufacturer,
        editManufacturer: _editManufacturer
    };

}]);
