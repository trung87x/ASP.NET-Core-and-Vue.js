<template>
  <v-card border flat class="h-100 d-flex flex-column rounded-xl overflow-hidden shadow-sm hover-card">
    <v-img
      :src="imageUrl"
      height="200"
      cover
    >
      <div class="d-flex justify-end pa-2 bg-gradient-bottom">
        <v-btn icon="mdi-pencil" size="small" color="white" class="mr-2" @click="$emit('edit', tour)"></v-btn>
        <v-btn icon="mdi-delete" size="small" color="error" @click="$emit('delete', tour)"></v-btn>
      </div>
    </v-img>
    
    <v-card-item>
      <v-card-title class="text-h5 font-weight-bold">{{ tour.city }}</v-card-title>
      <v-card-subtitle class="text-primary font-weight-medium">{{ tour.country }}</v-card-subtitle>
    </v-card-item>

    <v-card-text class="flex-grow-1">
      <p class="text-body-1 text-medium-emphasis mb-4">{{ tour.about }}</p>
      
      <v-divider class="mb-4"></v-divider>
      
      <div class="d-flex align-center justify-space-between mb-2">
        <h3 class="text-subtitle-1 font-weight-bold">Available Packages</h3>
        <v-chip size="x-small" color="primary" variant="tonal">{{ tour.packages?.length || 0 }} Packages</v-chip>
      </div>

      <v-list density="compact" bg-color="transparent">
        <v-list-item v-for="pkg in tour.packages" :key="pkg.id" class="px-0">
          <template v-slot:prepend>
            <v-icon icon="mdi-package-variant-closed" size="small" color="primary" class="mr-2"></v-icon>
          </template>
          <v-list-item-title class="text-body-2">{{ pkg.name }}</v-list-item-title>
          <template v-slot:append>
            <span class="text-success font-weight-bold text-body-2">${{ pkg.price }}</span>
          </template>
        </v-list-item>
      </v-list>
    </v-card-text>
  </v-card>
</template>

<script setup lang="ts">
import { defineProps, defineEmits } from 'vue';
import type { TourList } from '../types';

// Rule 11: Props must be defined with types
const props = defineProps<{
  tour: TourList;
  imageUrl?: string;
}>();

// Default values for optional props
const imageUrl = props.imageUrl || 'https://images.unsplash.com/photo-1476514525535-07fb3b4ae5f1?auto=format&fit=crop&w=800&q=80';

// Rule 11: Emits must be declared
defineEmits<{
  (e: 'edit', tour: TourList): void;
  (e: 'delete', tour: TourList): void;
}>();
</script>

<style scoped>
.hover-card {
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
}
.hover-card:hover {
  transform: translateY(-8px);
  box-shadow: 0 12px 24px rgba(0,0,0,0.3) !important;
  border-color: rgba(var(--v-theme-primary), 0.5) !important;
}

.bg-gradient-bottom {
  background: linear-gradient(to bottom, rgba(0,0,0,0.4), transparent);
}
</style>
