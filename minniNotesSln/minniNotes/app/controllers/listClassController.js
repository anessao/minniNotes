app.controller("listClassController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.classes;
    console.log($scope.today);

    var getClassList = function () {
        $http.get("/api/class/list")
            .then(function (result) {
                var dataResults = result.data;
                var listOfClasses = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfClasses.push(dataResults[key]);
                    });
                }
                $scope.classes = listOfClasses;
                console.log($scope.classes);
            }).catch(function (error) {
                console.log(error);
            });
    };
    getClassList();

    $scope.schools;

    var getSchoolList = function () {
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

    $scope.newClass = {};
    $scope.createClass = function () {
        $http.post("/api/class/add",
            {
                Name: $scope.newClass.Name,
                StartDate: $scope.newClass.StartDate,
                EndDate: $scope.newClass.EndDate,
                SchoolId: $scope.newClass.SchoolId
            })
            .then(function (result) {
                console.log(result);
                getClassList();
            }).catch(error => console.log(error));
    };
}
]);