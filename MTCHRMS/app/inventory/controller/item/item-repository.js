
'use strict';

invModule.factory('itemRepository', [
    '$resource', '$http', function($resource, $http) {

        console.log("item repository ");

        var _getAllItems = function() {
            return $resource('/api/item').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
        };

        var _getAllItemSuppliers = function () {
            return $resource('/api/item/ItemSuppliers').query();
        };

        var _getAllItemManufactuers = function () {
            return $resource('/api/item/ItemManufacturers').query();
        };

        var _getAllItemYears = function () {
            return $resource('/api/item/ItemYears').query();
        };

        var _getAllItemDepartments = function () {
            return $resource('/api/item/ItemDepartments').query();
        };

        var _getItemById = function(id) {
            return $resource('/api/item/GetSingleItem/?id=' + id).query();
        };

        var _addItem = function (item) {
            return $resource('/api/item').save(item);
        };

        var _addItemStock = function (stock) {
            return $resource('/api/item/addItemStock').save(stock);
        };

        var _editItem = function(item) {
            return $http.put('/api/item/' + item.itemId, item);
        };

        var _addItemDepartment = function (itemDepartment) {
            return $resource('/api/item/' + itemDepartment.itemId + '/PostItemDepartment').save(itemDepartment);
        };

        var _updateItemDepartment = function (itemDepartment) {
            return $resource('/api/item/' + itemDepartment.itemId + '/UpdateItemDepartment').save(itemDepartment);
        };

        var _deleteItemDepartment = function (itemDepartment) {
            return $resource('/api/item/' + itemDepartment.itemId + '/DeleteItemDepartment').save(itemDepartment);
        };

        var _addItemYear = function (itemYear) {
            return $resource('/api/item/' + itemYear.itemId + '/PostItemYear').save(itemYear);
        };

        var _updateItemYear = function (itemYear) {
            return $resource('/api/item/' + itemYear.itemId + '/UpdateItemYear').save(itemYear);
        };

        var _deleteItemYear = function (itemYear) {
            return $resource('/api/item/' + itemYear.itemId + '/DeleteItemYear').save(itemYear);
        };

        var _addItemSupplier = function (itemSupplier) {
            return $resource('/api/item/' + itemSupplier.itemId + '/PostItemSupplier').save(itemSupplier);
        };

        var _updateItemSupplier = function (itemSupplier) {
            return $resource('/api/item/' + itemSupplier.itemId + '/UpdateItemSupplier').save(itemSupplier);
        };

        var _deleteItemSupplier = function (itemSupplier) {
            return $resource('/api/item/' + itemSupplier.itemId + '/DeleteItemSupplier').save(itemSupplier);
        };

        var _addItemManufacturer = function (itemManufacturer) {
            return $resource('/api/item/' + itemManufacturer.itemId + '/PostItemManufacturer').save(itemManufacturer);
        };

        var _updateItemManufacturer = function (itemManufacturer) {
            return $resource('/api/item/' + itemManufacturer.itemId + '/UpdateItemManufacturer').save(itemManufacturer);
        };

        var _deleteItemManufacturer = function (itemManufacturer) {
            return $resource('/api/item/' + itemManufacturer.itemId + '/DeleteItemManufacturer').save(itemManufacturer);
        };

        var _getItemSearchList = function (item) {
            return $resource('/api/item/GetItemSearch').query(item);
        };


        return {
            getAllItems: _getAllItems,
            getAllItemSuppliers: _getAllItemSuppliers,
            getAllItemManufactuers: _getAllItemManufactuers,
            getAllItemYears: _getAllItemYears,
            getAllItemDepartments: _getAllItemDepartments,
            getItemById: _getItemById,
            addItem: _addItem,
            editItem: _editItem,
            addItemStock:_addItemStock,
            addItemDepartment: _addItemDepartment,
            updateItemDepartment: _updateItemDepartment,
            deleteItemDepartment: _deleteItemDepartment,
            addItemYear: _addItemYear,
            updateItemYear: _updateItemYear,
            deleteItemYear: _deleteItemYear,
            addItemSupplier: _addItemSupplier,
            updateItemSupplier: _updateItemSupplier,
            deleteItemSupplier: _deleteItemSupplier,
            addItemManufacturer: _addItemManufacturer,
            updateItemManufacturer: _updateItemManufacturer,
            deleteItemManufacturer: _deleteItemManufacturer,
            getItemSearchList: _getItemSearchList
        };

    }
]);
