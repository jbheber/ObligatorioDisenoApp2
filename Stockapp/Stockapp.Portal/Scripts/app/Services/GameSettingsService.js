(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('GameSettingsService', GameSettingsService);

    GameSettingsService.$inject = ['$http', 'GlobalService'];
    function GameSettingsService($http, GlobalService) {
        var service = {};

        service.LoadGameSettings = function () {
            //Load Game Settings when log-in
            if (GlobalService.resources.gameSettings == null) {
                $http.get(GlobalService.GetBaseUrl() + 'gamesettings')
                .success(function (result) {
                    GlobalService.SetGameSettings(result);
                })
                .error(function (data, status) {
                    console.log(data);
                    console.log(status);
                });
            }
        };

        return service;
    };

})();