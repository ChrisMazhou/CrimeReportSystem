AngularApp.factory('HomeService',
    ['$http',
    function ($http) {
        var getEmployees = function () {
            return $http.get("/Home/GetEmployees");
        };

        var getEmployee = function (id) {
            return {
                id: id,
                firstName: "Sifiso",
                surname: "Mazibuko"
            };
        }

        var saveEmployee = function (employee) {

        }


        return {
            getEmployees: getEmployees,
            saveEmployee: saveEmployee
        }
    }

    ]);