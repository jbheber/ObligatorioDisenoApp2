(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('NewTransactionController', NewTransactionController);

    NewTransactionController.$inject = ['$scope', 'GlobalService', '$location', 'TransactionService', 'StockService', 'toaster'];
    function NewTransactionController($scope, GlobalService, $location, TransactionService, StockService, toaster) {
        var self = this;
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === true) $location.path('/adminHome');
        } else
            $location.path('/');

        $scope.selectedStock = {};
        $scope.stocks = [];
        $scope.quantity = "";
        $scope.moneySpend = 0;
        $scope.isLoading = true;
        self.portfolio = GlobalService.GetCurrentPerson().Portfolio;
        $scope.availableMoney = self.portfolio.AvailableMoney;
        $scope.type = "";

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
            if (stock != null) {
                $scope.realStock = stock;
                $scope.isLoading = false;
            } else
                $scope.isLoading = true;

        };

        $scope.cancel = function () {
            $scope.selectedStock = {};
            $scope.quantity = 0;
            $scope.moneySpend = 0;
        };

        $scope.calculateInput = function () {
            $scope.moneySpend = $scope.quantity * $scope.selectedStock.UnityValue;
        };

        $scope.submit = function () {
            if ($scope.type == 1 && self.validateAvailableMoney() === false) {
                self.popToaster('error', 'Ups!', 'No puedes realizar esta transaccion porque no tienes el dinero disponible');
                return;
            }
            if ($scope.type == 1 && self.validateAvailableStocks() === false) {
                self.popToaster('error', 'Ups!', 'No puedes realizar esta transaccion porque no hay disponible la cantidad de stocks que quieres');
                return;
            }
            var transaction = {
                StockId: $scope.selectedStock.Id,
                StockQuantity: $scope.quantity,
                TotalValue: $scope.moneySpend,
                TransactionDate: moment(),
                PortfolioId: self.portfolio.Id,
                Type: $scope.type
            };
            var response = TransactionService.RegisterTransaction(transaction)
            .success(function (response) {
                self.popToaster('success', 'Transaccion completa', 'Se ralizo correctamente la transaccion');
            })
            .error(function (data, status) {
                self.popToaster('error', 'Ups', data.Message);
            });

            $scope.type = "";
        };

        self.validateAvailableMoney = function () {
            if ($scope.availableMoney < $scope.moneySpend)
                return false;
            return true;
        };

        self.validateAvailableStocks = function () {
            if ($scope.selectedStock.QuantiyOfActions < $scope.quantity)
                return false;
            return true;
        };

        self.popToaster = function (type, title, body) {
            toaster.pop({
                type: type,
                title: title,
                body: body,
                timeout: 5000
            });
        };
    };
})();