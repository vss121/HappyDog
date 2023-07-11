const connection = require('../dbConfig')

const doccasCtrl = {
  getDogs: async(req, res) => {
    connection.query('SELECT * FROM dog', (error, rows) => {
      if(error) throw error;
      console.log(rows);
      res.send(rosw);
    })
  }
}


module.exports = doccasCtrl