(function (angular) {
    'use strict';
    angular.module('app')
    .controller('deleteController', ['$scope', '$uibModalInstance', 'record',
        function ($scope, $uibModalInstance, record) {
            $scope.cancelModal = function () {
                $uibModalInstance.dismiss('close');
            }
            $scope.deleteConfirmed = function () {
                $uibModalInstance.close(record);
            }
        }
    ]);
})(angular);