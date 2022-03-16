<template>
  <div id="login-page">
    <FormulateForm v-model="formValues" @submit="submit">
      <EmailFormInput />
      <PasswordFormInput inputLabel="password" inputName="password" />
      <FormulateInput type="submit" label="log in" />
    </FormulateForm>

    <div>
      <p>
        don't have an account?
        <span @click="redirectToRegister" class="green-span">create one</span>
      </p>
    </div>
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
        if (response.status !== 200) return;
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