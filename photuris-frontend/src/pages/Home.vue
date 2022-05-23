<template>
  <div class="full-height">
    <Layout>
      <Sider breakpoint="md">
        <Menu
          @on-select="changeSelectedTab"
          active-name="all-pictures"
          theme="light"
          width="auto"
        >
          <MenuItem name="all-pictures">
            <Icon type="ios-navigate"></Icon>
            <span>all pictures</span>
          </MenuItem>
          <Submenu name="albums">
            <template slot="title">
              <Icon type="ios-navigate"></Icon>
              albums
            </template>
            <div v-for="album in albumNames" :key="album.id">
              <MenuItem :name="'album%' + album.name">{{
                album.name
              }}</MenuItem>
            </div>
            <AddAlbum />
          </Submenu>
        </Menu>
        <div slot="trigger"></div>
      </Sider>
      <Layout>
        <Content
          :style="{ margin: '20px', background: '#fff', minHeight: '260px' }"
        >
          <AllPictures
            :allAlbumNames="albumNames"
            v-if="selectedTab == 'all-pictures'"
          />
          <AlbumDisplay
            :allAlbumNames="albumNames"
            :albumName="formattedAlbumName"
            v-if="selectedTab == `album%${formattedAlbumName}`"
          />

          <UploadFileInput />
        </Content>
      </Layout>
    </Layout>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";
import AllPictures from "../components/pictures-display/AllPictures.vue";
import AlbumDisplay from "../components/pictures-display/AlbumDisplay.vue";
import UploadFileInput from "../components/UploadFileInput.vue";
import AddAlbum from "../components/AddAlbum.vue";

export default {
  name: "Home",
  components: { UploadFileInput, AllPictures, AddAlbum, AlbumDisplay },
  data() {
    return {
      albumNamesEndpoint: window.endpoints.getAlbumNames,
      selectedTab: "all-pictures",

      albumNames: [],
    };
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");
    this.albumNames = await this.getAllAlbumNames();
  },

  computed: {
    formattedAlbumName() {
      if (this.selectedTab === "all-pictures") return "";
      return this.selectedTab.split("album%")[1];
    },
  },

  methods: {
    async getAllAlbumNames() {
      return await fetch(
        this.albumNamesEndpoint + `${LoginUtils.getSessionCookieValue()}/`
      ).then((response) => response.json());
    },

    changeSelectedTab(newTabName) {
      this.selectedTab = newTabName;
    },
  },
};
</script>

<style scoped>
.ivu-layout {
  height: 100%;
}

.row {
  width: 99.9% !important;
}
</style>