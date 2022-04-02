<template>
  <div class="col-xs-12 col-md-3">
    <!-- <div> -->
    <img :src="getFormattedImage" class="img-responsive" />
  </div>
</template>

<script>
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
    return {};
  },
  computed: {
    getFormattedImage() {
      return `data:image/jpeg;base64, ${this.source.binaryImageData}`;
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