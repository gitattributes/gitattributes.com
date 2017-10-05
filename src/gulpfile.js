/// <binding Clean='clean' />

var gulp = require("gulp"),
  del = require("del"),
  fs = require("fs");
var bower = require("main-bower-files");
var bowerNormalize = require("gulp-bower-normalize");

var paths = {
  bower: "./bower_components/",
  lib: "./wwwroot/lib/",
  data: "./wwwroot/data/"
};

gulp.task("clean", function () {
  return del([paths.lib, paths.data]);
});

gulp.task("copy", ["clean"], function () {
  gulp.src('./data/gitattributes/*.gitattributes')
   .pipe(gulp.dest(paths.data));

  return gulp.src(bower(), { base: paths.bower })
    .pipe(bowerNormalize())
    .pipe(gulp.dest(paths.lib));
});
