<template>
  <v-container class="fill-height">
    <v-row justify="center" align="center">
      <v-col cols="12" sm="8" md="4">
        <v-card rounded="xl" class="pa-8 shadow-lg">
          <v-card-title class="text-center mb-6">
            <div class="text-h4 font-weight-bold mb-2">Welcome Back</div>
            <div class="text-body-1 text-medium-emphasis">Please login to your account</div>
          </v-card-title>
          
          <v-form v-model="valid" @submit.prevent="handleLogin">
            <v-text-field
              v-model="email"
              label="Email Address"
              prepend-inner-icon="mdi-email-outline"
              variant="outlined"
              :rules="[v => !!v || 'Email is required', v => /.+@.+\..+/.test(v) || 'Email must be valid']"
              required
              class="mb-4"
            ></v-text-field>

            <v-text-field
              v-model="password"
              label="Password"
              prepend-inner-icon="mdi-lock-outline"
              :append-inner-icon="showPassword ? 'mdi-eye' : 'mdi-eye-off'"
              :type="showPassword ? 'text' : 'password'"
              variant="outlined"
              :rules="[v => !!v || 'Password is required']"
              required
              @click:append-inner="showPassword = !showPassword"
              class="mb-6"
            ></v-text-field>

            <v-btn
              color="primary"
              size="large"
              block
              rounded="pill"
              type="submit"
              :loading="loading"
              :disabled="!valid"
            >
              Login
            </v-btn>
          </v-form>

          <v-divider class="my-8"></v-divider>

          <div class="text-center text-body-2">
            Don't have an account? <a href="#" class="text-primary font-weight-bold">Sign up</a>
          </div>
        </v-card>
      </v-col>
    </v-row>
  </v-container>
</template>

<script setup lang="ts">
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const valid = ref(false);
const email = ref('');
const password = ref('');
const showPassword = ref(false);
const loading = ref(false);

const handleLogin = async () => {
  if (!valid.value) return;
  
  loading.ref = true;
  // Rule 16: Authentication logic would go here
  setTimeout(() => {
    loading.value = false;
    router.push('/');
  }, 1000);
};
</script>
