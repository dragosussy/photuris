<template>
  <div id="change-email-page">
    <FormulateForm v-model="formValues" @submit="submit">
      <EmailFormInput inputLabel="new email" :inputName="new_email" />
      <FormulateInput type="submit" label="change" />
    </FormulateForm>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";

import EmailFormInput from "./EmailFormInput.vue";

export default {
  name: "ChangeEmail",
  components: { EmailFormInput },
  data() {
    return {
      changeEmailEndpoint: window.endpoints.changeEmailEndpoint,
      formValues: {},
    };
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");
  },

  methods: {
    submit() {
      return;
    },
  },
};
</script>