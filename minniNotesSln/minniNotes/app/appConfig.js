app.config(["$routeProvider", function ($routeProvider) {
    $routeProvider
        .when("/login",
        {
            templateUrl: "/app/views/Login.html",
            controller: "loginController"
        })
        .when("/profile",
        {
            templateUrl: "/app/views/Profile.html",
            controller: "profileController"
        })
        .when("/note/create", {
            templateUrl: "/app/views/NoteAdd.html",
            controller: "addNoteController"
        })
        .when("/note/all", {
            templateUrl: "/app/views/NoteList.html",
            controller: "listNoteController"
        })
        .when("/school/all", {
            templateUrl: "/app/views/SchoolList.html",
            controller: "listSchoolController"
        })
        .when("/class/all", {
            templateUrl: "/app/views/ClassList.html",
            controller: "listClassController"
        })
        .when("/test/create", {
            templateUrl: "/app/views/CreateTest.html",
            controller: "createTestController"
        });
}]);


app.run(["$rootScope", "$http", "$location", function ($rootScope, $http, $location) {

    $rootScope.isLoggedIn = function () { return !!sessionStorage.getItem("token"); };

    $rootScope.$on("$routeChangeStart", function (event, currRoute) {
        var anonymousPage = false;
        var originalPath = currRoute.originalPath;

        if (originalPath) {
            anonymousPage = originalPath.indexOf("/login") !== -1;
        }

        if (!anonymousPage && !$rootScope.isLoggedIn()) {
            event.preventDefault();
            $location.path("/login");
        }
    });

    var token = sessionStorage.getItem("token");

    if (token)
        $http.defaults.headers.common["Authorization"] = `bearer ${token}`;
}]);