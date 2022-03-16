<template>
  <div>
    <FormulateInput
      type="password"
      v-model="password"
      :name="inputToConfirm"
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
  props: ["inputToConfirmName"],
  data() {
    return {
      password: "",
      inputToConfirm:
        this.inputToConfirmName != null && this.inputToConfirmName.length != 0
          ? `${this.inputToConfirmName}_confirm`
          : "password_confirm",
      passwordValidationMapper: {
        confirm: "passwords don't match",
      },
    };
  },
};
</script>