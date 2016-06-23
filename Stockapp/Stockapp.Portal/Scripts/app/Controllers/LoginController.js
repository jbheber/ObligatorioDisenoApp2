(function () {
    'use strict';
    angular
        .module('Stockapp')
         .controller('LoginController', LoginController);

    LoginController.$inject = ['GlobalService', 'UserService', '$scope', '$location', 'PlayerService', 'AdminService', 'toaster'];

    function LoginController(GlobalService, UserService, $scope, $location, PlayerService, AdminService, toaster) {

        if (GlobalService.GetLoged() === true)
            $location.path(GlobalService.UserIsAdmin() ? '/adminHome' : '/portfolio');

        $scope.LogError = function (data, status) {
            console.log(data);
            console.log(status);
        };

        $scope.logn = {};

        $scope.login = function () {
            $scope.isLoading = true;
            var response = UserService.Login($scope.logn.Email, $scope.logn.Password)
                .success(function (result) {
                    $scope.isLoading = false;
                    GlobalService.SetActualUser(result);
                    if (GlobalService.UserIsAdmin()) {
                        AdminService.GetAdminByUserId(result.Id)
                        .success(function (admin) {
                            GlobalService.SetCurrentPerson(admin);
                            toaster.pop({
                                type: 'success',
                                title: 'Inicio de Sesion',
                                body: 'Se inicio sesion correctamente',
                                timeout: 3000
                            });
                            $location.path('/adminHome');
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
                    } else {
                        PlayerService.GetPlayerByUserId(result.Id)
                        .success(function (player) {
                            GlobalService.SetCurrentPerson(player);
                            toaster.pop({
                                type: 'success',
                                title: 'Inicio de Sesion',
                                body: 'Se inicio sesion correctamente',
                                timeout: 3000
                            });
                            $location.path('/portfolio');
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
                    }
                    GlobalService.SetLoged(true);
                }).error(function (data, status) {
                    $scope.isLoading = false;
                    $scope.LogError(data, status);
                    toaster.pop({
                        type: 'error',
                        title: 'Ups!',
                        body: data.Message,
                        timeout: 3000
                    });
                });
        };
    };

})();