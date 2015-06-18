
'use strict';

invModule.factory('manufacturerRepository', ['$resource', '$http', function ($resource, $http) {

    console.log("manufacturer repository ");


    var _getAllManufacturers = function () {
        return $resource('/api/manufacturer').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getManufacturerById = function (id) {
        return $resource('/api/manufacturer/' + id).query();
    };

    var _addManufacturer = function (manufacturer) {
        return $resource('/api/manufacturer').save(manufacturer);
    };

    var _editManufacturer = function (manufacturer) {
        return $http.put('/api/manufacturer/' + manufacturer.manufacturerId, manufacturer);
    };

    var _addManufacturerContact = function (manufacturerContact) {
        return $resource('/api/manufacturer/' + manufacturerContact.manufacturerId + '/PostManufacturerContact').save(manufacturerContact);
    };

    var _addManufacturerContract = function (manufacturerContract) {
        console.log(manufacturerContract);
        return $resource('/api/manufacturer/' + manufacturerContract.manufacturerId + '/PostManufacturerContract').save(manufacturerContract);
    };


    return {
        getAllManufacturers: _getAllManufacturers,
        getManufacturerById: _getManufacturerById,
        addManufacturer: _addManufacturer,
        editManufacturer: _editManufacturer,
        addManufacturerContact: _addManufacturerContact,
        addManufacturerContract: _addManufacturerContract
    };

}]);
