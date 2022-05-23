<template>
  <div>
    <VirtualList
      style="height: 100%; overflow-y: auto"
      item-class="col-xs-6 col-md-3 col-lg-2"
      :item-style="{ padding: '10px' }"
      :wrap-style="{ width: '99.9%' }"
      :data-key="'id'"
      :data-sources="pictures"
      :data-component="pictureComponent"
      :extra-props="{ allAlbumNames }"
      @tobottom="onScrollToBottom"
    />

    <Spin size="large" fix v-if="isLoading"></Spin>
  </div>
</template>

<script>
import VirtualList from "vue-virtual-scroll-list";

import LoginUtils from "../../utilities/LoginUtils.js";
import Picture from "../Picture.vue";

export default {
  name: "Album",

  components: { VirtualList },

  props: ["albumName", "allAlbumNames"],

  data() {
    return {
      pageNumber: 1,
      getPicturesFromAlbumEndpoint: window.endpoints.getPicturesFromAlbum,

      pictureComponent: Picture,
      pictures: [],

      isLoading: true,
    };
  },

  async created() {
    this.pictures = await this.getPage();
    document.querySelector('[role="group"]').classList.add("row");
  },

  methods: {
    async getPage() {
      this.isLoading = true;

      const pictures = await fetch(
        this.getPicturesFromAlbumEndpoint +
          `${LoginUtils.getSessionCookieValue()}/${this.albumName}/${
            this.pageNumber
          }`
      ).then((response) => response.json());

      this.isLoading = false;

      this.pageNumber += 1;
      return pictures;
    },

    async onScrollToBottom() {
      this.pictures = this.pictures.concat(await this.getPage());
    },
  },
};
</script>