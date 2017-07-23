AngularApp.controller('ReportCrimeController',
    ['$scope', '$location', '$window', '$routeParams', 'ReportCrimeService',
    function ($scope, $location, $window, $routeParams, ReportCrimeService) {

        $scope.reportModel = {
            id: null,
            status: '',
            name: '',
            contactNo: '',
            typeOfCrime: '',
            location: '',
            date: '',
            time: ''

        };

        $scope.reportModel.id = $routeParams.id;
        if ($scope.reportModel.id > 0) {
            ReportCrimeService.ReportGet($scope.reportModel.id).then(
                function (result) {
                    $scope.reportModel.id = result.data.id;
                    $scope.reportModel.status = result.data.status;
                    $scope.reportModel.name = result.data.name;
                    $scope.reportModel.contactNo = result.data.contactNo;
                    $scope.reportModel.typeOfCrime = result.data.typeOfCrime;
                    $scope.reportModel.location = result.data.location;
                    $scope.reportModel.date = result.data.date;
                    $scope.reportModel.time = result.data.time;           

                },
                function (error) {
                    handleError(error);
                });
        };

        $scope.submitForm = function () {

            $scope.$broadcast('show-errors-check-validity');

            if ($scope.NewReportForm.$invalid)
                return;


            var result = ReportCrimeService.SaveReportCrime($scope.reportModel.id,
                                                $scope.reportModel.status,
                                                $scope.reportModel.name,
                                                $scope.reportModel.contactNo,
                                                $scope.reportModel.typeOfCrime,
                                                $scope.reportModel.location,
                                                $scope.reportModel.date,
                                                $scope.reportModel.time
    
                );

            result.then(function (result) {
                $window.history.back();

            },
                           function (error) {
                               handleError(error);
                           });
        }
        $scope.resetForm = function () {
            $scope.$broadcast('show-errors-reset');
        }

        $scope.cancelForm = function () {
            $scope.$broadcast('show-errors-reset');
            $window.history.back();
        }
        $scope.newReport = function () {
            $location.path("/Testing/0");
        };

        var handleError = function (error) {
            $scope.hasError = true;
            $scope.errorMessage = error.statusText;
        }

        $scope.sortKeyOrder = {
            key: 'typeOfCrime',
            order: 'ASC',
        };


        $scope.totalItems = 0;
        $scope.currentPage = 1;
        $scope.maxSize = 5;
        $scope.recordsPerPage = 20;
        $scope.numberOfPageButtons = 5;

        $scope.sort = function (col) {
            if ($scope.sortKeyOrder.key === col) {
                if ($scope.sortKeyOrder.order == 'ASC')
                    $scope.sortKeyOrder.order = 'DESC';
                else
                    $scope.sortKeyOrder.order = 'ASC';
            } else {
                $scope.sortKeyOrder.key = col;
                $scope.sortKeyOrder.order = 'ASC';
            }
            loadGrid();
        };


        var loadGrid = function () {
            var searchFor = '';

            ReportCrimeService.ReportGrid($scope.currentPage, $scope.recordsPerPage,
                                      $scope.sortKeyOrder.key, $scope.sortKeyOrder.order, searchFor).then(
                function (result) {
                    $scope.reportGrid = result.data.results;
                    $scope.totalItems = result.data.recordCount;
                },
                function (error) {
                    alert("an error occured: unable to get data");
                });


        };


        $scope.pageChanged = function () {
            loadGrid();
        };


        loadGrid();

    }
    ]);