<template>
  <div>
    <FormulateInput
      type="password"
      v-model="password"
      :name="inputName"
      :label="inputLabel"
      :validation="[
        ['required'],
        ['matches', /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{7,}$/],
      ]"
      :validation-messages="passwordValidationMapper"
      error-behavior="blur"
    />
  </div>
</template>

<script>
export default {
  name: "PasswordFormInput",
  props: ["inputLabel", "inputName"],
  data() {
    return {
      password: "",
      passwordValidationMapper: {
        required: "missing password.",
        matches:
          "password must be 7+ characters long and contain a capital letter and a number.",
      },
    };
  },
};
</script>