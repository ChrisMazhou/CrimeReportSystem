AngularApp.controller('RegisterController',
    ['$scope', '$location','$window', 'AccountService',

    function ($scope, $location,$window, AccountService) {

        $scope.registerModel = {
            userName:'',
            genderType: '',
            idOrPassport: '',
            surname: '',
            firstName: '',
            title: '',
            email: '',
            telephone: '',
            password: '',
            confirmPassword: '',
            postalCode: '',
            city: '',
            addressLine1: '',
            addressLine2:''
        };

        $scope.submitForm = function () {

            $scope.$broadcast('show-errors-check-validity');

            if ($scope.registerForm.$invalid)
                return;

            var result = AccountService.register($scope.registerModel.userName,
                                                $scope.registerModel.genderType,
                                                $scope.registerModel.idOrPassport,
                                                $scope.registerModel.surname,
                                                $scope.registerModel.firstName,
                                                $scope.registerModel.title,
                                                $scope.registerModel.email,
                                                $scope.registerModel.telephone,
                                                $scope.registerModel.password,
                                                $scope.registerModel.confirmPassword,
                                                $scope.registerModel.postalCode,
                                                $scope.registerModel.city,
                                                $scope.registerModel.addressLine1,
                                                $scope.registerModel.addressLine2

                );
            result.then(function (result) {
                    $location.path('/home');
            },
            function (error) {
                alert('Some error occured');
            });
        }

        $scope.resetForm = function () {
            $scope.$broadcast('show-errors-reset');
        }

        $scope.cancelForm = function () {
            $scope.$broadcast('show-errors-reset');
            $window.history.back();
        }
    }

]);


