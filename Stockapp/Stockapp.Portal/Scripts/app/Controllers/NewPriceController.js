(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('NewPriceController', NewPriceController);

    NewPriceController.$inject = ['$scope', '$location', 'GlobalService', 'toaster', 'StockService', '$filter', 'dateTimeConfig', '$parse', 'datePickerUtils'];
    function NewPriceController($scope, $location, GlobalService, toaster, StockService, $filter, dateTimeConfig, $parse, datePickerUtils) {
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === false) $location.path('/portfolio');
        } else
            $location.path('/');

        $scope.changeDate = moment();
        $scope.realStock = {};
        $scope.selectedStock = {};

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
            $scope.selectedStock = jQuery.extend(true, {}, stock);
        };

        $scope.dateChanged = function (datepicker, value) {
            if (moment() > moment(value))
                $scope.changeDate = moment(value);
        };

        $scope.submit = function () {
            $scope.isLoading = true;
            var response = StockService.UpdateStock({
                Stock: $scope.selectedStock,
                DateOfChange: $scope.changeDate
            })
                .success(function (response) {
                    $scope.realStock = $scope.selectedStock;

                    $filter('filter')
                        ($scope.stocks, { Id: $scope.selectedStock.Id })[0] = $scope.selectedStock;

                    toaster.pop({
                        type: 'success',
                        title: 'Precio Modificado!!',
                        body: "El precio de " + $scope.selectedStock.Code + " fue modificado",
                        timeout: 3000
                    });
                    $scope.isLoading = false;
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
                    $scope.isLoading = false;
                });
        };

        $scope.cancel = function () {
            $scope.selectedStock = $scope.realStock;
        };
    };
})();