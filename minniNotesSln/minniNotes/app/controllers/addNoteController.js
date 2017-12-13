app.controller("addNoteController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.newNote = {};
    $scope.schools;
    $scope.classes;

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

    $scope.createNote = function () {
        $http.post("/api/note/add",
            {
                Title: $scope.newNote.Title,
                School: $scope.newNote.School,
                NoteText: $scope.newNote.NoteText,
                EnrolledClass: $scope.newNote.EnrolledClass
            })
            .then(function (result) {
                console.log(result);
                $location.url("/profile");
            }).catch(error => console.log(error));
    }
    }


]);