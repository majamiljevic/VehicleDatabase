(function (angular) {
    'use strict';
    angular.module('app')
    .directive('manufacturersDropdown', [
        function () {
            return {
                restrict: 'E',
                scope: {
                    manufacturer: '='
                },
                templateUrl: 'modules/common/directives/manufacturers-dropdown/manufacturers-dropdown.directive.html',
                controller: ['$scope', 'manufacturersService', function ($scope, manufacturersService) {                    
                    $scope.select2options = function (options) {
                        var s2options = {

                            allowClear: true,
                            placeholder: "Select an manufacturer",
                            minimumResultsForSearch: 1, // no search box
                            multiple: false,
                            initSelection: function (element, callback) {
                                var elemValue = $scope.manufacturer;
                                if (typeof elemValue === 'object') {
                                    callback(elemValue);
                                } else {
                                    manufacturersService.getById(elemValue).then(function (response) {
                                        callback({ id: response.data.id, text: response.data.name });
                                    });
                                }
                            },
                            formatNoMatches: function (term) {
                                return "";
                            },
                            
                            query: function (query) {
                                manufacturersService.get(query.page, query.term).then(function (response) {
                                    $scope.results = [];
                                    $.each(response.data.makes, function (index, make) {
                                        $scope.results.push({
                                            id: make.id,
                                            text: make.name,
                                        });
                                    })
                                    var data = {
                                        more: false,
                                        results: $scope.results,
                                    }
                                    query.callback(data);
                                });
                            }
                        };
                        return s2options;
                    };
                }]
                    
            }
        }
    ])
})(angular);