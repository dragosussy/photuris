import Crypto from "../utilities/Crypto";
import localStorageConstants from "../constants/localStorage.json";

const KeysStorageHelper = {
    storeMasterKey: function(masterKey) {
      localStorage.setItem(localStorageConstants.masterKey, masterKey);
    },

    storePasswordDerivedKey: function(password) {
      const passwordDerivedKey = Crypto.deriveKeyFromPassword(password);
      localStorage.setItem(
        localStorageConstants.passwordDerivedKey,
        passwordDerivedKey
      );
    },

    getMasterKey: function() {
        return localStorage.getItem(localStorageConstants.masterKey);
    },

    getPasswordDerivedKey: function() {
        return localStorage.getItem(localStorageConstants.passwordDerivedKey);
    }
}

export default KeysStorageHelper;