app.controller("createTestController", ["$scope", "$http", "$location", function ($scope, $http, $location) {
    $scope.notes;
    $scope.schools;
    $scope.classes;
    $scope.chosenClass;
    $scope.decks;
    $scope.chosenNote;
    $scope.cardsInDeck;
    $scope.chosenDeckTitle;

    $scope.doneCreatingCards = false;
    $scope.decksExist = true;
    $scope.showNoteList = true;

    $scope.showCreateDeck = function () {
        $scope.decksExist = false;
    };

    $scope.clearChosenNote = function () {
        $scope.showNoteList = true;
        $scope.chosenNote = "";
    };

    $scope.chooseNote = function (note) {
        $scope.chosenNote = note.NoteText;
        $scope.showNoteList = false;
    };

    $scope.createDeck = function () {
        $http.post("/api/deck/add",
          {
              Title: $scope.newDeckTitle
          })
          .then(function (result) {
              console.log(result);
              getDeckList();
              $scope.decksExist = true;
          }).catch(error => console.log(error));
    };

    $scope.createCard = function () {
        console.log("deck id for posting card", $scope.newTest.DeckId);
        $http.post("/api/cards/add",
        {
            Question: $scope.newTest.Question,
            SchoolId: $scope.chosenClass,
            Answer: $scope.newTest.Answer,
            DeckId: $scope.newTest.DeckId

        }).then(function (result) {
            $scope.newTest.Question = "";
            $scope.newTest.Answer = "";
            $scope.doneCreatingCards = true;
            $scope.loadDeckCards(result.data.Deck.Id);
        }).catch(error => console.log(error));
    };

    $scope.loadDeckCards = function (deck) {
        getCardsInDeck(deck);
    };

    $scope.deleteCardFromDeck = function (cardid) {

    }

    var getCardsInDeck = function (deckid) {
        $http.get(`/api/cards/list/${deckid}`)
            .then(function (result) {
                var dataResults = result.data;
                var listOfCardsInDeck = [];

                if (dataResults.length > 0) {
                    Object.keys(dataResults).forEach((key) => {
                        dataResults[key].id = key;
                        listOfCardsInDeck.push(dataResults[key]);
                    });
                }
                $scope.cardsInDeck = listOfCardsInDeck;
                if (listOfCardsInDeck.length > 0) {
                    $scope.chosenDeckTitle = listOfCardsInDeck[0].Deck.Title;
                }
                console.log($scope.cardsInDeck);
            })
    };

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


}]);