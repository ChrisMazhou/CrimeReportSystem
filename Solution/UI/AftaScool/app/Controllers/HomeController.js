AngularApp.controller('HomeController',
    ['$scope','HomeService',
    function ($scope, HomeService) {

        HomeService.getEmployees().then(function (results) {
            var data = results.data;
        },
        function (results) {
            var error = results.statusText;
        })

        $scope.models = {
            helloAngular: 'I work!'
        };

        $scope.navbarProperties = {
            isCollapsed: true
        };

       
    }

]);

