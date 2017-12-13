app.controller("listSchoolController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.schools;

    var getSchoolList = function() {
    $http.get("/api/school/list")
        .then(function (result) {
            var dataResults = result.data;
            var listOfSchools = [];

            if (dataResults.length > 0) {
                Object.keys(dataResults).forEach((key) => {
                    dataResults[key].id = key;
                    listOfSchools.push(dataResults[key]);
                });
            }
            $scope.schools = listOfSchools;
            console.log($scope.schools);
        }).catch(function (error) {
            console.log(error);
        });
    };
    getSchoolList();

    $scope.newSchool = {};
    $scope.createSchool = function () {
        $http.post("/api/school/add",
            {
                Name: $scope.newSchool.Name,
                City: $scope.newSchool.City,
                State: $scope.newSchool.State
            })
            .then(function (result) {
                console.log(result);
                getSchoolList();
            }).catch(error => console.log(error));
    }
}
]);