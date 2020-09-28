mergeInto(LibraryManager.library, {

 
    uploadString: function (categoryName, fileName, link) {
  	var url = Pointer_stringify(link);
	var categoryNameJS = Pointer_stringify(categoryName);
	var fileNameJS = Pointer_stringify(fileName);

	var desertRef = firebase.storage().ref('ReferenceFrameFall2020/' + categoryNameJS + '/' + fileNameJS + '.txt');

desertRef.putString(url, 'data_url').then(function(snapshot) {
  console.log('Uploaded a data_url string!');
});
  },
 

});
