// getMinted

Questions = new Mongo.Collection("questions");

Template.player.helpers({
  playerName: function () {
    return Session.get("playerName");
  },
  playerId: function () {
    return Session.get("playerId");
  }
});

Template.newPlayerForm.events({
  'submit form': function(){
    event.preventDefault();
    console.log("Form submitted");
    console.log(event.type)
  }
});

Template.questionList.helpers({
  questions: function () {
    return Questions.find({}, { sort: { answer: -1, name: 1 } });
  },
  selectedQuestion: function () {
    var question = Questions.findOne(Session.get("selectedQuestion"));
    return question && question.name;
  }
});

Template.questionList.events({
  'click .inc': function () {
    Questions.update(Session.get("selectedQuestion"), {$inc: {answer: 5}});
  }
});

Template.question.helpers({
  selected: function () {
    return Session.equals("selectedQuestion", this._id) ? "selected" : '';
  }
});

Template.question.events({
  'click': function () {
    Session.set("selectedQuestion", this._id);
  }
});

// temp until user name form done
// Meteor.call("newPlayer", "MrTest", function(error, results) {
//   Session.set("playerName", results.data.Name);
//   Session.set("playerId", results.data.Id);
// });

// Meteor.startup(function () {
//   Meteor.call("getQuestion", 77, function(error, results) {
//     Session.get("playerId")
//     console.log(results.content);
//   });
// });