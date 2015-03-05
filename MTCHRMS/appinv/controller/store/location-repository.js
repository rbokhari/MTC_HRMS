
'use strict';

invModule.factory('locationRepository', ['$resource', '$http', function ($resource, $http) {

    console.log("location repository ");


    var _getAllLocations = function () {
        return $resource('/api/storelocation').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getLocationById = function (id) {
        return $resource('/api/storelocation/' + id).get();
    };

    var _addLocation = function (location) {
        return $resource('/api/storelocation').save(location);
    };

    var _editLocation = function (location) {
        return $http.put('/api/storelocation/' + location.storeId, location);
    };

    return {
        getAllLocations: _getAllLocations,
        getLocationById: _getLocationById,
        addLocation: _addLocation,
        editLocation: _editLocation
    };

}]);
