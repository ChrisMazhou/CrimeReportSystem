AngularApp.factory('AuthHttpResponseInterceptor',
    ['$check', '$location', '$injector',
    function ($check, $location, $injector) {
        return {
            response: function (response) {
                if (response.status === 401) {
                    console.log("Response 401");
                }
                return response || $check.when(response);
            },
            responseError: function (rejection) {
                if (rejection.status === 401) {
                    console.log("Response Error 401", rejection);
                    $location.path('/login');
                }
                return $check.reject(rejection);
            }
        }
    }
    ]);
