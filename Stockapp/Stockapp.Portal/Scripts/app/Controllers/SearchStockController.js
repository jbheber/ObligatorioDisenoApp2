(function () {
    'use strict';
    angular
        .module('Stockapp')
         .controller('SearchStockController', SearchStockController);

    SearchStockController.$inject = ['GlobalService', '$scope', '$location', "NgTableParams", 'StockService', 'toaster'];

    function SearchStockController(GlobalService, $scope, $location, NgTableParams, StockService, toaster) {

        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === true) $location.path('/adminHome');
        } else
            $location.path('/');

        $scope.searchName = "";
        $scope.searchDescription = "";
        $scope.stocks = [];
        $scope.news = [];
        $scope.customConfigParams = createUsingFullOptions($scope.stocks);
        $scope.configNews = createUsingFullOptions($scope.news);


        $scope.submit = function () {
            var response = StockService.FindStock($scope.searchName, $scope.searchDescription)
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
            })
        };

        function createUsingFullOptions(data) {
            var initialParams = {
                count: 5 // initial page size
            };
            var initialSettings = {
                // page size buttons (right set of buttons in demo)
                counts: [],
                // determines the pager buttons (left set of buttons in demo)
                paginationMaxBlocks: 13,
                paginationMinBlocks: 2,
                dataset: data
            };
            return new NgTableParams(initialParams, initialSettings);
        };

        $scope.generateChart = function (stock) {
            $("#chart").html("");
            var dates = ['x'];
            var values = ['Precio'];
            $scope.currentPrice = stock.UnityValue;
            $scope.quantity = stock.QuantiyOfActions;
            if (stock.StockHistory != null) {
                var length = Math.min(stock.StockHistory.length, 20);
                for (var i = 0; i < length; i++) {
                    dates.push(moment(stock.StockHistory[i].DateOfChange));
                    values.push(stock.StockHistory[i].RecordedValue);
                }
            }
            if (stock.StockNews != null) {
                for (var i = 0; i < stock.StockNews.length; i++) {
                    stock.StockNews[i].PublicationDate = moment(stock.StockNews[i].PublicationDate).format('DD/MM/YYYY');
                }
            }
            $scope.news = stock.StockNews;
            var chart = c3.generate({
                bindto: '#chart',
                data: {
                    x: 'x',
                    columns: [
                        dates,
                        values
                    ]
                },
                axis: {
                    y: {
                        label: { // ADD
                            text: 'Cotización',
                            position: 'outer-middle'
                        }
                    },
                    x: {
                        type: 'timeseries',
                        tick: {
                            format: '%Y-%m-%d %H:%M:%S'
                        },
                        label: { // ADD
                            text: 'Fecha',
                            position: 'outer-middle'
                        }
                    }
                }
            });
        };
    };

})();