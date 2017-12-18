app.controller("playTestController", ["$scope", "$http", "$location", "$rootScope", "$routeParams", function ($scope, $http, $location, $rootScope, $routeParams) {
    $scope.cardsInDeck;
    $scope.Question = "";
    $scope.answers = [];
    $scope.score;
    $scope.cardCount = 1;
    $scope.cardAmount;
    $scope.percentScore = 0;
    $scope.showFinal = false;

    //////////
    // Get Cards by Deck Id - GET
    //////////

    var chosenDeckId = $routeParams.deckId;

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
                $scope.cardAmount = $scope.cardsInDeck.length;
                loadQuestion();
                setAnswers();
            })
    };
    getCardsInDeck(chosenDeckId);

    //////////
    // Load in question to be shown
    //////////
    var testCounter = 0
    var questionId;


    var loadQuestion = function () {
        $scope.Question = $scope.cardsInDeck[testCounter].Question;
        questionId = $scope.cardsInDeck[testCounter].Id;
    }

    //////////
    // Set random batch of answers
    //////////

    var setAnswers = function () {
        var dataResults = $scope.cardsInDeck;
        var listOfAnswers = [];
        var chosenAnswers = [];

        Object.keys(dataResults).forEach((key) => {
            var answer = {
                Text: dataResults[key].Answer,
                answerId: dataResults[key].Id,
                points: 0
            };
            listOfAnswers.push(answer);
        });

        listOfAnswers.forEach((answer) => {
            if (answer.answerId == questionId) {
                answer.points = 1;
                chosenAnswers.push(answer);
            }
        });
        
        for (x = 0; chosenAnswers.length < 3; x++) {
            var randomNum = Math.floor((Math.random() * listOfAnswers.length - 1) + 1);

                var wrongAnswerId = listOfAnswers[randomNum].answerId;
                if (wrongAnswerId != questionId) {
                    if (chosenAnswers.length == 2) {
                        if (wrongAnswerId != chosenAnswers[1].answerId) {
                            chosenAnswers.push(listOfAnswers[randomNum]);
                        }
                    } else {
                        chosenAnswers.push(listOfAnswers[randomNum]);
                    }
                    
                }
        }
        
        $scope.answers = shuffle(chosenAnswers);
    };

    //////////
    // Calculate and Submit Answers - PUT on final score
    //////////

    var currentPoints = 0;
    var totalPoints = 0;

    $scope.calculateAnswer = function (points) {
        currentPoints = points;
    }

    $scope.submitAnswer = function () {
        totalPoints += currentPoints;
        $scope.cardCount++
        $scope.answers = [];
        $scope.Question = "";
        $scope.percentScore = Math.round((totalPoints / $scope.cardAmount) * 100);
        testCounter++

        if ($scope.cardsInDeck[testCounter]) {
            loadQuestion();
            setAnswers();
        } else {
            updateDeckScore($scope.percentScore);
            if ($scope.percentScore < 60) {
                $scope.message = "You Fail!"
            } else {
                $scope.message = "You Win!";
            }
            
            console.log("No more!");
            testCounter = 0;
            currentPoints = 0;
            totalPoints = 0;
            $scope.showFinal = true;
        }
    };

    var updateDeckScore = function (score) {
        $http.put(`api/deck/update`,
            {
                DeckId: chosenDeckId,
                HighestScore: score
            })
            .then(function (result) {
            console.log("result", result);
        }).catch(function (error) {
            console.log("put error: ", error);
        });
    };

    var shuffle = function (array) {
        var currentIndex = array.length, temporaryValue, randomIndex;
        while (0 !== currentIndex) {

            randomIndex = Math.floor(Math.random() * currentIndex);
            currentIndex -= 1;

            temporaryValue = array[currentIndex];
            array[currentIndex] = array[randomIndex];
            array[randomIndex] = temporaryValue;
        }
        return array;
    };


}]);