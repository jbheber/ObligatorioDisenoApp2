(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('MenuController', MenuController);

    MenuController.$inject = ['$scope', 'GlobalService', '$location', 'GameSettingsService'];
    function MenuController($scope, GlobalService, $location, GameSettingsService) {
        GameSettingsService.LoadGameSettings();

        $scope.logged = false;
        $scope.adminUser = false;
        $scope.userName = "";

        $scope.$on('lgdUpdated', function () {
            $scope.logged = GlobalService.GetLoged();
            $scope.adminUser = GlobalService.UserIsAdmin();
            $scope.userName = $scope.logged ? GlobalService.GetActualUser().Name : "";
        });

        $scope.logOut = function () {
            $location.path('/');
            GlobalService.SetLoged(false);
            GlobalService.ResetResources();
        }
    }
})();