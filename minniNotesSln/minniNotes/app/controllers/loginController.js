﻿app.controller("loginController", ["$rootScope", "$scope", "$http", "$location", "$window", function ($rootScope, $scope, $http, $location, $window) {
    $scope.username = "";
    $scope.password = "";
    $scope.loginShow = true

    var logout = function () {
        $http.post('api/Account/Logout').then(function (result) {
            $window.sessionStorage.user = null;
            $rootScope.username = null;
            $location.url('/login');
        }).catch(function (error) {
            console.log("logout error");
        });
    }
    if ($location.path() === '/logout') {
        logout();
    }


    var loginUser = function () {
        $scope.error = "";
        $scope.inProgress = true;
        $http({
            method: 'POST',
            url: "/Token",
            headers: { 'Content-Type': 'application/x-www-form-urlencoded' },
            transformRequest: function (obj) {
                var str = [];
                for (var p in obj)
                    str.push(encodeURIComponent(p) + "=" + encodeURIComponent(obj[p]));
                return str.join("&");
            },
            data: { grant_type: "password", username: $scope.username, password: $scope.password }
        })
            .then(function (result) {
                sessionStorage.setItem('token', result.data.access_token);
                $http.defaults.headers.common['Authorization'] = `bearer ${result.data.access_token}`;
                $window.sessionStorage.username = result.data.userName;
                $rootScope.username = result.data.userName;
                $location.path("/note/create");
                $scope.inProgress = false;
            }, function (result) {
                $scope.error = result.data.error_description;
                $scope.inProgress = false;
            });
    };

    $scope.login = function () {
        loginUser();
    };

    $scope.showRegisterFeilds = function () {
        $scope.loginShow = false;
    };

    $scope.register = function () {
        $scope.loginShow = true;
        $scope.username = $scope.email;

        registerUserInfo = {
            Email: $scope.email,
            Password: $scope.password,
            ConfirmPassword: $scope.confirmPass
        };

        $http({
            method: 'POST',
            url: 'api/Account/Register',
            data: registerUserInfo
        })
            .then(function (result) {
                loginUser();
            });
    };
}
]);