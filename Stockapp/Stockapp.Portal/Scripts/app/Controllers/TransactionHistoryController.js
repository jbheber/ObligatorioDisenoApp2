(function () {
    'use strict';
    angular
        .module('Stockapp')
         .controller('TransactionHistoryController', TransactionHistoryController);

    TransactionHistoryController.$inject = ['GlobalService', '$scope', '$location', 'TransactionService', "NgTableParams", 'dateTimeConfig', '$parse', 'datePickerUtils', 'toaster', 'StockService'];

    function TransactionHistoryController(GlobalService, $scope, $location, TransactionService, NgTableParams, dateTimeConfig, $parse, datePickerUtils, toaster, StockService) {

        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === true) $location.path('/adminHome');
        } else
            $location.path('/');

        $scope.Portfolio = GlobalService.GetCurrentPerson().Portfolio;
        $scope.transactions = [];
        $scope.selectedStock = {};
        $scope.fromDate = moment().add(-7, 'days');
        $scope.toDate = moment();
        $scope.stocks = [];

        var response = StockService.GetAllStocks()
        .success(function (response) {
            response.push({
                Code: 'Ninguno',
                Id: 0
            });
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
            $scope.selectedStock = stock;
        };

        $scope.fromDateChanged = function (datepicker, value) {
            if (moment() > moment(value))
                $scope.fromDate = moment(value);
        };

        $scope.toDateChanged = function (datepicker, value) {
            if (moment() > moment(value))
                $scope.toDate = moment(value);
        };

        $scope.submit = function () {
            var callback = TransactionService.SearchUserTransaction({
                from: $scope.fromDate.format('YYYY-MM-DD'),
                to: $scope.toDate.format('YYYY-MM-DD'),
                stockId: $scope.selectedStock.Id,
                portfolioId: $scope.Portfolio.Id
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
            });
        };
        $scope.defaultConfigTableParams = new NgTableParams({}, { dataset: $scope.transactions });
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
                dataset: $scope.transactions
            };
            return new NgTableParams(initialParams, initialSettings);
        };
    };

})();