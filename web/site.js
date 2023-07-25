import 'bootswatch/paper/bootstrap.css'
import '@selectize/selectize/dist/css/selectize.css'
import './site.css'

import $ from 'jquery'
import {} from '@selectize/selectize'

$(document).ready(function() {
  var ignores = JSON.parse($('script#ignores-data').html());
  var types = $('#types');
  types.selectize({
    plugins: ['restore_on_backspace', 'remove_button'],
    inputClass: 'form-control selectize-input',
    options: ignores,
    placeholder: 'Select file types to generate gitattributes file',
    valueField: 'id',
    labelField: 'name',
    searchField: ['name'],
    delimiter: ',',
    persist: false,
    create: false,
    selectOnTab: true
  });

  $('#btnGenerate').on('click', function() {
    var typesValue = types.val();
    if (typesValue.length) {
      var uriEncodedFiles = encodeURIComponent(typesValue);
      window.location="/api/"+uriEncodedFiles;
    }
  });

  $('#btnDownload').on('click', function() {
    var typesValue = types.val();
    if (typesValue.length) {
      var uriEncodedFiles = encodeURIComponent(typesValue);
      window.location="/api/f/"+uriEncodedFiles;
    }
  });
});
