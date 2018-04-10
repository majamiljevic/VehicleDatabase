﻿(function (angular) {
    'use strict';
    angular.module('app')
        .controller('manufacturersController', ['$scope', '$location', '$uibModal', 'manufacturersService',
            function ($scope, $location, $uibModal, manufacturersService) {

                $scope.manufacturers = [];
                $scope.searchString = "";
                $scope.selectedManufacturersIds = [];
                $scope.reverseSort = false;
                $scope.page = 1;
                $scope.sortOrder = "name";
                $scope.selectAll = false;

                //get data
                $scope.getData = function () {
                    $scope.selectedManufacturersIds = [];
                    $scope.selectAll = false;
                    var sortOrder = $scope.sortOrder;
                    return manufacturersService.get($scope.page, $scope.searchString, sortOrder).then(function (response) {
                        $scope.totalItemsCount = response.data.totalCount;
                        $scope.manufacturers = response.data.makes;
                    })
                };

                $scope.getData();

                //search
                $scope.searchReload = function () {
                    $scope.page = 1;
                    $scope.getData();
                };

                //sort
                $scope.setSorting = function (orderByField, reverseSort) {
                    event.preventDefault();
                    var sortingKey = orderByField;
                    $scope.reverseSort = reverseSort;
                    $scope.sortOrder = !reverseSort ? sortingKey : sortingKey + '_desc';
                    $scope.getData();
                };

                //pagination
                $scope.pageChanged = function () {
                    $scope.getData();
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
                        templateUrl: 'modules/common/components/delete/delete.controller.html',
                        controller: 'deleteController',
                        resolve: {
                            record: function () { return manufacturer; }
                        }
                    });
                    $scope.modalInstance.result.then(function (selectedItem) {
                        manufacturersService.delete(selectedItem.id).then(function () {
                            $scope.getData();
                        });
                    }, function () { console.log("canceled") });
                };

                //show delete button
                $scope.manufacturerChecked = function (manufacturer) {
                    $scope.selectedManufacturersIds = [];
                    angular.forEach($scope.manufacturers, function (manufacturer) {
                        if (manufacturer.checked) {
                            $scope.selectedManufacturersIds.push(manufacturer.id)
                        }
                    })
                    $scope.selectAll = $scope.selectedManufacturersIds.length === $scope.manufacturers.length;
                };

                //select all
                $scope.checkAll = function () {
                    $scope.selectedManufacturersIds = [];
                    angular.forEach($scope.manufacturers, function (manufacturer) {
                        manufacturer.checked = $scope.selectAll;
                        if ($scope.selectAll) {
                            $scope.selectedManufacturersIds.push(manufacturer.id)
                        }
                    })
                };

                //delete multiple records
                $scope.deleteMultipleRecords = function () {
                    $scope.modalInstanceBatch = $uibModal.open({
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'modules/common/components/delete/delete.controller.html',
                        controller: 'deleteController',
                        resolve: {
                            record: function () { return $scope.selectedManufacturersIds; }
                        }
                    });
                    $scope.modalInstanceBatch.result.then(function (selectedItems) {
                        manufacturersService.deleteBatch(selectedItems).then(function () {
                            $scope.getData();
                        })
                    }, function () { console.log("canceled") });
                };
            }]);
})(angular);
