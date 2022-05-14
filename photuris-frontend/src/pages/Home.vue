<template>
  <div class="full-height">
    <UploadFileInput />
    <VirtualList
      style="height: 95%; overflow-y: auto"
      item-class="col-xs-6 col-md-3 col-lg-2"
      :item-style="{ padding: '10px' }"
      :data-key="'id'"
      :data-sources="pictures"
      :data-component="pictureComponent"
      @tobottom="onScrollToBottom"
    />
    <div slot="footer" class="loading-spinner">Loading ...</div>
  </div>
</template>

<script>
import VirtualList from "vue-virtual-scroll-list";

import LoginUtils from "../utilities/LoginUtils.js";
import UploadFileInput from "../components/UploadFileInput.vue";
import Picture from "../components/Picture.vue";

export default {
  name: "Home",
  components: { UploadFileInput, VirtualList },
  data() {
    return {
      pageNumber: 1,
      getPicturesEndpoint: window.endpoints.getPictures,

      pictureComponent: Picture,
      pictures: [],
    };
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");

    document.querySelector('[role="group"]').classList.add("row");

    this.pictures = await this.getPage();
  },

  methods: {
    async getPage() {
      const pictures = await fetch(
        this.getPicturesEndpoint +
          `${LoginUtils.getSessionCookieValue()}/${this.pageNumber}`
      ).then((response) => response.json());

      this.pageNumber += 1;
      return pictures;
    },

    async onScrollToBottom() {
      this.pictures = this.pictures.concat(await this.getPage());
    },
  },
};
</script>

<style>
</style>