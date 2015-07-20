var AngularApp = angular.module('AngularApp', ['ngRoute', 'ui.bootstrap', 'validation.match', 'ui.bootstrap.ShowErrors']);

AngularApp.config(
    ['$routeProvider', '$httpProvider', '$locationProvider',
        function ($routeProvider, $httpProvider, $locationProvider) {

            $routeProvider
            .when("/Home", {
                templateUrl: "App/Views/Home/Home/Home.html",
                controller: "HomePageController"
            })
            .when('/login', {
                templateUrl: 'app/Views/Account/Login.html',
                controller: 'LoginController'
            })
            .when('/register', {
            templateUrl: 'app/Views/Account/Register.html',
            controller: 'RegisterController'
            })
            .otherwise({
                redirectTo:'/home'
            });


            $locationProvider.html5Mode(true);
            $httpProvider.interceptors.push("AuthHttpResponceInterceptors");
        }]);