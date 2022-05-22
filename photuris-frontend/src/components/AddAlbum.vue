<template>
  <div id="add-album">
    <Button type="success" long @click="createAddAlbumModal">add album</Button>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils";

export default {
  name: "AddAlbum",

  data() {
    return {
      addAlbumEndpoint: window.endpoints.addAlbum,
    };
  },

  methods: {
    createAddAlbumModal() {
      const self = this;

      this.$Modal.confirm({
        render: (h) => {
          return h("Input", {
            data() {
              return {
                addAlbumEndpoint: self.addAlbumEndpoint,
              };
            },
            props: {
              value: this.value,
              autofocus: true,
              placeholder: "enter album name...",
            },
            on: {
              input: (val) => {
                this.value = val;
              },
            },
          });
        },
        onOk: () => {
          fetch(this.addAlbumEndpoint, {
            method: "POST",
            mode: "cors",
            headers: {
              "Content-Type": "application/json",
            },
            body: JSON.stringify({
              albumName: this.value,
              sessionToken: LoginUtils.getSessionCookieValue(),
            }),
          });
        },
      });
    },
  },
};
</script>

<style>
</style>