'use strict';
app.controller('indexController', ['$scope', '$location', 'authService', '$rootScope', function ($scope, $location, authService, $rootScope) {

    $rootScope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.logOut = function () {
        authService.logOut();
        $location.path('/home');
    }

    $scope.authentication = authService.authentication;

}]);