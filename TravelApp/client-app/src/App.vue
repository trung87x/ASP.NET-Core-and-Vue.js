<template>
  <v-app theme="dark">
    <v-navigation-drawer v-model="drawer" location="left" temporary>
      <v-list nav>
        <v-list-item prepend-icon="mdi-home" title="Home" to="/"></v-list-item>
        <v-list-item v-if="!isAuthenticated" prepend-icon="mdi-login" title="Login" to="/login"></v-list-item>
        <v-list-item v-else prepend-icon="mdi-logout" title="Logout" @click="handleLogout"></v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar flat border>
      <v-app-bar-nav-icon @click="drawer = !drawer"></v-app-bar-nav-icon>
      <v-app-bar-title class="font-weight-bold text-uppercase">
        <v-icon icon="mdi-airplane-takeoff" color="primary" class="mr-2"></v-icon>
        TravelApp
      </v-app-bar-title>
      <v-spacer></v-spacer>
      
      <div class="hidden-sm-and-down mr-4">
        <v-btn v-if="!isAuthenticated" to="/login" variant="text">Login</v-btn>
        <template v-else>
          <span class="text-caption mr-4">{{ user?.email }}</span>
          <v-btn variant="text" color="error" @click="handleLogout">Logout</v-btn>
        </template>
      </div>

      <v-btn icon="mdi-account-circle" :to="isAuthenticated ? '' : '/login'"></v-btn>
    </v-app-bar>

    <v-main>
      <router-view v-slot="{ Component }">
        <transition name="fade" mode="out-in">
          <component :is="Component" />
        </transition>
      </router-view>
    </v-main>

    <v-footer border class="text-center d-flex flex-column pa-4">
      <div class="mb-2">
        <v-btn v-for="icon in icons" :key="icon" :icon="icon" variant="text" size="small"></v-btn>
      </div>
      <div class="text-caption text-medium-emphasis">
        {{ new Date().getFullYear() }} — <strong>TravelApp Premium</strong>
      </div>
    </v-footer>
  </v-app>
</template>

<script setup lang="ts">
import { ref, computed } from 'vue';
import { useStore } from 'vuex';
import { useRouter } from 'vue-router';

const store = useStore();
const router = useRouter();
const drawer = ref(false);
const icons = ['mdi-facebook', 'mdi-twitter', 'mdi-linkedin', 'mdi-instagram'];

const isAuthenticated = computed(() => store.getters.isAuthenticated);
const user = computed(() => store.state.user);

const handleLogout = () => {
  store.dispatch('logout');
  router.push('/login');
};
</script>

<style>
.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.2s ease;
}

.fade-enter-from,
.fade-leave-to {
  opacity: 0;
}

body {
  font-family: 'Inter', sans-serif;
  background-color: #121212;
}
</style>
