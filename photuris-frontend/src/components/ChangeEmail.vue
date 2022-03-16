<template>
  <div id="change-email-page">
    <div v-show="errorMessage.length != 0">{{ errorMessage }}</div>
    <div v-show="successMessage.length != 0">{{ successMessage }}</div>
    <FormulateForm v-model="formValues" @submit="submit">
      <EmailFormInput inputLabel="new email" inputName="new_email" />
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
      changeEmailEndpoint: window.endpoints.changeEmail,
      formValues: {},
      errorMessage: "",
      successMessage: "",
    };
  },

  async created() {
    if (!(await LoginUtils.isLoggedIn())) this.$router.push("/login");
  },

  methods: {
    submit() {
      fetch(this.changeEmailEndpoint + LoginUtils.getSessionCookieValue(), {
        method: "PUT",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          newEmail: this.formValues.new_email,
        }),
      })
        .then(async (response) => {
          var text = await response.text();
          return { text: text, isOk: response.ok };
        })
        .then((response) => {
          if (!response.isOk) this.errorMessage = response.text;
          else this.successMessage = response.text;
        });
    },
  },
};
</script>