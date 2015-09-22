/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular.min.js" />
/// <reference path="D:\MyData\Projects\MTC_HRMS\MTCHRMS\MTCHRMS\Scripts/angular-resource.min.js" />
/// <reference path="../../module/hrms-module.js" />

'use strict';

hrmsModule.factory('ticketRepository', ['$resource', '$http', function ($resource, $http) {

    var _getAllTickets = function() {
        return $resource('/api/ticket').query(); // can use get() instead of query(), but using query() because it except to return back array of objects
    };

    var _getTicketById = function(id) {
        return $resource('/api/ticket/' + id).get();
    };

    var _addTicket = function (ticket) {
        return $resource('/api/ticket').save(ticket);
    };

    var _editTicket = function (ticket) {
        return $http.put('/api/ticket/' + ticket.ticketId, ticket);
    };

    return {
        getAllTickets: _getAllTickets,
        getTicketById: _getTicketById,
        addTicket: _addTicket,
        editTicket: _editTicket
    };

}]);

