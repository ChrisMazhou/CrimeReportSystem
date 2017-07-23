AngularApp.controller('LoginController',
            ['$scope', '$location', 'AccountService', 'Security',
    function ($scope, $location, AccountService, Security) {

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

                        Security.login(result.data.id, result.data.userName, result.data.displayName, result.data.allowedPrivileges);
                        $location.path('/home');
                    },

                    function (error) {
                        $scope.hasError = true;
                        $scope.errorMessage = error.statusText;
                        Security.logout();
                    }

                    );
            }

        }

        $scope.resetForm = function () {
            $scope.$broadcast('show-errors-reset');
        }
    }

            ]);

