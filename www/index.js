saveFileExplorer = function(title, base64, callback, errorCallback,) {
    return cordova.exec(callback, errorCallback, "FileExplorer", "save", {title: title, base64:base64});
};