AngularApp.controller('LoginController',
    ['$scope', '$location', 'AccountService',

    function ($scope, $location, AccountService) {

        $scope.loginModel = {
                        userName: '',
                        password: '',
                        rememberMe: false
                    };
       

        $scope.submitForm = function () {

            $scope.$broadcast('show-errors-check-validity');

            if ($scope.loginForm.$valid) {

                AccountService.login($scope.loginModel.userName,
                                     $scope.loginModel.password,
                                     $scope.loginModel.rememberMe).then(

                    function (result) {
                        $location.path('/home');
                    },

                    function (error) {
                        $scope.hasError = true;
                        $scope.errorMessage = error.statusText;
                    }

                    );
            }

        }

        $scope.resetForm = function () {
            $scope.$broadcast('show-errors-reset');
        }
    }

    ]);

