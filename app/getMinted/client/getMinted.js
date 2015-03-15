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
    var playerNameVar = event.target.playerName.value;
    Session.set("playerName", playerNameVar);
    Session.set("questionPending", true);
    Meteor.call("newPlayer", playerNameVar, function(error, results) {
      Session.set("playerName", results.data.Name);
      Session.set("playerId", results.data.Id);
      Meteor.call("addQuestion", results.data.Id, function(error, newId) {
        Session.set("selectedQuestion", newId);
        Session.set("questionPending", false);
      });
    });
  }
});

Template.questionList.helpers({
  questions: function () {
    return Questions.find({playerId: Session.get("playerId")}, { sort: { questionId: 1 } });
  },
  selectedQuestion: function () {
    var selected = Questions.findOne(Session.get("selectedQuestion"));
    return selected && selected.question;
  },
  selectedOptions: function () {
    var selected = Questions.findOne(Session.get("selectedQuestion"));
    console.log(selected.question.QuestionOptions);
    return selected && selected.question.QuestionOptions;
  }
});

Template.questionList.events({
  'click .option': function () {
    console.log(event.target)
    // Questions.update(Session.get("selectedQuestion"), {$inc: {answer: 5}});
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