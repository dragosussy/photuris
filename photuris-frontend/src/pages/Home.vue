<template>
  <div>
    Home
    <!-- <div>
      <button @click="test">Test IsLoggedIn</button>
      <p>{{ testIsLoggedIn }}</p>
    </div> -->
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";

export default {
  name: "Home",
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