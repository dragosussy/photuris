<template>
  <div id="user-profile-page">
    User Profile
    <p @click="changeSelectedTab('change-email')">change email</p>
    <ChangeEmail v-show="selectedTab == 'change-email'" />

    <p @click="changeSelectedTab('change-password')">change password</p>
    <ChangePassword v-show="selectedTab == 'change-password'" />
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";

import ChangeEmail from "../components/ChangeEmail.vue";
import ChangePassword from "../components/ChangePassword.vue";

export default {
  name: "UserProfile",
  components: { ChangeEmail, ChangePassword },
  data() {
    return {
      selectedTab: "",
    };
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");
  },

  methods: {
    changeSelectedTab(newTabName) {
      this.selectedTab = newTabName;
    },
  },
};
</script>