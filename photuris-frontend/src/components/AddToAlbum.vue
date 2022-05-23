<template>
  <div>
    <Collapse>
      <Panel :hide-arrow="true" name="1">
        <Icon type="ios-add-circle-outline" size="24" /> to album

        <CheckboxGroup
          @on-change="addPictureToAlbum"
          slot="content"
          v-model="newlySelected"
        >
          <Checkbox
            v-for="album in allAlbumNames"
            :key="album.id"
            :label="album.name"
            :disabled="isAlbumSelected(album.name)"
          >
            <span>{{ album.name }}</span>
          </Checkbox>
        </CheckboxGroup>
      </Panel>
    </Collapse>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils";

export default {
  props: ["allAlbumNames", "pictureId", "selectedAlbums"],

  data() {
    return {
      addPictureToAlbumEndpoint: window.endpoints.addPictureToAlbum,
      newlySelected: [],
    };
  },

  created() {},

  methods: {
    isAlbumSelected(albumName) {
      return (
        this.selectedAlbums.some((a) => a.name === albumName) ||
        this.newlySelected.some((a) => a === albumName)
      );
    },

    addPictureToAlbum() {
      fetch(this.addPictureToAlbumEndpoint, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          pictureId: this.pictureId,
          albumName: this.newlySelected[this.newlySelected.length - 1],
          sessionToken: LoginUtils.getSessionCookieValue(),
        }),
      }).then((response) => console.log(response));
    },
  },
};
</script>