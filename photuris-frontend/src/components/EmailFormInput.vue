<template>
  <div>
    <FormulateInput
      type="email"
      v-model="email"
      :name="inputName"
      :label="inputLabel"
      :validation="[['required'], ['email']]"
      :validation-messages="emailValidationMapper"
      error-behavior="blur"
    />
  </div>
</template>

<script>
export default {
  name: "EmailFormInput",
  props: ["inputName", "inputLabel"],
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