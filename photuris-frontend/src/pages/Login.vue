<template>
    <div id="login-page">
        <FormulateForm v-model="formValues" @submit="submit">
            <email-form-input />
            <password-form-input />
            <FormulateInput
                type="submit"
                label="log in"
            />
        </FormulateForm>
        <button @click="submit">Test</button>
    </div>
</template>

<script>
import PasswordFormInput from '../components/PasswordFormInput.vue'
import EmailFormInput from '../components/EmailFormInput.vue'

export default {
  components: { PasswordFormInput, EmailFormInput },
    name: "Login",
    data() {
        return {
            loginEndpoint: window.endpoints.loginApi,
            formValues: {}
        };
    },
    methods: {
        submit() {
            var self = this;

            fetch(this.loginEndpoint, {
                method: "POST", 
                mode: "cors", 
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify({ 
                    email: self.formValues.email,
                    password: self.formValues.password
                })
            })
            .then(response => console.log(response));
        }
    },
}
</script>