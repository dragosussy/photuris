<template>
  <div
    id="register-page"
    class="full-height d-flex align-items-center justify-content-center"
  >
    <FormulateForm v-model="formValues" @submit="submit">
      <EmailFormInput inputLabel="email" inputName="email" />
      <PasswordFormInput inputLabel="password" inputName="password" />
      <ConfirmPasswordFormInput />
      <FormulateInput type="submit" label="register" />
    </FormulateForm>
  </div>
</template>

<script>
import PasswordFormInput from "../components/PasswordFormInput.vue";
import EmailFormInput from "../components/EmailFormInput.vue";
import ConfirmPasswordFormInput from "../components/ConfirmPasswordFormInput.vue";

import LoginUtils from "../utilities/LoginUtils.js";
import KeysStorageHelper from "../utilities/KeysStorageHelper";
import Crypto from "../utilities/Crypto";

export default {
  name: "Register",
  components: { PasswordFormInput, EmailFormInput, ConfirmPasswordFormInput },
  data() {
    return {
      registerEndpoint: window.endpoints.register,
      formValues: {},
    };
  },

  async created() {
    if (await LoginUtils.isLoggedIn()) this.$router.push("/home");
  },

  methods: {
    handleKeysPipeline() {
      const masterKey = Crypto.generateMasterKey();

      KeysStorageHelper.storeMasterKey(masterKey);
      KeysStorageHelper.storePasswordDerivedKey(this.formValues.password);
    },

    submit() {
      this.handleKeysPipeline();

      fetch(this.registerEndpoint, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: this.formValues.email,
          password: this.formValues.password,
          encryptedMasterKey: Crypto.encryptMasterKey(
            KeysStorageHelper.getMasterKey(),
            KeysStorageHelper.getPasswordDerivedKey()
          ),
        }),
      }).then((response) => {
        if (response.status === 200) {
          this.$router.push("/login");
        }
      });
    },
  },
};
</script>