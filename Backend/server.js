const express = require('express');
const fs = require('fs');
const cors = require('cors');
const path = require('path');
const bodyParser = require('body-parser');

const app = express();
app.use(bodyParser.json());
app.use(bodyParser.urlencoded({extended: true}));
app.use(express.json());
app.use(cors());

const port = 4000;
app.listen(port, ()=> {
  console.log("port"+port);
})

app.use('/api/doccas', require('./routes/doccasRouter'));


//const keys = require('./config/keys.js')

// const mongoose = require('mongoose');
// mongoose.connect(keys.mongoURI, {useNewUrlParser:true, useUnifiedTopology:true});

// //Routes
// app.get('/auth', async (req, res) => {
//   console.log(req.query);
//   res.send("Hello "+req.query.userId+" It is "+Date.now());
// });

// const port = 13756;
// app.listen(keys.port, () => {
//   console.log("Listening on " + keys.port);
// });