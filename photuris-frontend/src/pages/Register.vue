<template>
  <div id="register-page">
    <FormulateForm v-model="formValues" @submit="submit">
      <EmailFormInput />
      <PasswordFormInput />
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
    submit() {
      fetch(this.registerEndpoint, {
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
        if (response.status === 200) this.$router.push("/login");
      });
    },
  },
};
</script>