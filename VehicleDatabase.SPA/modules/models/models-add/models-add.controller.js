(function (angular) {
    'use strict';
    angular.module('app')
    .controller('modelsAddController', ['$scope', '$routeParams', '$location', 'modelsService', 'manufacturersService',
        function ($scope, $routeParams, $location, modelsService, manufacturersService) {

            $scope.model = {};
            var id = $routeParams.id;
            $scope.isEdit = id !== null && id !== undefined;

            if ($scope.isEdit) {
                modelsService.getById(id).then(function (response) {
                    $scope.model = response.data;
                    $scope.manufacturer = {
                        id: response.data.make.id,
                        text: response.data.make.name
                    }
                });
            };

            //save
            $scope.saveChanges = function () {
                if ($scope.addModelForm.$valid) {
                    $scope.model.makeId = $scope.manufacturer.id;
                    if ($scope.isEdit) {
                        modelsService.put($scope.model.id, $scope.model).then(function (data) {
                            $location.path('/models');
                        });
                    }
                    else {
                        modelsService.post($scope.model).then(function (data) {
                            $location.path('/models');
                        });
                    }
                }
            };
        }
    ]);
})(angular);