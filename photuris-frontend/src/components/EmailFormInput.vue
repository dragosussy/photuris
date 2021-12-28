<template>
  <div>
    <FormulateInput
      type="email"
      v-model="email"
      name="email"
      label="email"
      :validation="[['required'], ['email']]"
      :validation-messages="emailValidationMapper"
      error-behavior="blur"
    />
  </div>
</template>

<script>
export default {
  name: "EmailFormInput",
  data() {
    return {
      email: "",
      emailValidationMapper: {
        required: "missing email.",
        email: "invalid email.",
      },
    };
  },
};
</script>