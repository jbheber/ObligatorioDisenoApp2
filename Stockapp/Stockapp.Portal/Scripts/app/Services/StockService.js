(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('StockService', StockService);

    StockService.$inject = ['$http', 'GlobalService'];
    function StockService($http, GlobalService) {
        var service = {};

        service.GetAllStocks = function () {
            return $http.get(GlobalService.GetBaseUrl() + 'stock');
        };

        service.UpdateStock = function (stock) {
            return $http.put(GlobalService.GetBaseUrl() + 'stock', stock);
        };

        service.FindStock = function (searchName, searchString) {
            return $http.get(GlobalService.GetBaseUrl() + 'stock/getfilterstocks/' + searchName + '/' + searchString);
        }

        return service;
    }

})();