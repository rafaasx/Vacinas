 var  modules = modules || [];
(function () {
    'use strict';
    modules.push('Regiao');

    angular.module('Regiao', ['ngRoute', 'ngSanitize'])
        .controller('Regiao_list', [
            '$scope', '$http', function($scope, $http) {

                $http.get('/Api/Regiao/')
                    .then(function(response) { $scope.data = response.data; });

            }
        ])
        .controller('Regiao_details', [
            '$scope', '$http', '$routeParams', function($scope, $http, $routeParams) {

                $http.get('/Api/Regiao/' + $routeParams.id)
                    .then(function(response) { $scope.data = response.data; });

            }
        ])
        .controller('Regiao_create', [
            '$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location) {

                $scope.data = {};

                $scope.save = function() {
                    $http.post('/Api/Regiao', $scope.data)
                        .then(function(response) {
                         $location.path("Regiao");
                    });
                }

            }
        ])
        .controller('Regiao_edit', [
            '$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location) {

                $http.get('/Api/Regiao/' + $routeParams.id)
                    .then(function(response) { $scope.data = response.data; });


                $scope.save = function() {
                    $http.put('/Api/Regiao/' + $routeParams.id, $scope.data)
                        .then(function(response) { $location.path("Regiao"); });
                }

            }
        ])
        .controller('Regiao_delete', [
            '$scope', '$http', '$routeParams', '$location', function($scope, $http, $routeParams, $location) {

                $http.get('/Api/Regiao/' + $routeParams.id)
                    .then(function(response) { $scope.data = response.data; });
                $scope.save = function() {
                    $http.delete('/Api/Regiao/' + $routeParams.id, $scope.data)
                        .then(function(response) { $location.path("Regiao"); });
                }

            }
        ])
        .config([
            '$routeProvider', function($routeProvider) {
                $routeProvider
                    .when('/Regiao', {
                        title: 'Regiao - Listar',
                        templateUrl: '/Static/Regiao_List',
                        controller: 'Regiao_list'
                    })
                    .when('/Regiao/Create', {
                        title: 'Regiao - Adicionar',
                        templateUrl: '/Static/Regiao_Edit',
                        controller: 'Regiao_create'
                    })
                    .when('/Regiao/Edit/:id', {
                        title: 'Regiao - Editar',
                        templateUrl: '/Static/Regiao_Edit',
                        controller: 'Regiao_edit'
                    })
                    .when('/Regiao/Delete/:id', {
                        title: 'Regiao - Deletar',
                        templateUrl: '/Static/Regiao_Delete',
                        controller: 'Regiao_delete'
                    })
                    .when('/Regiao/:id', {
                        title: 'Regiao - Details',
                        templateUrl: '/Static/Regiao_Details',
                        controller: 'Regiao_details'
                    });
            }
        ])
        .config(['$locationProvider', function ($locationProvider) {
            $locationProvider.hashPrefix('');
        }]);
;

})();
