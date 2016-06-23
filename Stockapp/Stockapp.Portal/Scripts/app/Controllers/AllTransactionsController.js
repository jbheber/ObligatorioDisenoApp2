(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('AllTransactionsController', AllTransactionsController);

    AllTransactionsController.$inject = ['$scope', 'GlobalService', '$location', 'StockService', 'TransactionService', 'dateTimeConfig', '$parse', 'datePickerUtils', 'toaster',"NgTableParams"];
    function AllTransactionsController($scope, GlobalService, $location, StockService, TransactionService, dateTimeConfig, $parse, datePickerUtils, toaster, NgTableParams) {
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === false) $location.path('/portfolio');
        } else
            $location.path('/');

        $scope.fromDate = moment().add(-7, 'days');
        $scope.toDate = moment();
        $scope.selectedStock = {};
        $scope.type = "";
        $scope.transactions = [];
        $scope.defaultConfigTableParams = new NgTableParams({}, { dataset: $scope.transactions });
        $scope.customConfigParams = createUsingFullOptions();

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

        $scope.changedStock = function (stock) {
            $scope.realStock = stock;
        };

        $scope.fromDateChanged = function (datepicker, value) {
            if (moment() > moment(value))
                $scope.fromDate = moment(value);
        };

        $scope.toDateChanged = function (datepicker, value) {
            if (moment() > moment(value))
                $scope.toDate = moment(value);
        };

        $scope.changedType = function (type){
            $scope.type = type;
        };

        $scope.submit = function () {
            var callback = TransactionService.SearchTransaction({
                from: $scope.fromDate.format('YYYY-MM-DD'),
                to: $scope.toDate.format('YYYY-MM-DD'),
                stockId: $scope.selectedStock.Id,
                type: $scope.type
            })
            .success(function (response) {
                for (var i = 0; i < response.length; i++)
                    response[i].TransactionDate = moment(response[i].TransactionDate).format('DD/MM/YYYY');
                $scope.transactions = response;
                $scope.customConfigParams.dataset = response;
                $scope.customConfigParams.reload();

            })
            .error(function (data, status) {
                toaster.pop({
                    type: 'error',
                    title: 'Ups!',
                    body: data.Message,
                    timeout: 3000
                });
            })
        };


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
                dataset: $scope.transactions
            };
            return new NgTableParams(initialParams, initialSettings);
        };
    };
})();
