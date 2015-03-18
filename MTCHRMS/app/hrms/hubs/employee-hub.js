
hrmsModule.factory('EmployeeStream', [
    '$rootScope', function ($rootScope) {
        'use strict';
        return {
            on : function(eventName, callback) {
                var connection = $.hubConnection();
                var staffHubProxy = connection.createHubProxy('hrmsStaffHub');
                
                staffHubProxy.on(eventName, function() {
                    var args = arguments;
                    $rootScope.$apply(function() {
                        callback.apply(staffHubProxy, args);
                    });
                });
                connection.start(function () { }).done(function () { });
            }
        };
    }
]);
