(function () {
    'use strict';

    angular
        .module('Stockapp')
        .directive('validatePassword', ValidatePassword)
        .directive('validateCi', ValidateCI)
        .directive('validatePrice', ValidateStockPrice)
        .directive('validateDate', ValidateDate);

    function ValidatePassword() {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, mCtrl) {
                function validatePassword(value) {

                    mCtrl.$setValidity('length', true);
                    mCtrl.$setValidity('letter', true);
                    mCtrl.$setValidity('number', true);

                    var matches = value.match(/\d+/g);
                    if (matches == null)
                        mCtrl.$setValidity('number', false);

                    if (value.length < 6)
                        mCtrl.$setValidity('length', false);

                    if (value.match(/[a-z]/i) == null)
                        mCtrl.$setValidity('letter', false);

                    return value;
                }
                mCtrl.$parsers.push(validatePassword);
            }
        };
    };
    function ValidateCI() {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, mCtrl) {
                function validateCI(value) {

                    mCtrl.$setValidity('length', true);
                    mCtrl.$setValidity('letter', true);

                    var length = value.toString().length
                    if (length < 7 || length > 8)
                        mCtrl.$setValidity('length', false);

                    if (isNaN(value))
                        mCtrl.$setValidity('letter', false);

                    return value;
                }
                mCtrl.$parsers.push(validateCI);
            }
        };
    };
    function ValidateCode() {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, mCtrl) {
                function validatePassword(value) {

                    mCtrl.$setValidity('length', true);
                    mCtrl.$setValidity('letter', true);
                    mCtrl.$setValidity('number', true);

                    var matches = value.match(/\d+/g);
                    if (matches == null)
                        mCtrl.$setValidity('number', false);

                    if (value.length < 8)
                        mCtrl.$setValidity('length', false);

                    if (value.match(/[a-z]/i) == null)
                        mCtrl.$setValidity('letter', false);

                    return value;
                }
                mCtrl.$parsers.push(validatePassword);
            }
        };
    };
    function ValidateStockPrice() {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, mCtrl) {
                function validateStockPrice(value) {

                    mCtrl.$setValidity('negative', true);
                    mCtrl.$setValidity('letter', true);

                    if (value < 1)
                        mCtrl.$setValidity('negative', false);

                    if (isNaN(value))
                        mCtrl.$setValidity('letter', false);

                    return value;
                }
                mCtrl.$parsers.push(validateStockPrice);
            }
        };
    };
    function ValidateDate() {
        return {
            require: 'ngModel',
            link: function (scope, element, attr, mCtrl) {
                function validateDate(value) {

                    mCtrl.$setValidity('date', true);

                    if (moment() < moment(value))
                        mCtrl.$setValidity('date', false);

                    return value;
                }
                mCtrl.$parsers.push(validateDate);
            }
        };
    };

})();