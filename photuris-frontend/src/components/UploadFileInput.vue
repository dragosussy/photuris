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
import LoginUtils from "../utilities/LoginUtils";
export default {
  name: "UploadFile",
  data() {
    return {
      uploadPictureEndpoint: window.endpoints.uploadPicture,
      inputImage: null,
    };
  },

  created() {
    // window.crypto.subtle
    //   .generateKey(
    //     {
    //       name: "AES-GCM",
    //       length: 256,
    //     },
    //     true,
    //     ["encrypt", "decrypt"]
    //   )
    //   .then((key) => console.log(key))
    //   .catch((error) => console.error(error));
  },

  methods: {
    changePicture(event) {
      this.inputImage = event.target.files[0];
      console.log(this.inputImage);
    },

    uploadPicture() {
      // TODO: error handling when no image is uploaded
      if (this.inputImage == null) return;

      const formData = new FormData();
      formData.append("file", this.inputImage);
      // formData.append("metaData", {
      //   datetimecreatedstring: new Date(
      //     this.inputImage.lastModified
      //   ).toLocaleString(),
      // });
      formData.append(
        "datetimecreatedstring",
        new Date(this.inputImage.lastModified).toLocaleString()
      );
      fetch(this.uploadPictureEndpoint + LoginUtils.getSessionCookieValue(), {
        method: "POST",
        mode: "cors",
        body: formData,
      }).then((response) => console.log(response));
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