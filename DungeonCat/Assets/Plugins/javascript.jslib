mergeInto(LibraryManager.library, {
    Alert: function (str) {
        window.alert(UTF8ToString(str));
    },

    Save: function (str) {
        const state = UTF8ToString(str)

        window.localStorage.setItem("DungeonCatLatestSave", state)
    },

    Load: function () {
        let state = window.localStorage.getItem("DungeonCatLatestSave");

        if (state == null) {
            state = ""
        }

        const bufferSize = lengthBytesUTF8(state) + 1;
        const buffer = _malloc(bufferSize);
        stringToUTF8(state, buffer, bufferSize);
        return buffer;
    }
})