(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('TransactionService', TransactionService);

    TransactionService.$inject = ['$http', 'GlobalService'];
    function TransactionService($http, GlobalService) {
        var service = {};

        service.RegisterTransaction = function (transaction) {
            return $http.post(GlobalService.GetBaseUrl() + 'transaction/', transaction);
        };

        service.SearchTransaction = function (searchParameters) {
            return $http.get(GlobalService.GetBaseUrl() + 'transaction/' + searchParameters.from + '/' + searchParameters.to + '/' + (searchParameters.stockId || 0) + '/' + searchParameters.type);
        };

        service.SearchUserTransaction = function (searchParameters) {
            return $http.get(GlobalService.GetBaseUrl() + 'transaction/getusertransactions/' + searchParameters.portfolioId + '/' + searchParameters.from + '/' + searchParameters.to + '/' + (searchParameters.stockId || 0));
        };

        return service;
    };

})();