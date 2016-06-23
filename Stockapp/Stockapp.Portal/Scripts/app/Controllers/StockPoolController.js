(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('StockPoolController', StockPoolController);

    StockPoolController.$inject = ['$scope', 'GlobalService', '$location', 'StockService', 'NgTableParams'];
    function StockPoolController($scope, GlobalService, $location, StockService, NgTableParams) {
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === false) $location.path('/portfolio');
        } else
            $location.path('/');
        $scope.stocks = [];

        var response = StockService.GetAllStocks()
        .success(function (response) {
            $scope.stocks = response;
        })
        .error(function (data, status) {
            toaster.pop({
                type: 'error',
                title: 'Ups!',
                body: data.Message,
                timeout: 3000
            });
            console.log(data);
            console.log(status);
        });


        $scope.customConfigParams = createUsingFullOptions();

        function createUsingFullOptions() {
            var initialParams = {
                count: 5 // initial page size
            };
            var initialSettings = {
                // page size buttons (right set of buttons in demo)
                counts: [],
                // determines the pager buttons (left set of buttons in demo)
                paginationMaxBlocks: 13,
                paginationMinBlocks: 2,
                dataset: $scope.stocks
            };
            return new NgTableParams(initialParams, initialSettings);
        };
    };
})();