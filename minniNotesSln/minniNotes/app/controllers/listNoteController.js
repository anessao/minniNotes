app.controller("listNoteController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.notes;

    $http.get("/api/note/list")
        .then(function (result) {
            var dataResults = result.data;
            var listOfNotes = [];

            if (dataResults.length > 0) {
                Object.keys(dataResults).forEach((key) => {
                    dataResults[key].id = key;
                    listOfNotes.push(dataResults[key]);
                });
            }
            $scope.notes = listOfNotes;
            console.log($scope.notes);
        }).catch(function (error) {
            console.log(error);
        });
    $scope.goToCreateNote = function () {
        $location.url("/note/create");
    };
}
]);