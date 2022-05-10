<template>
  <div>
    <img :src="displayImage" />
    <input type="file" accept="image/png, image/jpeg" @change="changePicture" />
    <button @click="testEncrypt">Encrypt!</button>
    <button @click="testDecrypt">Decrypt!</button>
  </div>
</template>

<script>
import Crypto from "../utilities/Crypto";

export default {
  data() {
    return {
      inputImage: null,
      displayImage: null,
    };
  },
  methods: {
    formatImage() {
      const self = this;
      return new Promise(function (resolve, reject) {
        var reader = new FileReader();

        reader.onload = function (e) {
          resolve(e.target.result);
        };
        reader.onerror = () => reject("error formatting img.");

        reader.readAsDataURL(self.inputImage);
      });
    },
    changePicture(event) {
      const self = this;
      this.inputImage = event.target.files[0];
      this.formatImage().then((result) => (self.displayImage = result));
    },

    testEncrypt() {
      const self = this;
      Crypto.encryptFile(this.inputImage, "test")
        .then((encryptedFile) => {
          console.log(encryptedFile);
          self.inputImage = encryptedFile;
          self.formatImage().then((result) => (self.displayImage = result));
        })
        .catch((error) => console.log(error));
    },
    testDecrypt() {
      const self = this;
      Crypto.decryptFile(this.inputImage, "test")
        .then((decryptedFile) => {
          console.log(decryptedFile);
          self.inputImage = decryptedFile;
          self.formatImage().then((result) => (self.displayImage = result));
        })
        .catch((error) => console.log(error));
    },
  },
};
</script>