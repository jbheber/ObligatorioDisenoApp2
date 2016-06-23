(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('StockNewsService', StockNewsService);

    StockNewsService.$inject = ['$http', 'GlobalService'];
    function StockNewsService($http, GlobalService) {
        var service = {};

        service.RegisterStockNews = function (stockNews) {
            return $http.post(GlobalService.GetBaseUrl() + 'stocknews', stockNews);
        };

        return service;
    }

})();