(function () {
    'use strict';
    angular
        .module('Stockapp')
         .controller('PortfolioController', PortfolioController);

    PortfolioController.$inject = ['GlobalService', '$scope', '$location', 'PlayerService', "NgTableParams"];

    function PortfolioController(GlobalService, $scope, $location, PlayerService, NgTableParams) {

        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === true) $location.path('/adminHome');
        } else
            $location.path('/');

        PlayerService.GetPlayerByUserId(GlobalService.GetActualUser().Id)
                .success(function (player) {
                    GlobalService.SetCurrentPerson(player);
                })
                .error(function (data, status) {
                    $scope.LogError(data, status);
                    toaster.pop({
                        type: 'error',
                        title: 'Ups!',
                        body: data.Message,
                        timeout: 3000
                    });
                });

        $scope.Portfolio = GlobalService.GetCurrentPerson().Portfolio;
        $scope.actions = $scope.Portfolio.AvailableActions;
        for (var i = 0; i < $scope.actions.length; i++) {
            $scope.actions[i].TotalValue = $scope.actions[i].QuantityOfActions * $scope.actions[i].Stock.UnityValue;
            $scope.actions[i].PercentageVariation = $scope.actions[i].PercentageVariation;
        }

        $scope.defaultConfigTableParams = new NgTableParams({}, { dataset: $scope.actions });
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
                dataset: $scope.actions
            };
            return new NgTableParams(initialParams, initialSettings);
        };
    };

})();