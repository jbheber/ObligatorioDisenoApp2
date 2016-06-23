(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('UserService', UserService);

    UserService.$inject = ['$http', 'GlobalService'];
    function UserService($http, GlobalService) {
        var service = {};

        service.Login = function (email, password) {
            return $http.get(GlobalService.GetBaseUrl() + 'user/signin/' + email + '/' + password + '/');
        };

        service.RegisterUser = function (register) {
            var newUser = {
                User: {
                    Name: register.userName,
                    Email: register.Email,
                    Password: register.Password
                },
                InvitationCode: {
                    Code: register.InvitationCode
                }
            };

            return $http.post(GlobalService.GetBaseUrl() + 'user', newUser);
        }

        return service;
    }

})();