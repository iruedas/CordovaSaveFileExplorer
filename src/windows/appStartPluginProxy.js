
module.exports = {
    save: function(successCallback, errorCallback, strInput) {
        var fileExplorer = new SaveFileExplorer.FileExplorer();
        fileExplorer.save(strInput.title, strInput.base64)
			.then(function(response){
				successCallback(response);
			});
    }
}

require("cordova/exec/proxy").add("FileExplorer", module.exports);