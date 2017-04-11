var modules = modules || [];
angular.module('app', modules);
angular.module('app').controller('MainMenu', ['$scope', function ($scope) {
    $scope.modules = modules;
}]);
