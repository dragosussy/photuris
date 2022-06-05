<template>
  <div>
    <div>
      email:
      <Input
        prefix="ios-contact"
        style="width: auto"
        v-model="email"
        disabled
      />
    </div>
    <div>
      date created:
      <Input
        prefix="ios-contact"
        style="width: auto"
        v-model="datecreated"
        disabled
      />
    </div>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";

export default {
  name: "UserInfo",
  data() {
    return {
      userDetailsEndpoint: window.endpoints.userDetails,

      email: "",
      datecreated: "",
    };
  },
  mounted() {
    fetch(this.userDetailsEndpoint + `${LoginUtils.getSessionCookieValue()}`)
      .then((response) => response.json())
      .then((details) => {
        this.email = details.email;
        this.datecreated = details.dateCreated;
      });
  },
};
</script>

<style scoped>
div {
  padding: 5px;
}
</style>