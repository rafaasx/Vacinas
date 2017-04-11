var modules = modules || [];
(function () {
    'use strict';
    modules.push('Pacote');

    angular.module('Pacote', ['ngRoute', 'ui.bootstrap'])
    .controller('Pacote_list', ['$scope', '$http', '$interval', function ($scope, $http, $interval) {
        $scope.busca = "";
        $http.get('/Api/Pacote/')
        .then(function (response) {
            $scope.data = response.data;
        });

        $scope.getEstoque = function () {
            $http.get('/Api/Estoque/BuscarSaldo/0')
                .then(function (response) {
                    $scope.estoque = response.data;
                });
        };
        $scope.estoque = 0;
        var theInterval = $interval(function () {
            $scope.getEstoque();
        }.bind(this), 1000 * 60 * 2); // 2 Min

        $scope.$on('$destroy', function () {
            $interval.cancel(theInterval);
        });
        $scope.getEstoque();

        $scope.filtro = function (item) {
            if ($scope.busca != "")
                return item.Id == $scope.busca || item.Regiao_Nome.toLowerCase() == $scope.busca.toLowerCase() || item.TotalGeral == $scope.busca;
            else
                return true;
        }
    }])
    .controller('Pacote_details', ['$scope', '$http', '$routeParams', function ($scope, $http, $routeParams) {

        $http.get('/Api/Pacote/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });

    }])
    .controller('Pacote_create', ['$scope', '$http', '$routeParams', '$location', '$interval', function ($scope, $http, $routeParams, $location, $interval) {

        $scope.data = { Id: 0 };
        $http.get('/Api/Regiao/')
        .then(function (response) {
            $scope.RegiaoId_options = response.data;
        });

        $scope.save = function () {
            $http.post('/Api/Pacote/', $scope.data)
            .then(function (response) { $location.path("Pacote"); });
        }

        $scope.calcularTotais = function () {
            $http.post('/Api/Pacote/CalcularTotais/0', $scope.data)
            .then(function (response) {
                $scope.data = response.data;
                $scope.data.DataEnvio = new Date($scope.data.DataEnvio);
            });
        }

        $scope.changeRegiao = function () {
            $scope.data.RegiaoId = $scope.RegiaoId;
        }

        $scope.estoque = 0;
        $scope.getEstoque = function () {
            $http.get('/Api/Estoque/BuscarSaldo/0')
                .then(function (response) {
                    $scope.estoque = response.data;
                });
        };
        var theInterval = $interval(function () {
            $scope.getEstoque();
        }.bind(this), 1000 * 60 * 2); // 2 Min

        $scope.$on('$destroy', function () {
            $interval.cancel(theInterval);
        });
        $scope.getEstoque();
        $scope.validarEstoque = function () {
            return $scope.data.TotalGeral > $scope.estoque;
        };
        $scope.today = function () {
            $scope.data.DataEnvio = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.data.DataEnvio = null;
        };
        $scope.inlineOptions = {
            customClass: getDayClass,
            minDate: new Date(),
            showWeeks: true
        };
        $scope.dateOptions = {
            formatYear: 'yyyy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 0
        };
        
        $scope.format = 'dd/MM/yyyy';
        $scope.altInputFormats = ['d!/M!/yyyy'];
        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.openPopup = function () {
            $scope.popup.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.data.DataEnvio = new Date(year, month, day);
        };

        $scope.popup = {
            opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
          {
              date: tomorrow,
              status: 'full'
          },
          {
              date: afterTomorrow,
              status: 'partially'
          }
        ];

        function getDayClass(data) {
            var date = data.date,
              mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        };



    }])
    .controller('Pacote_edit', ['$scope', '$http', '$routeParams', '$location', '$interval', function ($scope, $http, $routeParams, $location, $interval) {
        $scope.data = {};
        $http.get('/Api/Regiao/')
        .then(function (response) {
            $scope.RegiaoId_options = response.data;
        });

        $http.get('/Api/Pacote/' + $routeParams.id)
        .then(function (response) {
            $scope.data = response.data;
            $scope.RegiaoId = response.data.RegiaoId;
            $scope.data.DataEnvio = new Date($scope.data.DataEnvio);
        });

        $scope.changeRegiao = function () {
            $scope.data.RegiaoId = $scope.RegiaoId;
        }

        $scope.save = function () {

            $http.put('/Api/Pacote/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("Pacote"); });
        }

        $scope.estoque = 0;
        $scope.getEstoque = function () {
            $http.get('/Api/Estoque/BuscarSaldo/0')
                .then(function (response) {
                    $scope.estoque = response.data;
                });
        };
        var theInterval = $interval(function () {
            $scope.getEstoque();
        }.bind(this), 1000 * 60 * 2); // 2 Min

        $scope.$on('$destroy', function () {
            $interval.cancel(theInterval);
        });
        $scope.getEstoque();

        $scope.calcularTotais = function () {
            $http.post('/Api/Pacote/CalcularTotais/' + $routeParams.id, $scope.data)
                .then(function (response) {
                    $scope.data = response.data;
                    $scope.data.DataEnvio = new Date($scope.data.DataEnvio);
                });
        };
        $scope.today = function () {
            $scope.data.DataEnvio = new Date();
        };
        $scope.today();

        $scope.clear = function () {
            $scope.data.DataEnvio = null;
        };

        $scope.inlineOptions = {
            customClass: getDayClass,
            minDate: new Date(),
            showWeeks: true
        };

        $scope.dateOptions = {
            formatYear: 'yyyy',
            maxDate: new Date(2020, 5, 22),
            minDate: new Date(),
            startingDay: 0
        };


        $scope.format = 'dd/MM/yyyy';
        $scope.altInputFormats = ['d!/M!/yyyy'];
        $scope.toggleMin = function () {
            $scope.inlineOptions.minDate = $scope.inlineOptions.minDate ? null : new Date();
            $scope.dateOptions.minDate = $scope.inlineOptions.minDate;
        };

        $scope.toggleMin();

        $scope.openPopup = function () {
            $scope.popup.opened = true;
        };

        $scope.setDate = function (year, month, day) {
            $scope.data.DataEnvio = new Date(year, month, day);
        };

        $scope.popup = {
            opened: false
        };

        var tomorrow = new Date();
        tomorrow.setDate(tomorrow.getDate() + 1);
        var afterTomorrow = new Date();
        afterTomorrow.setDate(tomorrow.getDate() + 1);
        $scope.events = [
          {
              date: tomorrow,
              status: 'full'
          },
          {
              date: afterTomorrow,
              status: 'partially'
          }
        ];

        function getDayClass(data) {
            var date = data.date,
              mode = data.mode;
            if (mode === 'day') {
                var dayToCheck = new Date(date).setHours(0, 0, 0, 0);

                for (var i = 0; i < $scope.events.length; i++) {
                    var currentDay = new Date($scope.events[i].date).setHours(0, 0, 0, 0);

                    if (dayToCheck === currentDay) {
                        return $scope.events[i].status;
                    }
                }
            }

            return '';
        };

        $scope.validarEstoque = function () {
            return $scope.data.TotalGeral > $scope.estoque;
        };
    }])
    .controller('Pacote_delete', ['$scope', '$http', '$routeParams', '$location', function ($scope, $http, $routeParams, $location) {

        $http.get('/Api/Pacote/' + $routeParams.id)
        .then(function (response) { $scope.data = response.data; });
        $scope.save = function () {
            $http.delete('/Api/Pacote/' + $routeParams.id, $scope.data)
            .then(function (response) { $location.path("Pacote"); });
        }

    }])

    .config(['$routeProvider', function ($routeProvider) {
        $routeProvider
        .when('/Pacote', {
            title: 'Pacote - List',
            templateUrl: '/Static/Pacote_List',
            controller: 'Pacote_list'
        })
        .when('/Pacote/Create', {
            title: 'Pacote - Create',
            templateUrl: '/Static/Pacote_Edit',
            controller: 'Pacote_create'
        })
        .when('/Pacote/Edit/:id', {
            title: 'Pacote - Edit',
            templateUrl: '/Static/Pacote_Edit',
            controller: 'Pacote_edit'
        })
        .when('/Pacote/Delete/:id', {
            title: 'Pacote - Delete',
            templateUrl: '/Static/Pacote_Delete',
            controller: 'Pacote_delete'
        })
        .when('/Pacote/:id', {
            title: 'Pacote - Details',
            templateUrl: '/Static/Pacote_Details',
            controller: 'Pacote_details'
        })
    }])
    ;

})();
