(function () {
    'use strict';
    angular
        .module('Stockapp')
            .controller('GenerateInvitatitonCodesController', GenerateInvitatitonCodesController);

    GenerateInvitatitonCodesController.$inject = ['$scope', 'GlobalService', '$location', 'GenerateCodeService','toaster'];
    function GenerateInvitatitonCodesController($scope, GlobalService, $location, GenerateCodeService, toaster) {
        //Security
        if (GlobalService.GetLoged() === true) {
            if (GlobalService.UserIsAdmin() === false) $location.path('/portfolio');
        } else
            $location.path('/');

        $scope.codes = [];

        $scope.generateCode = function () {

            var response = GenerateCodeService.GetCode()
                    .success(function (result) {
                        $scope.codes.push(result.Code);
                        toaster.pop({
                            type: 'success',
                            title: 'Exito!',
                            body: 'Codigo creado y almacenado',
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
                    });
        };
    }
})();