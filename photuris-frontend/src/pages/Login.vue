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

export default {
  name: "Login",
  components: { PasswordFormInput, EmailFormInput },
  data() {
    return {
      loginEndpoint: window.endpoints.login,
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
        this.addAuthCookie(response);
        this.$router.go(); // refresh page
      });
    },

    addAuthCookie(serverResponse) {
      serverResponse.text().then((responseString) => {
        let responseObject = JSON.parse(responseString);

        this.$cookies.set(
          "auth_token",
          responseObject.token,
          Date.parse(responseObject.expirationDate)
        );
      });
    },

    redirectToRegister() {
      this.$router.push("/register");
    },
  },
};
</script>

<style scoped>
</style>