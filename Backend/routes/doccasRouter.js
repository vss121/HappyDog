const doccasCtrl = require('../controllers/doccasCtrl');
const router = require("express").Router();

// api/doccas/ 상태
router.route('/')
  .get(doccasCtrl.getDogs)
// post 할지 get 할지

module.exports = router;