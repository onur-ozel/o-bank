const mongoose = require("mongoose");
// "flag": "https://restcountries.eu/data/ala.svg", 
// "name": "Åland Islands",
//   "alpha2Code": "AX",
//   "alpha3Code": "ALA",
//   "capital": "Mariehamn",
//   "region": "Europe",
//   "subregion": "Northern Europe",
//   "demonym": "Ålandish",
//   "nativeName": "Åland",
//     "numericCode": "248"
// }
const countrySchema = mongoose.Schema({
  flag: {
    type: String
  },
  name: {
    type: String,
    required: true
  },
  alpha2Code: {
    type: String
  },
  alpha3Code: {
    type: String,
    required: true
  },
  capital: {
    type: String
  },
  region: {
    type: String
  },
  subregion: {
    type: String
  },
  demonym: {
    type: String
  },
  nativeName: {
    type: String
  },
  numericCode: {
    type: String
  }
}, {
  collection: 'Country'
});

module.exports = mongoose.model("Country", countrySchema);