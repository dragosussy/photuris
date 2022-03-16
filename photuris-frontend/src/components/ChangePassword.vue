<template>
  <div id="change-password-page">
    <div v-show="errorMessage.length != 0">{{ errorMessage }}</div>
    <div v-show="successMessage.length != 0">{{ successMessage }}</div>
    <FormulateForm v-model="formValues" @submit="submit">
      <PasswordFormInput inputName="old_password" inputLabel="old password" />
      <PasswordFormInput inputName="new_password" inputLabel="new password" />
      <ConfirmPasswordFormInput inputToConfirmName="new_password" />
      <FormulateInput type="submit" label="change" />
    </FormulateForm>
  </div>
</template>

<script>
import LoginUtils from "../utilities/LoginUtils.js";

import PasswordFormInput from "./PasswordFormInput.vue";
import ConfirmPasswordFormInput from "./ConfirmPasswordFormInput.vue";

export default {
  name: "ChangePassword",
  components: { PasswordFormInput, ConfirmPasswordFormInput },
  data() {
    return {
      changePasswordEndpoint: window.endpoints.changePassword,
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
      fetch(this.changePasswordEndpoint + LoginUtils.getSessionCookieValue(), {
        method: "PUT",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          oldPassword: this.formValues.old_password,
          newPassword: this.formValues.new_password,
          newPasswordConfirmation: this.formValues.new_password_confirm,
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