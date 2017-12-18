app.controller("listDecksController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.decks;
    $scope.newDeckTitle;
    $scope.showAddDeck = false;


    var getDeckList = function () {
        $http.get("/api/deck/list")
            .then(function (result) {
                var dataResults = result.data;
                var listOfDecks = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfDecks.push(dataResults[key]);
                    });
                }
                $scope.decks = listOfDecks;
                console.log(listOfDecks);
            }).catch(function (error) {
                console.log(error);
            });
    };
    getDeckList();

    $scope.playDeck = function (deckId) {
        $location.url(`/deck/play/${deckId}`);
    };

    $scope.delete = function (deckId) {
        console.log("deleting");
        $http.delete(`api/deck/remove/${deckId}`)
            .then(function (result) {
                console.log("working delete");
                getDeckList();
            })
    }

    $scope.showCreateDeck = function () {
        $scope.showAddDeck = true;
    }

    $scope.createDeck = function () {
        $http.post("/api/deck/add",
            {
                Title: $scope.newDeckTitle
            })
            .then(function (result) {
                console.log(result);
                getDeckList();
                $scope.showAddDeck = false;
            }).catch(error => console.log(error));
    };
    
}
]);