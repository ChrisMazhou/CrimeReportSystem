AngularApp.factory('HomeService',
    ['$http',
    function ($http) {
        var getEmployees = function () {
            return $http.get("/Home/GetEmployees");
        };

        var getEmployee = function (id) {
            return {
                id: id,
                firstName: "Kefilwe",
                surname: "Mkhwanazi"
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