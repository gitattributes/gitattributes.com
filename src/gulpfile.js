/// <binding Clean='clean' />

const gulp = require("gulp");
const del = require("del");
const fs = require("fs");

const paths = {
  data: "./wwwroot/data/"
};

function clean() {
  return del([paths.data]);
};

function copy() {
  return gulp.src('./data/gitattributes/*.gitattributes')
   .pipe(gulp.dest(paths.data));
};

const build = gulp.series(clean, copy);

exports.build = build;
exports.clean = clean;
exports.default = build;
