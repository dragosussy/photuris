<template>
  <div>
    {{ index }}
    <img :src="getFormattedImage" />
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