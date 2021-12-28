<template>
  <div>
    <FormulateInput
      type="password"
      v-model="password"
      name="password_confirm"
      label="confirm password"
      :validation="[['confirm']]"
      :validation-messages="passwordValidationMapper"
      error-behavior="blur"
    />
  </div>
</template>

<script>
export default {
  name: "ConfirmPasswordFormInput",
  data() {
    return {
      password: "",
      passwordValidationMapper: {
        confirm: "passwords don't match",
      },
    };
  },
};
</script>