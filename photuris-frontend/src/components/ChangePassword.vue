<template>
  <div id="change-password-page">
    <FormulateForm v-model="formValues" @submit="submit">
      <PasswordFormInput inputName="old_password" inputLabel="old password" />
      <PasswordFormInput inputName="new_password" inputLabel="new password" />
      <ConfirmPasswordFormInput />
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
      changePasswordEndpoint: window.endpoints.changePasswordEndpoint,
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