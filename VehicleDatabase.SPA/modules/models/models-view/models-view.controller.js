(function (angular) {
    'use strict';
    angular.module('app')
        .controller('modelsController', ['$scope', '$location', '$uibModal', 'modelsService',
            function ($scope, $location, $uibModal, modelsService) {

                $scope.models = [];
                $scope.searchString = "";
                $scope.selectedModelsIds = [];
                $scope.selectAll = false;
                $scope.reverseSort = false;
                $scope.page = 1;
                $scope.sortOrder = "name";

                //get data
                $scope.getData = function () {
                    $scope.selectedModelsIds = [];
                    $scope.selectAll = false;
                    var sortOrder = $scope.sortOrder;
                    var makeId = $scope.manufacturer ? $scope.manufacturer.id : undefined;

                    return modelsService.get($scope.page, $scope.searchString, sortOrder, makeId).then(function (response) {
                        $scope.totalItemsCount = response.data.totalCount;
                        $scope.models = response.data.models;
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
                function goToAddModel() {
                    $location.path('add-model');
                };

                $scope.addModelButton = goToAddModel;

                //edit
                function goToEditModel(model) {
                    $location.path('edit-model/' + model.id);
                };

                $scope.editModel = goToEditModel;

                //delete
                $scope.deleteModelModal = function (model) {
                    $scope.modalInstance = $uibModal.open({
                        ariaLabelledBy: 'modal-title',
                        ariaDescribedBy: 'modal-body',
                        templateUrl: 'modules/common/components/delete/delete.controller.html',
                        controller: 'deleteController',
                        resolve: {
                            record: function () { return model; }
                        }
                    });
                    $scope.modalInstance.result.then(function (selectedItem) {
                        modelsService.delete(selectedItem.id).then(function () {
                            $scope.getData();
                        });
                    }, function () { console.log("canceled") });
                };

                //show delete button
                $scope.modelChecked = function (model) {
                    $scope.selectedModelsIds = [];
                    angular.forEach($scope.models, function (model) {
                        if (model.checked) {
                            $scope.selectedModelsIds.push(model.id)
                        }
                    })
                    $scope.selectAll = $scope.selectedModelsIds.length === $scope.models.length;
                };

                //select all
                $scope.checkAll = function () {
                    $scope.selectedModelsIds = [];
                    angular.forEach($scope.models, function (model) {
                        model.checked = $scope.selectAll;
                        if ($scope.selectAll) {
                            $scope.selectedModelsIds.push(model.id)
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
                            record: function () { return $scope.selectedModelsIds; }
                        }
                    });
                    $scope.modalInstanceBatch.result.then(function (selectedItems) {
                        modelsService.deleteBatch(selectedItems).then(function () {
                            $scope.getData();
                            $scope.modelSelected();
                        })
                    }, function () { console.log("canceled") });
                };
            }]);
})(angular);