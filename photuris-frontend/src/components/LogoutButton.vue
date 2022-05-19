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

  data() {
    return {
      logoutEndpoint: window.endpoints.logout,
      authCookieValue: "",
    };
  },

  created() {
    this.authCookieValue = LoginUtils.getSessionCookieValue();
  },

  computed: {
    isLoggedIn() {
      return (
        this.authCookieValue !== undefined &&
        this.authCookieValue !== null &&
        this.authCookieValue !== ""
      );
    },
  },

  methods: {
    logout() {
      fetch(this.logoutEndpoint, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          sessionToken: this.authCookieValue,
        }),
      }).then(() => {
        this.$cookies.remove("auth_token");
        this.$router.go();
      });
    },
  },
};
</script>

