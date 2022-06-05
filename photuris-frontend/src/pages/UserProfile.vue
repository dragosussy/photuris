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
            <template slot="title">
              <Icon type="ios-navigate"></Icon>
              user details
            </template>
            <MenuItem name="user-info">user details</MenuItem>
          </MenuItem>
          <Submenu name="settings">
            <template slot="title">
              <Icon type="ios-navigate"></Icon>
              change settings
            </template>
            <MenuItem name="change-email">change email</MenuItem>
          </Submenu>
        </Menu>
        <div slot="trigger"></div>
      </Sider>
      <Layout>
        <Content
          :style="{ margin: '20px', background: '#fff', minHeight: '260px' }"
        >
          <ChangeEmail v-show="selectedTab == 'change-email'" />
          <UserInfo v-show="selectedTab == 'user-info'" />
        </Content>
      </Layout>
    </Layout>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";

import ChangeEmail from "../components/ChangeEmail.vue";
import UserInfo from "../components/UserInfo.vue";

export default {
  name: "UserProfile",
  components: { ChangeEmail, UserInfo },
  data() {
    return {
      selectedTab: "user-info",
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