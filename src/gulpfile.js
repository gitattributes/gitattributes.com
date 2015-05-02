/// <binding Clean='clean' />

var gulp = require("gulp"),
  del = require("del"),
  fs = require("fs");

eval("var project = " + fs.readFileSync("./project.json"));

var paths = {
  bower: "./bower_components/",
  lib: "./" + project.webroot + "/lib/"
};

gulp.task("clean", function (cb) {
  del([paths.lib], cb);
});

