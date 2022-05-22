<template>
  <div class="">
    <img :src="displayPictureFormatted" class="img-responsive" />
    <div @click="addPictureToAlbum">
      <Icon type="ios-add-circle-outline" size="24" /> to album
    </div>
  </div>
</template>

<script>
import KeysStorageHelper from "../utilities/KeysStorageHelper";
import CryptoJs from "../utilities/Crypto";
import LoginUtils from "../utilities/LoginUtils";

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
      addPictureToAlbumEndpoint: window.endpoints.addPictureToAlbum,
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

    addPictureToAlbum() {
      fetch(this.addPictureToAlbumEndpoint, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          pictureId: this.source.id,
          albumName: "test",
          sessionToken: LoginUtils.getSessionCookieValue(),
        }),
      }).then((response) => console.log(response));
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