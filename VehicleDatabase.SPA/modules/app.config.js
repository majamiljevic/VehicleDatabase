(function (angular) {
    'use strict';
    angular.module('app')
            .config(['$routeProvider',
            function ($routeProvider) {
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
                   .otherwise({ redirectTo: '/' });
            }])
})(angular);