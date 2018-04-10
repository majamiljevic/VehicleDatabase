(function (angular) {
    'use strict';
    angular.module('app')
        .service('modelsService', ['$http',
            function ($http) {
                var baseUrl = 'http://vehicle.local/api/models/';

                //get
                this.get = function get(page, searchString, sortOrder, makeId) {
                    return $http.get(baseUrl + '?page=' + page + '&searchString=' + searchString + '&sortOrder=' + sortOrder + '&makeId=' + makeId);
                };

                //add
                this.post = function post(data) {
                    var req = {
                        method: 'POST',
                        url: baseUrl,
                        data: data
                    };
                    return $http(req);
                };

                //edit
                this.put = function put(id, data) {
                    var req = {
                        method: 'PUT',
                        url: baseUrl + id,
                        data: data
                    };
                    return $http(req);
                };

                //delete
                this.delete = function deleteModel(id) {
                    return $http.delete(baseUrl + id);
                };

                //get by id
                this.getById = function getById(id) {
                    return $http.get(baseUrl + id);
                };

                //delete multiple records
                this.deleteBatch = function deleteMultipleRecords(selectedItems) {
                    var req = {
                        method: 'DELETE',
                        url: baseUrl + 'batch',
                        headers: {
                            'Content-Type': 'application/json'
                        },
                        data: selectedItems
                    };
                    return $http(req);
                };
            }
        ]);
})(angular);