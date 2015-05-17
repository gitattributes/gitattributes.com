/// <binding Clean='clean' />

var gulp = require("gulp"),
  del = require("del"),
  fs = require("fs");
var bower = require("main-bower-files");
var bowerNormalize = require("gulp-bower-normalize");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
  bower: "./bower_components/",
  lib: "./" + project.webroot + "/lib/",
  data: "./" + project.webroot + "/data/"
};

gulp.task("clean", function (cb) {
  del([paths.lib, paths.data], cb);
});

gulp.task("copy", ["clean"], function () {
  gulp.src('./data/gitattributes/*.gitattributes')
   .pipe(gulp.dest(paths.data));

  return gulp.src(bower(), { base: paths.bower })
    .pipe(bowerNormalize())
    .pipe(gulp.dest(paths.lib));
});
