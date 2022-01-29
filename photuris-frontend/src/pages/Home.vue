<template>
  <div>
    Home
    <!-- <div>
      <button @click="test">Test IsLoggedIn</button>
      <p>{{ testIsLoggedIn }}</p>
    </div> -->
    <UploadFile />
  </div>
</template>

<script>
import UploadFile from "../components/UploadFileInput.vue";
import LoginUtils from "../utilities/LoginUtils.js";

export default {
  name: "Home",
  components: { UploadFile },
  data() {
    return {
      testIsLoggedIn: false,
    };
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");
  },

  methods: {
    // async test() {
    //   let result = await LoginUtils.isLoggedIn();
    //   this.testIsLoggedIn = result;
    // },
  },
};
</script>