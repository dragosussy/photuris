<template>
  <div class="">
    <img :src="displayPictureFormatted" class="img-responsive" />
  </div>
</template>

<script>
import KeysStorageHelper from "../utilities/KeysStorageHelper";
import CryptoJs from "../utilities/Crypto";

export default {
  name: "Picture",
  props: {
    index: { type: Number },
    source: {
      type: Object,
      default() {
        return {};
      },
    },
  },
  data() {
    return {
      displayPictureFormatted: "",
      pictureBlob: null,
    };
  },
  created() {
    const self = this;

    CryptoJs.decryptFile(
      new Blob([this.source.imageDataBase64]),
      KeysStorageHelper.getMasterKey()
    )
      .then((decryptedFile) => {
        self.pictureBlob = decryptedFile;
        self
          .formatImage()
          .then((result) => (self.displayPictureFormatted = result));
      })
      .catch((error) => console.log(error));
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

        reader.readAsDataURL(self.pictureBlob);
      });
    },
  },
};
</script>

<style>
img {
  display: block;
  max-width: 100%;
  height: auto;
}
</style>