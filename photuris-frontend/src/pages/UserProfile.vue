<template>
  <div id="user-profile-page">
    <Layout>
      <Sider breakpoint="md">
        <Menu
          @on-select="changeSelectedTab"
          active-name="account-details"
          theme="light"
          width="auto"
        >
          <MenuItem name="account-details">
            <Icon type="ios-navigate"></Icon>
            <span>account details</span>
          </MenuItem>
          <Submenu name="settings">
            <template slot="title">
              <Icon type="ios-navigate"></Icon>
              change settings
            </template>
            <MenuItem name="change-email">change email</MenuItem>
            <MenuItem name="change-password">change password</MenuItem>
          </Submenu>
        </Menu>
        <div slot="trigger"></div>
      </Sider>
      <Layout>
        <Content
          :style="{ margin: '20px', background: '#fff', minHeight: '260px' }"
        >
          <ChangeEmail v-show="selectedTab == 'change-email'" />
          <ChangePassword v-show="selectedTab == 'change-password'" />
        </Content>
      </Layout>
    </Layout>
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

<style>
.ivu-layout-sider {
  background: #fff !important;
}
</style>