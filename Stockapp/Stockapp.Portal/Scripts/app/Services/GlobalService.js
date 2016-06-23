(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('GlobalService', GlobalService);

    function GlobalService($rootScope) {
        var service = {};

        service.resources = {
            actualUser: null,
            logged: false,
            isAdmin: false,
            baseUrl: 'http://localhost:1839/api/',
            gameSettings: null,
            person: null
        };

        service.GetBaseUrl = function() {
            return service.resources.baseUrl;
        };

        service.GetActualUser = function () {
            return service.resources.actualUser;
        };

        service.SetActualUser = function SetActualUser(user) {
            service.resources.isAdmin = user.IsAdmin;
            service.resources.actualUser = user;
        };

        service.GetLoged = function () {
            return service.resources.logged;
        };

        service.SetLoged = function (lgd) {
            service.resources.logged = lgd;
            $rootScope.$broadcast("lgdUpdated");
        };

        service.UserIsAdmin = function () {
            return service.resources.isAdmin;
        };

        service.SetGameSettings = function (settings) {
            service.resources.gameSettings = settings;
        };

        service.SetCurrentPerson = function (person) {
            service.resources.person = person;
        };

        service.GetCurrentPerson = function () {
            return service.resources.person;
        };

        service.ResetResources = function () {
            var gameSettings = service.resources.gameSettings;
            service.resources = {
                actualUser: null,
                logged: false,
                isAdmin: false,
                baseUrl: 'http://localhost:1839/api/',
                gameSettings: gameSettings,
                person: null
            };
        };

        return service; 
    }

})();