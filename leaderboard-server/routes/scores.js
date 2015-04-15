var express = require('express');
var router = express.Router();

// GET times of the given level
router.get('/times/:level', function(req, res) {
  console.log('GET scores');

  // Get the database object
  var db = req.db;

  // Get the level number from parameters
  var level = req.params.level;

  // Get the collection indentified by level parameter
  var collection = db.get('level' + level);

  // Find all entries and sort them by time
  collection.find({}, { sort: { time: 1} }, function (err, docs) {
  	if (err) {
  		// Handle error
  		console.error('Failed to get scores', err);
  		res.status(500).send('Failed to get scores');
  	} else {
  		// Respond with the JSON object
  		res.json(docs);
  	}
  });
});

// POST a score to the given level
router.post("/submit/:level", function(req, res) {
	console.log("POST score");

	// Get parameters from the message body
	var level = Number(req.params.level);
	var name = req.body.name;
	var time = Number(req.body.time);

	if (!(name && time)) {
		console.error("Invalid data format");
		res.status(400).send("Invalid data format");
		return;
	}

	// Get the database object and the right collection
	var db = req.db;
	var collection = db.get('level' + level);

	collection.insert({
		"name": name,
		"time": time
	}, function(err, doc) {
		if (err) {
			console.error("Writing to the DB failed", err);
			res.status(500).send("Writing to the DB failed");
		} else {
			// Return the added score
			res.json(doc);
		}
	});
});

module.exports = router;
