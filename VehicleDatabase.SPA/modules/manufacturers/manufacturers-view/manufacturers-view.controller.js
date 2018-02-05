(function (angular) {
    'use strict';
    angular.module('app')
        .controller('manufacturersController', ['$scope', '$location', '$uibModal', 'NgTableParams', 'manufacturersService',
            function ($scope, $location, $uibModal, NgTableParams, manufacturersService) {

                $scope.manufacturers = [];
                $scope.searchString = "";

                $scope.manufacturersTable = new NgTableParams({
                    sorting: { name: "asc" }
                }, {
                    // page size buttons (right set of buttons)
                    counts: [],
                    // determines the pager buttons (left set of buttons)
                    paginationMaxBlocks: 5,
                    paginationMinBlocks: 0,
                    getData: function (params) {

                        var sorting = params.sorting();
                        var sortingKey = Object.keys(sorting)[0];
                        var sortingValue = sorting[sortingKey];
                        var sortOrder = sortingValue === 'asc' ? sortingKey : sortingKey + '_' + sortingValue;
                        return manufacturersService.get(params.url().page, $scope.searchString, sortOrder).then(function (response) {
                            params.total(response.data.totalCount);

                            $scope.manufacturers = response.data.makes;
                            return response.data.makes;
                        });

                    }
                });

                //search
                $scope.searchReload = function () {
                    $scope.manufacturersTable.page(1);
                    $scope.manufacturersTable.reload();
                };


                //add
                function goToAddManufacturer() {
                    $location.path('add-manufacturer');
                };

                $scope.addManufacturerButton = goToAddManufacturer;

                //edit
                function goToEditManufacturer(manufacturer) {
                    $location.path('edit-manufacturer/' + manufacturer.id);
                };

                $scope.editManufacturer = goToEditManufacturer;


                //delete modal
                $scope.deleteManufacturerModal = function (manufacturer) {
                    $scope.modalInstance = $uibModal.open({
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'modules/manufacturers/manufacturers-delete/manufacturers-delete.controller.html',
                        controller: 'deleteManufacturerController',
                        resolve: {
                            manufacturer
                        }
                    });

                    $scope.modalInstance.result.then(function (selectedItem) {
                        manufacturersService.delete(selectedItem.id).then(function () {
                            $scope.manufacturersTable.reload();
                        });
                    });

                };

            }]);
})(angular);