<template>
  <div v-if="isLoggedIn">
    <a class="nav-link" href="#" @click="logout">
      <Icon type="md-log-out" size="24" />
      Sign out
    </a>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils";

export default {
  name: "Logout",

  computed: {
    isLoggedIn() {
      const authCookieValue = LoginUtils.getSessionCookieValue();
      return (
        authCookieValue !== undefined &&
        authCookieValue !== null &&
        authCookieValue !== ""
      );
    },
  },

  methods: {
    logout() {},
  },
};
</script>

