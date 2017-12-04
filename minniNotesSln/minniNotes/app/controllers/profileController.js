app.controller("profileController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.username = "Controller Working";
    $scope.userId = 'testing';

    $http.get("/api/note/list")
        .then(function (result) {
            console.log(result.data);
        }).catch(function (error) { console.log(error) });

}
]);