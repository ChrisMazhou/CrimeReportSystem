AngularApp.controller('HomeController',
    ['$scope', 'HomeService', 'Security', '$rootScope', 'EnumsService',
function ($scope, HomeService, Security, $rootScope, EnumsService) {

    HomeService.getEmployees().then(function (results) {
        var data = results.data;
    },
    function (results) {
        var error = results.statusText;
    })

    $scope.navbarProperties = {
        isCollapsed: true
    };

    $scope.currentUser = Security.currentUser;

    $rootScope.$on(Security.scopeUpdateEvent, function (event, currentUser) {
        $scope.currentUser = Security.currentUser;
    });
}

    ]);

