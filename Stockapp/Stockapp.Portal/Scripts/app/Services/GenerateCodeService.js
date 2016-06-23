(function () {
    'use strict';

    angular
        .module('Stockapp')
        .factory('GenerateCodeService', GenerateCodeService);

    GenerateCodeService.$inject = ['$http', 'GlobalService'];
    function GenerateCodeService($http, GlobalService) {
        var service = {};

        service.GetCode = function () {
            return $http.post(GlobalService.GetBaseUrl() + 'invitationcode/', GlobalService.GetActualUser());
        };

        return service;
    }

})();