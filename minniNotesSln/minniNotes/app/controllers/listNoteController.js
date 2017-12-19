app.controller("listNoteController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.notes;
    var getNotes = function () {
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
    };
    getNotes();


    $scope.goToCreateNote = function () {
        $location.url("/note/create");
    };

    $scope.goToEditView = function (noteid) {
        $location.url(`/note/edit/${noteid}`);
    }

    $scope.delete = function (noteId) {
        $http.delete(`api/note/remove/${noteId}`)
            .then(function (result) {
                getNotes();
            });
    };
}]);