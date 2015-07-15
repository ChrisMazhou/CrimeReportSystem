AngularApp.factory('MasterData',
   ['$http',
       function ($http) {

      /*Return a mapping of the same service methods as server side MasterDataController*/
        var _SaveLearner = function(id,learnerName,IdpassportNum,city,addressLine1,addressLine2,postalCode,telephoneNumber){
            return $http.post("Learner/SaveLearner/",{
                Id:id,
                LearnerName:learnerName,
                IdPassportNum:IdpassportNum,
                City:city,
                AddressLine1:addressLine1,
                AddressLine2:addressLine2,
                PostalCode:postalCode,
                telephoneNumber:telephoneNumber
            });
        };

        var _learnerList = function () {
            return $http.get("/Learner/LearnerList");

        };

        var _learnerGrid = function (currentPage, recordsPerPage, sortKey, sortOrder, searchfor) {
            var req = {
                CurrentPage: currentPage,
                RecordsPerPage: recordsPerPage,
                SortKey: sortKey,
                SortOrder: sortOrder,
                Searchfor: searchfor
            }
            return $http.put("/Learner/LearnerGrid", req);
        }

        return {
            LearnerSave: _SaveLearner,
            LearnerList: _learnerList,
            LearnerGrid: _learnerGrid,
           
        }
       }

   ]);