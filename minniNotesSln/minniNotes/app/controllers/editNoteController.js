app.controller("editNoteController", ["$scope", "$http", "$location", "$routeParams", function ($scope, $http, $location, $routeParams) {
    var chosenNote = $routeParams.noteid;
    $scope.chosenNoteObject;
    $scope.schools;
    $scope.classes;

    $scope.isInEdit = false;

    $http.get(`/api/note/view/${chosenNote}`)
        .then(function (result) {
            $scope.chosenNoteObject = result.data;
            console.log(result.data);
        }).catch(function (error) {
            console.log(error);
        });

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

    $scope.showEditMode = function () {
        $scope.isInEdit = true;
    };

    $scope.sendUpdate = function () {
        var newNoteData = $scope.chosenNoteObject;

        $http.put(`api/note/edit/${newNoteData.Id}`,
            {
                NoteText: newNoteData.NoteText,
                Title: newNoteData.Title,
                SchoolId: $scope.schoolId,
                ClassId: $scope.classId
            })
            .then(function (result) {
                $scope.isInEdit = false;
                $scope.chosenNoteData = result.data;
                console.log("result", result);
            }).catch(function (error) {
                console.log("put error: ", error);
            });
    };

}]);