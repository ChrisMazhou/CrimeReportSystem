AngularApp.factory('AccountService',
    ['$http',
    function ($http) {
        var login = function (userName, password, rememberMe) {
            return $http.post("/Account/Login", {
                UserName: userName,
                Password: password,
                RememberMe: rememberMe
            });
        };

        var register = function (userName, genderType, idOrPassport, surname,firstName,
                                 title,email,telephone, password, confirmPassword, postalCode,
                                 city,addressLine1, addressLine2) {
            return $http.post("/Account/Register", {
                            UserName : userName,
                            GenderType: genderType,
                            IDOrPassport:idOrPassport,
                            Surname: surname,
                            FirstName: firstName,
                            Title:title,
                            Email:email,
                            Telephone: telephone,
                            Password: password,
                            ConfirmPassword: confirmPassword,
                            PostalCode:postalCode,
                            City:city,
                            AddressLine1:addressLine1,
                            AddressLine2: addressLine2
            });
        };

        return {
            login: login,
            register: register
        }
    }

    ]);

