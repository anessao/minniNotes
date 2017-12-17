app.controller("listDecksController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.decks;

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
    
}
]);