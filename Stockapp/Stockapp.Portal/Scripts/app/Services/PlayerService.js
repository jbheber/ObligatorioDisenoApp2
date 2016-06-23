(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('PlayerService', PlayerService);

    PlayerService.$inject = ['$http', 'GlobalService'];
    function PlayerService($http, GlobalService) {
        var service = {};

        service.RegisterPlayer = function (register, user) {
            var newPlayer = {
                CI: register.CI,
                name: register.Name,
                Surname: register.Surname,
                UserId: user.Id,
                Email: register.Email,
                Portfolio: {
                    AvailableMoney: GlobalService.resources.gameSettings.InitialMoney
                }
            };

            return $http.post(GlobalService.GetBaseUrl() + 'player', newPlayer);
        };

        service.GetPlayerByUserId = function (userId) {
            return $http.get(GlobalService.GetBaseUrl() + 'player/' + userId);
        };

        service.UpdatePlayer = function (player) {
            return $http.put(GlobalService.GetBaseUrl() + 'player/', player);
        };

        return service;
    }

})();