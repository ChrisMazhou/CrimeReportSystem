AngularApp.factory('EnumsService',
   ['$http', function ($http) {

       //status type
       var _statusType = [];

       var loadStatusType = function () {
           return $http.get("/Enums/StatusTypeEnum").then(function (result) {
               _statusType = result.data;
           });
       };

       var getStatusTypes = function () {
           return _statusType;
       }

       loadStatusType();
       //school type
       var _schoolType = [];

       var loadSchoolType = function () {
           return $http.get("/Enums/SchoolTypeEnum").then (function (result){
           _schoolType = result.data;
           });
       };

       var getSchoolTypes = function(){
           return _schoolType;
       }
       loadSchoolType();

     
       //gender type
       var _genderType = [];

       var loadGenderType = function () {
           return $http.get("/Enums/GenderTypeEnum").then(function (result) {
               _genderType = result.data;
           });
       };

       var getGenderTypes = function () {
           return _genderType;
       }

       loadGenderType();

       //interface
       return {
           statusTypes: getStatusTypes,
           genderTypes: getGenderTypes,
           schoolTypes:getSchoolTypes
       }


   }
   ]);