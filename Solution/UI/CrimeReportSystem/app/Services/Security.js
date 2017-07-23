AngularApp.factory('Security',
   ['$rootScope', function ($rootScope) {

       var _scopeUpdateEvent = 'current-user-updated';

       var _CurrentUser = {
           loggedIn: false,
           id: 0,
           userName: '',
           displayName: '',
           allowedPrivileges: null
       };

       var login = function (id, userName, displayName, allowedPrivileges) {
           _CurrentUser.id = id;
           _CurrentUser.userName = userName;
           _CurrentUser.displayName = displayName;
           _CurrentUser.allowedPrivileges = allowedPrivileges;

           _CurrentUser.loggedIn = true;

           $rootScope.$emit(_scopeUpdateEvent, _CurrentUser);

       };

       var logout = function () {
           _CurrentUser.id = 0;
           _CurrentUser.userName = '';
           _CurrentUser.displayName = '';
           _CurrentUser.isLoggedIn = false;

           $rootScope.$emit(_scopeUpdateEvent, _CurrentUser);

       };

       $rootScope.$emit(_scopeUpdateEvent, _CurrentUser);


       return {
           currentUser: _CurrentUser,
           login: login,
           logout: logout,
           scopeUpdateEvent: _scopeUpdateEvent
       }


   }
   ]);