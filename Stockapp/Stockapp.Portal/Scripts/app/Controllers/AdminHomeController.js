(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('AdminHomeController', AdminHomeController);

    AdminHomeController.$inject = ['$scope', 'GlobalService', '$location', 'GameSettingsService'];
    function AdminHomeController($scope, GlobalService, $location, GameSettingsService) {
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === false) $location.path('/portfolio');
        } else
            $location.path('/');
    }
})();