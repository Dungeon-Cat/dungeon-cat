function returnStr(returnStr) {
  const bufferSize = lengthBytesUTF8(returnStr) + 1;
  const buffer = _malloc(bufferSize);
  stringToUTF8(returnStr, buffer, bufferSize);
  return buffer;
}

mergeInto(LibraryManager.library, {
  
  Alert: function (str) {
    window.alert(UTF8ToString(str));
  },

  Save(string) {
    
  },

  Load() {
    
  }
  
})