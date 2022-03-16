<template>
  <div>
    Home
    <UploadFileInput />
  </div>
</template>

<script>
import UploadFileInput from "../components/UploadFileInput.vue";
import LoginUtils from "../utilities/LoginUtils.js";

export default {
  name: "Home",
  components: { UploadFileInput },
  data() {
    return {};
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");
  },

  methods: {},
};
</script>