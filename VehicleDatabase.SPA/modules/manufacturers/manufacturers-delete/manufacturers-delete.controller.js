(function (angular) {
    'use strict';
    angular.module('app')
    .controller('deleteManufacturerController', ['$scope', '$uibModalInstance', 'manufacturer',
        function ($scope, $uibModalInstance, manufacturer) {

            $scope.cancelModal = function () {
                $uibModalInstance.dismiss('close');
            }
            $scope.deleteManufacturer = function () {
                $uibModalInstance.close(manufacturer);
            }
        }
    ]);
})(angular);