AngularApp.factory('ReportCrimeService',
    ['$http',
   function ($http) {
       
       var saveReportCrime = function (id,status,name,contactNo,typeOfCrime, location, date, time) {

           return $http.post("/ReportCrime/SaveReport", {

               Id: id,
               Status:status,
               Name: name,
               ContactNo:contactNo,
               TypeOfCrime: typeOfCrime,
               Location: location,
               Date: date,
               Time: time

           });
       }
       var _CrimeList = function () {

           return $http.get("/ReportCrime/CrimeList");

       };
       var _reportGrid = function (currentPage, recordsPerPage, sortKey, sortOrder, searchfor) {
           var req = {
               CurrentPage: currentPage,
               RecordsPerPage: recordsPerPage,
               SortKey: sortKey,
               SortOrder: sortOrder,
               Searchfor: searchfor
           }
           return $http.put("/ReportCrime/ReportGrid", req);
       }
       var _reportGet = function (crimeId) {
           return $http.get("/ReportCrime/GetReportCrimes/" + crimeId);
       }
       return {
           SaveReportCrime: saveReportCrime,
           CrimeList: _CrimeList,
           ReportGrid: _reportGrid,
           ReportGet:_reportGet

       }



   }


    ]);