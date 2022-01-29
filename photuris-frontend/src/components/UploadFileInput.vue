<template>
  <div>
    <input type="file" accept="image/png, image/jpeg" @change="uploadPicture" />
  </div>
</template>

<script>
export default {
  name: "UploadFile",
  data() {
    return {};
  },

  created() {
    window.crypto.subtle
      .generateKey(
        {
          name: "AES-GCM",
          length: 256,
        },
        true,
        ["encrypt", "decrypt"]
      )
      .then((key) => console.log(key))
      .catch((error) => console.error(error));
  },

  methods: {
    uploadPicture(event) {
      console.log(event.target.files);
    },
  },
};
</script>