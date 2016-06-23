(function () {
    'use strict';
    var application = angular.module('Stockapp', ['ngRoute', 'ngMaterial', 'ngMessages', 'ngAnimate', 'toaster', 'datePicker', 'angularMoment', 'ngTable','gridshore.c3js.chart']);

    application.config(function ($routeProvider) {
        $routeProvider
        .when('/', {
            templateUrl: 'views/home.html'
        })
        .when('/login', {
            templateUrl: 'views/login.html',
            controller: 'LoginController'
        })
        .when('/register', {
            templateUrl: 'views/register.html',
            controller: 'RegisterController'
        })
        .when('/portfolio', {
            templateUrl: 'views/portfolio.html',
            controller: 'PortfolioController'
        })
        .when('/adminHome', {
            templateUrl: 'views/adminHome.html',
            controller: 'AdminHomeController'
        })
        .when('/edit', {
            templateUrl: 'views/edit.html',
            controller: 'UpdateController'
        })
        .when('/generateCode', {
            templateUrl: 'views/generateInvitationCodes.html',
            controller: 'GenerateInvitatitonCodesController'
        })
        .when('/newPrice', {
            templateUrl: 'views/newPrice.html',
            controller: 'NewPriceController'
        })
        .when('/registerNews', {
            templateUrl: 'views/registerNews.html',
            controller: 'RegisterNewsController'
        })
        .when('/newTransaction', {
            templateUrl: 'views/newTransaction.html',
            controller: 'NewTransactionController'
        })
        .when('/allTransactions', {
            templateUrl: 'views/allTransactions.html',
            controller: 'AllTransactionsController'
        })
        .when('/stockPool', {
            templateUrl: 'views/viewStockPool.html',
            controller: 'StockPoolController'
        })
        .when('/transactionHistory', {
            templateUrl: 'views/transactionHistory.html',
            controller: 'TransactionHistoryController'
        })
        .when('/searchStock', {
            templateUrl: 'views/searchStock.html',
            controller: 'SearchStockController'
        })
        .otherwise({
            redirectTo: '/'
        });
    });
})();