 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('Estoque');

    angular.module('Estoque', ['ngRoute', 'idf.br-filters'])
    .controller('Estoque_list', ['$scope', '$http', function($scope, $http){

        $http.get('/Api/Estoque/')
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Estoque_details', ['$scope', '$http', '$routeParams', function($scope, $http, $routeParams){

        $http.get('/Api/Estoque/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

    }])
    .controller('Estoque_create', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $scope.data = {};
        
        $scope.save = function(){
            $http.post('/Api/Estoque/', $scope.data)
            .then(function(response){ $location.path("Estoque"); });
        }

    }])
    .controller('Estoque_edit', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Estoque/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});

        
        $scope.save = function(){
            $http.put('/Api/Estoque/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Estoque"); });
        }

    }])
    .controller('Estoque_delete', ['$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location){

        $http.get('/Api/Estoque/' + $routeParams.id)
        .then(function(response){$scope.data = response.data;});
        $scope.save = function(){
            $http.delete('/Api/Estoque/' + $routeParams.id, $scope.data)
            .then(function(response){ $location.path("Estoque"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
            $routeProvider
            .when('/Estoque', {
                title: 'Estoque - List',
                templateUrl: '/Static/Estoque_List',
                controller: 'Estoque_list'
            })
            .when('/Estoque/Create', {
                title: 'Estoque - Create',
                templateUrl: '/Static/Estoque_Edit',
                controller: 'Estoque_create'
            })
            .when('/Estoque/Edit/:id', {
                title: 'Estoque - Edit',
                templateUrl: '/Static/Estoque_Edit',
                controller: 'Estoque_edit'
            })
            .when('/Estoque/Delete/:id', {
                title: 'Estoque - Delete',
                templateUrl: '/Static/Estoque_Delete',
                controller: 'Estoque_delete'
            })
            .when('/Estoque/:id', {
                title: 'Estoque - Details',
                templateUrl: '/Static/Estoque_Details',
                controller: 'Estoque_details'
            })
    }])
;

})();
