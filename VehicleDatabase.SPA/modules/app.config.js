(function (angular) {
    'use strict';
    angular.module('app')
            .config(['$routeProvider', '$locationProvider',
            function ($routeProvider, $locationProvider) {
                $locationProvider.hashPrefix('');
                $routeProvider
                    .when('/', {
                        templateUrl: 'modules/manufacturers/manufacturers-view/manufacturers-view.controller.html',
                        title: 'Manufacturers',
                        controller: 'manufacturersController',
                    })
                    .when('/add-manufacturer', {
                        templateUrl: 'modules/manufacturers/manufacturers-add/manufacturers-add.controller.html',
                        title: 'Add manufacturer',
                        controller: 'manufacturersAddController',
                    })
                    .when('/edit-manufacturer/:id', {
                        templateUrl: 'modules/manufacturers/manufacturers-add/manufacturers-add.controller.html',
                        title: 'Edit manufacturer',
                        controller: 'manufacturersAddController',
                    })
                    .when('/models', {
                        templateUrl: 'modules/models/models-view/models-view.controller.html',
                        title: 'Models',
                        controller: 'modelsController',
                    })
                    .when('/add-model', {
                        templateUrl: 'modules/models/models-add/models-add.controller.html',
                        title: 'Add model',
                        controller: 'modelsAddController',
                    })
                    .when('/edit-model/:id', {
                        templateUrl: 'modules/models/models-add/models-add.controller.html',
                        title: 'Edit model',
                        controller: 'modelsAddController',
                    })
                   .otherwise({ redirectTo: '/' });
            }])
})(angular);