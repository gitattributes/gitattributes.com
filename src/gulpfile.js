/// <binding Clean='clean' />

var gulp = require("gulp"),
  del = require("del"),
  fs = require("fs");

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
  return gulp.src('./data/gitattributes/*.gitattributes')
    .pipe(gulp.dest(paths.data));
});
