// Set up a collection to contain question information. On the server,
// it is backed by a MongoDB collection named "questions".
Questions = new Mongo.Collection("questions");

Meteor.methods({
  newPlayer: function (requestedPlayerName) {
    this.unblock();
    return Meteor.http.post("http://fintechjsy.azurewebsites.net/api/players", {params: {Name: requestedPlayerName}});
  },

  addQuestion: function (playerId) {
    this.unblock();
    var newQuestion =  Meteor.http.get("http://fintechjsy.azurewebsites.net/api/Answers/NextQuestion/" + playerId);
    newQuestion.data.Title = newQuestion.data.Title.trim();
    return Questions.insert({playerId: playerId, questionId: newQuestion.data.Id, question: newQuestion.data});
  }
});

Meteor.startup(function () {
  // if (Questions.find().count() === 0) {
  //   var names = ["Buy a car?", "Get a job?", "Go on holiday?",
  //   "Buy or rent a house", "Get married?", "Party time?"];
  //   _.each(names, function (name) {
  //     Questions.insert({
  //       name: name,
  //       answer: 1 
  //     });
  //   });
  // }
});

