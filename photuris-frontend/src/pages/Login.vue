<template>
  <div
    id="login-page"
    class="full-height d-flex align-items-center justify-content-center"
  >
    <FormulateForm v-model="formValues" @submit="submit">
      <EmailFormInput inputLabel="email" inputName="email" />
      <PasswordFormInput inputLabel="password" inputName="password" />
      <FormulateInput type="submit" label="log in" />

      <span class="error-text">{{ errorMessage }}</span>

      <div class="create-account-text">
        <p>
          don't have an account?
          <span @click="redirectToRegister" class="green-span"
            >create one.</span
          >
        </p>
      </div>
    </FormulateForm>
  </div>
</template>

<script>
import PasswordFormInput from "../components/PasswordFormInput.vue";
import EmailFormInput from "../components/EmailFormInput.vue";
import LoginUtils from "../utilities/LoginUtils.js";
import KeysStorageHelper from "../utilities/KeysStorageHelper";
import Crypto from "../utilities/Crypto";

export default {
  name: "Login",
  components: { PasswordFormInput, EmailFormInput },
  data() {
    return {
      loginEndpoint: window.endpoints.login,
      getMasterKeyEndpoint: window.endpoints.getEncryptedMasterKey,
      formValues: {},
      errorMessage: "",
    };
  },

  async created() {
    if (await LoginUtils.isLoggedIn()) this.$router.push("/home");
  },

  methods: {
    submit() {
      fetch(this.loginEndpoint, {
        method: "POST",
        mode: "cors",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          email: this.formValues.email,
          password: this.formValues.password,
        }),
      }).then((response) => {
        if (response.status !== 200) {
          response.text().then((text) => (this.errorMessage = text));
          return;
        }

        this.processLoginResponse(response);
        this.$router.go(); // refresh page
      });
    },

    processLoginResponse(response) {
      response.json().then((responseObject) => {
        console.log("auth cookie obj", responseObject);

        KeysStorageHelper.storePasswordDerivedKey(this.formValues.password);
        this.setMasterKey();
        this.addAuthCookie(responseObject);
      });
    },

    setMasterKey() {
      fetch(`${this.getMasterKeyEndpoint}?email=${this.formValues.email}`, {
        method: "GET",
        mode: "cors",
      })
        .then((response) => response.text())
        .then((encryptedMasterKey) => {
          const decryptedMasterKey = Crypto.decryptMasterKey(
            encryptedMasterKey,
            KeysStorageHelper.getPasswordDerivedKey()
          );

          console.log("decr master key login", decryptedMasterKey);

          KeysStorageHelper.storeMasterKey(decryptedMasterKey);
        });
    },

    addAuthCookie(responseObject) {
      this.$cookies.set(
        "auth_token",
        responseObject.token,
        Date.parse(responseObject.expirationDate)
      );
    },

    redirectToRegister() {
      this.$router.push("/register");
    },
  },
};
</script>

<style scoped>
</style>