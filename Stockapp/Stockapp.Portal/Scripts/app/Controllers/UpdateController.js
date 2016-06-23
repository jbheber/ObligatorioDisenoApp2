(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('UpdateController', UpdateController);

    UpdateController.$inject = ['$scope', '$location', 'GlobalService', 'PlayerService', 'AdminService', 'toaster'];
    function UpdateController($scope, $location, GlobalService, PlayerService, AdminService, toaster) {

        if (GlobalService.GetLoged() === false)
            $location.path('/');

        //deep copy of the person.
        var clonePerson = jQuery.extend(true, {}, GlobalService.GetCurrentPerson());
        $scope.Person = clonePerson;

        $scope.submit = function () {
            $scope.isLoading = true;
            var updatedPerson = $scope.Person;
            updatedPerson.Email = updatedPerson.User.Email;

            if (updatedPerson.User.IsAdmin) {
                var response = AdminService.UpdateAdmin(updatedPerson)
                    .success(function (result) {
                        GlobalService.SetCurrentPerson(updatedPerson);
                        $scope.isLoading = false;
                        toaster.pop({
                            type: 'success',
                            title: 'Editado',
                            body: 'El usuario ' + updatedPerson.User.Name + ' fue editado',
                            timeout: 3000
                        });
                    })
                    .error(function (data, status) {
                        toaster.pop({
                            type: 'error',
                            title: 'Ups!',
                            body: data.Message,
                            timeout: 3000
                        });
                        $scope.isLoading = false;
                    });
            } else {
                var response = PlayerService.UpdatePlayer(updatedPerson)
                    .success(function (result) {
                        GlobalService.SetCurrentPerson(updatedPerson);
                        $scope.isLoading = false;
                        toaster.pop({
                            type: 'success',
                            title: 'Editado',
                            body: 'El usuario ' + updatedPerson.User.Name + ' fue editado',
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
            }
        };
    }
})();