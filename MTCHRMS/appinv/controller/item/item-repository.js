
'use strict';

invModule.factory('itemRepository', [
    '$resource', '$http', function($resource, $http) {

        console.log("item repository ");

        var _getAllItems = function() {
            return $resource('/api/item').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
        };
        
        var _getItemById = function(id) {
            return $resource('/api/item/' + id).query();
        };

        var _addItem = function (item) {
            return $resource('/api/item').save(item);
        };

        var _editItem = function(item) {
            return $http.put('/api/item/' + item.itemId, item);
        };

        return {
            getAllItems: _getAllItems,
            getItemById: _getItemById,
            addItem: _addItem,
            editItem: _editItem
        };

    }
]);
