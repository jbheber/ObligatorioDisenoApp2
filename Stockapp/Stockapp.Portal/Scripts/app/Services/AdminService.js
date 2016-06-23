(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('AdminService', AdminService);

    AdminService.$inject = ['$http', 'GlobalService'];
    function AdminService($http, GlobalService) {
        var service = {};

        service.GetAdminByUserId = function (userId) {
            return $http.get(GlobalService.GetBaseUrl() + 'admin/' + userId);
        };

        service.UpdateAdmin = function (admin) {
            return $http.put(GlobalService.GetBaseUrl() + 'admin/', admin);
        };

        return service;
    }

})();