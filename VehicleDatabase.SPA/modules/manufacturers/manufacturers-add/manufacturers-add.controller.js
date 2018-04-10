(function (angular) {
    'use strict';
    angular.module('app')
        .controller('manufacturersAddController', ['$scope', '$routeParams', '$location', 'manufacturersService',
            function ($scope, $routeParams, $location, manufacturersService) {
                $scope.manufacturer = {};
                var id = $routeParams.id;
                $scope.isEdit = id !== null && id !== undefined;

                if ($scope.isEdit) {
                    manufacturersService.getById(id).then(function (response) {
                        $scope.manufacturer = response.data;
                    });
                }

                //save
                $scope.saveChanges = function () {
                    if ($scope.addManufacturerForm.$valid) {
                        if ($scope.isEdit) {
                            manufacturersService.put($scope.manufacturer.id, $scope.manufacturer).then(function (data) {
                                $location.path('/');
                            });
                        }
                        else {
                            manufacturersService.post($scope.manufacturer).then(function (data) {
                                $location.path('/');
                            });
                        }
                    }
                };
            }]);
})(angular);

