(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('RegisterNewsController', RegisterNewsController);

    RegisterNewsController.$inject = ['$scope', 'GlobalService', '$location', 'StockService', 'toaster', 'StockNewsService', 'dateTimeConfig', '$parse', 'datePickerUtils'];
    function RegisterNewsController($scope, GlobalService, $location, StockService, toaster, StockNewsService, dateTimeConfig, $parse, datePickerUtils) {
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === false) $location.path('/portfolio');
        } else
            $location.path('/');

        $scope.stocks = [];
        $scope.selectedStocks = [];
        $scope.newsBody = "";
        $scope.selectedStock = null;
        $scope.isLoading = true;
        $scope.publicationDate = moment();

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

        $scope.addStock = function () {
            if ($scope.selectedStock !== null) {
                $scope.selectedStocks.push($scope.selectedStock)
                var index = $scope.stocks.indexOf($scope.selectedStock);
                $scope.stocks.splice(index, 1);
            }
            $scope.selectedStock = null;
            $scope.isLoading = false;
        };

        $scope.removeStock = function (index) {
            var stock = $scope.selectedStocks[index];
            $scope.selectedStocks.splice(index, 1);
            $scope.stocks.push(stock);
            if( $scope.selectedStocks.length == 0)
                $scope.isLoading = true;
        };

        $scope.dateChanged = function (datepicker, value) {
            if (moment() > moment(value))
                $scope.publicationDate = moment(value);
        };

        $scope.submit = function () {
            $scope.isLoading = true;
            var stockNews = {
                ReferencedStocks: $scope.selectedStocks,
                PublicationDate: $scope.publicationDate,
                Title: $scope.newsTitle,
                Content: $scope.newsBody
            };
            var response = StockNewsService.RegisterStockNews(stockNews)
            .success(function (response) {
                toaster.pop({
                    type: 'success',
                    title: 'Noticia agregada',
                    body: 'Se agrego correctamente la noticia',
                    timeout: 3000
                });
                $location.path('/adminHome');
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
        };
    };
})();