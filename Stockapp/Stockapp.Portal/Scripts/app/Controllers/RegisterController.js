(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('RegisterController', RegisterController);

    RegisterController.$inject = ['UserService', '$scope', '$location', 'GlobalService', 'PlayerService', 'toaster'];
    function RegisterController(UserService, $scope, $location, GlobalService, PlayerService, toaster) {

        if (GlobalService.GetLoged() === true)
            $location.path(GlobalService.UserIsAdmin() ? '/adminHome' : '/portfolio');

        $scope.submit = function () {
            $scope.isLoading = true;
            var response = UserService.RegisterUser($scope.register)
                .success(function (result) {
                    var response = PlayerService.RegisterPlayer($scope.register, result)
                        .success(function (result) {
                            GlobalService.SetCurrentPerson(result);
                            $scope.isLoading = false;
                            $scope.login();
                            toaster.pop({
                                type: 'success',
                                title: 'Usuario Creado',
                                body: 'Se creo tu usuario correctamente',
                                timeout: 3000
                            });
                        })
                        .error(function (data, status) {
                            $scope.isLoading = false;
                            console.log(data);
                            console.log(status);
                            toaster.pop({
                                type: 'error',
                                title: 'Ups!',
                                body: data.Message,
                                timeout: 3000
                            });
                        });
                    $scope.isLoading = false;
                    $scope.login();
                })
                .error(function (data, status) {
                    $scope.isLoading = false;
                    console.log(data);
                    console.log(status);
                    toaster.pop({
                        type: 'error',
                        title: 'Ups!',
                        body: data.Message,
                        timeout: 3000
                    });
                });
        };

        $scope.login = function () {
            $scope.isLoading = true;
            var response = UserService.Login($scope.register.Email, $scope.register.Password)
                .success(function (result) {
                    $scope.isLoading = false;
                    GlobalService.SetActualUser(result);
                    GlobalService.SetLoged(true);
                    $location.path('/portfolio');
                })
                .error(function (data, status) {
                    $scope.isLoading = false;
                    console.log(data);
                    console.log(status);
                    toaster.pop({
                        type: 'error',
                        title: 'Ups!',
                        body: data.Message,
                        timeout: 3000
                    });
                });
        };
    }
})();