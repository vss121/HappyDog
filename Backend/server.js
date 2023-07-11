const express = require('express');
const keys = require('./config/keys.js')
//console.log(express);
//console.log("hello world");

const app = express();

const mongoose = require('mongoose');
mongoose.connect(keys.mongoURI, {useNewUrlParser:true, useUnifiedTopology:true});

//Routes
app.get('/auth', async (req, res) => {
  console.log(req.query);
  res.send("Hello "+req.query.userId+" It is "+Date.now());
});

const port = 13756;
app.listen(keys.port, () => {
  console.log("Listening on " + keys.port);
});