import CryptoJs from 'crypto-js'

function convertWordArrayToUint8Array(wordArray) {
    var len = wordArray.words.length,
        u8_array = new Uint8Array(len << 2),
        offset = 0, word, i
    ;
    for (i=0; i<len; i++) {
        word = wordArray.words[i];
        u8_array[offset++] = word >> 24;
        u8_array[offset++] = (word >> 16) & 0xff;
        u8_array[offset++] = (word >> 8) & 0xff;
        u8_array[offset++] = word & 0xff;
    }
    return u8_array;
}

const Crypto = {
    encryptFile: function(file, key) {
        return new Promise(function(resolve, reject){
            var reader = new FileReader();

            reader.onload = () => {
                var wordArray = CryptoJs.lib.WordArray.create(reader.result);
                var encrypted = CryptoJs.AES.encrypt(wordArray, key).toString();

                resolve(new Blob([encrypted]));
            };
            reader.onerror = () => reject("error encrypting data.");

            reader.readAsArrayBuffer(file);
        })
    },

    decryptFile: function(blob, key) {
        return new Promise(function(resolve, reject) {
            var reader = new FileReader();

            reader.onload = () => {
                var decrypted = CryptoJs.AES.decrypt(reader.result, key);
                var typedArray = convertWordArrayToUint8Array(decrypted);

                resolve(new Blob([typedArray]));
            }
            reader.onerror = () => reject("error decrypting data.");

            reader.readAsText(blob)
        });
    }
}

export default Crypto;