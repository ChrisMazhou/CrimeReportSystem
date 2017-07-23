var AngularApp = angular.module('AngularApp', ['ngRoute','ui.bootstrap','validation.match','ui.bootstrap.showErrors','chieffancypants.loadingBar']);

AngularApp.config(
    ['$routeProvider', '$httpProvider', '$locationProvider',
    function ($routeProvider, $httpProvider, $locationProvider) {

        $routeProvider
        .when("/home", {
            templateUrl: "app/Views/Home/Home.html",
            controller: "HomeController"
        })
        .when('/login', {
            templateUrl: 'app/Views/Account/Login.html',
            controller: 'LoginController'
        })
        .when('/register', {
            templateUrl: 'app/Views/Account/Register.html',
            controller: 'RegisterController'
        })
        .when('/ReportCrime', {
            templateUrl: 'app/Views/ReportCrime/SaveReport.html',
            controller: 'ReportCrimeController'
        })
       .when('/Testing/:id', {
           templateUrl: 'app/Views/ReportCrime/Testing.html',
                controller: 'ReportCrimeController'
            })
       .when('/ReportCrimes', {
           templateUrl: 'app/Views/ReportCrime/ReportMaintenance.html',
                controller: 'ReportCrimeController'
            })
   
    .otherwise({
        redirectTo: '/home'
    });

        $locationProvider.html5Mode(true);

        $httpProvider.interceptors.push('AuthHttpResponseInterceptor');


        //initialize get if not there
        if (!$httpProvider.defaults.headers.get) {
            $httpProvider.defaults.headers.get = {};
        }

        $httpProvider.defaults.headers.get['If-Modified-Since'] = 'Mon, 26 Jul 1997 05:00:00 GMT';
        $httpProvider.defaults.headers.get['Cache-Control'] = 'no-cache';
        $httpProvider.defaults.headers.get['Pragma'] = 'no-cache';

    }]);

