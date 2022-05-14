<template>
  <div id="file-upload">
    <div class="full-height d-flex align-items-center justify-content-center">
      <div>
        <input
          type="file"
          accept="image/png, image/jpeg"
          @change="changePicture"
        />
        <div
          class="full-height d-flex align-items-center justify-content-center"
        >
          <FormulateInput type="button" label="upload" @click="uploadPicture" />
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import KeysStorageHelper from "../utilities/KeysStorageHelper";
import LoginUtils from "../utilities/LoginUtils";
import CryptoJs from "../utilities/Crypto";

export default {
  name: "UploadFile",
  data() {
    return {
      uploadPictureEndpoint: window.endpoints.uploadPicture,
      inputImage: null,
    };
  },

  methods: {
    changePicture(event) {
      this.inputImage = event.target.files[0];
    },

    uploadPicture() {
      // TODO: error handling when no image is uploaded
      if (this.inputImage == null) return;

      const self = this;
      CryptoJs.encryptFile(this.inputImage, KeysStorageHelper.getMasterKey())
        .then((encryptedFile) => {
          const formData = new FormData();

          formData.append("file", encryptedFile);
          formData.append(
            "datetimecreatedstring",
            new Date(self.inputImage.lastModified).toLocaleString("en-GB", {
              timeZone: "UTC",
            })
          );
          fetch(
            self.uploadPictureEndpoint + LoginUtils.getSessionCookieValue(),
            {
              method: "POST",
              mode: "cors",
              body: formData,
            }
          ).then((response) => console.log(response));
        })
        .catch((error) => console.log(error));
    },
  },
};
</script>

<style>
#file-upload {
  position: absolute;
  bottom: 0;
  right: 0;
}
</style>