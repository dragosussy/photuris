<template>
  <div id="app">
    <Menu style="display: flex" mode="horizontal" theme="dark" active-name="1">
      <MenuItem name="1">
        <Icon type="ios-camera-outline" size="32" />
        <router-link to="/home">Photuris</router-link>
      </MenuItem>
      <MenuItem name="2">
        <router-link to="/home">home</router-link>
      </MenuItem>
      <MenuItem name="4">
        <router-link to="/user-profile">user profile</router-link>
      </MenuItem>
      <MenuItem style="margin-left: auto" name="4">
        <LogoutButton></LogoutButton>
      </MenuItem>
    </Menu>

    <router-view></router-view>
  </div>
</template>

<script>
import LogoutButton from "./components/LogoutButton.vue";

export default {
  name: "App",
  components: { LogoutButton },
};
</script>

<style>
@import "./style/shared.css";

html,
body {
  height: 100%;
}

#app {
  height: 90%;
}

.nav-link {
  padding: 0;
}

.right-menu-item {
  right: -71%;
}

a {
  color: #41b883 !important;
}
</style>