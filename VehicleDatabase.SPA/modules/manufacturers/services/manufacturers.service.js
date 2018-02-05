(function (angular) {
    'use strict';
    angular.module('app')
        .service('manufacturersService', ['$http',
            function ($http) {
                var baseUrl = 'http://vehicle.local/api/manufacturers/';

                //get
                this.get = function get(page, searchString, sortOrder) {
                    return $http.get(baseUrl + '?page=' + page + '&searchString=' + searchString + '&sortOrder=' + sortOrder);
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
                this.delete = function deleteManufacturer(id) {
                    return $http.delete(baseUrl + id);
                };

                //get by id
                this.getById = function getById(id) {
                    return $http.get(baseUrl + id);
                };
            }
        ]);

})(angular);