<template>
    <div>
        <FormulateInput 
            type="password"
            v-model="password"
            name="password"
            label="password"
            :validation="[['required'], ['matches', /^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{7,}$/]]"
            :validation-messages="passwordValidationMapper"
            error-behavior="blur"
        />
    </div>
</template>

<script>
export default {
    data() {
        return {
            password: '',
            passwordValidationMapper: {
                required: 'missing password.',
                matches: 'password must be 7+ characters long and contain a capital letter and a number.'
            },
        };
    }
}
</script>