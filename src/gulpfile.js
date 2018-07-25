/// <binding Clean='clean' />

var gulp = require("gulp"),
  del = require("del"),
  fs = require("fs");

var paths = {
  data: "./wwwroot/data/"
};

gulp.task("clean", function () {
  return del([paths.data]);
});

gulp.task("copy", ["clean"], function () {
  gulp.src('./data/gitattributes/*.gitattributes')
   .pipe(gulp.dest(paths.data));
});
