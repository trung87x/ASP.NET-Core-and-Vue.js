<template>
  <v-container>
    <div class="d-flex justify-space-between align-center mb-6">
      <h1 class="text-h3">Travel Destinations</h1>
      <v-btn color="primary" prepend-icon="mdi-plus" @click="openCreateDialog">
        Add Destination
      </v-btn>
    </div>

    <v-alert v-if="error" type="error" title="Error" variant="tonal" class="mb-4" closable @click:close="clearError">
      {{ error }}
    </v-alert>

    <div v-if="isLoading" class="d-flex justify-center pa-10">
      <v-progress-circular indeterminate color="primary" size="64"></v-progress-circular>
    </div>

    <v-row v-else>
      <v-col v-for="list in tourLists" :key="list.id" cols="12" md="4">
        <TourCard 
          :tour="list" 
          @edit="openEditDialog" 
          @delete="confirmDelete"
          @add-package="openAddPackageDialog"
        />
      </v-col>
    </v-row>

    <!-- Create/Edit Destination Dialog -->
    <v-dialog v-model="dialog" max-width="500px">
      <v-card rounded="xl" class="pa-4">
        <v-card-title>
          <span class="text-h5">{{ isEdit ? 'Edit Destination' : 'New Destination' }}</span>
        </v-card-title>
        <v-card-text>
          <v-form ref="form" v-model="valid">
            <v-text-field
              v-model="editedItem.city"
              label="City"
              :rules="[v => !!v || 'City is required']"
              required
              variant="outlined"
              class="mb-2"
            ></v-text-field>
            <v-text-field
              v-model="editedItem.country"
              label="Country"
              :rules="[v => !!v || 'Country is required']"
              required
              variant="outlined"
              class="mb-2"
            ></v-text-field>
            <v-textarea
              v-model="editedItem.about"
              label="About"
              variant="outlined"
              rows="3"
            ></v-textarea>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn variant="text" @click="close">Cancel</v-btn>
          <v-btn color="primary" variant="elevated" :loading="isSubmitting" :disabled="!valid" @click="save">
            Save
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Add Package Dialog -->
    <v-dialog v-model="dialogPackage" max-width="500px">
      <v-card rounded="xl" class="pa-4">
        <v-card-title>
          <span class="text-h5">Add Package to {{ currentTour?.city }}</span>
        </v-card-title>
        <v-card-text>
          <v-form v-model="validPackage">
            <v-text-field
              v-model="newPackage.name"
              label="Package Name"
              :rules="[v => !!v || 'Name is required']"
              required
              variant="outlined"
              class="mb-2"
            ></v-text-field>
            <v-text-field
              v-model.number="newPackage.price"
              label="Price"
              type="number"
              prefix="$"
              :rules="[v => v > 0 || 'Price must be positive']"
              required
              variant="outlined"
              class="mb-2"
            ></v-text-field>
          </v-form>
        </v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn variant="text" @click="dialogPackage = false">Cancel</v-btn>
          <v-btn color="primary" variant="elevated" :loading="isSubmitting" :disabled="!validPackage" @click="savePackage">
            Add
          </v-btn>
        </v-card-actions>
      </v-card>
    </v-dialog>

    <!-- Delete Confirmation Dialog -->
    <v-dialog v-model="dialogDelete" max-width="400px">
      <v-card rounded="xl" class="pa-4">
        <v-card-title class="text-h5 text-center">Confirm Delete</v-card-title>
        <v-card-text class="text-center">Are you sure you want to delete <b>{{ editedItem.city }}</b>?</v-card-text>
        <v-card-actions>
          <v-spacer></v-spacer>
          <v-btn variant="text" @click="closeDelete">Cancel</v-btn>
          <v-btn color="error" variant="elevated" :loading="isSubmitting" @click="deleteItemConfirm">Delete</v-btn>
          <v-spacer></v-spacer>
        </v-card-actions>
      </v-card>
    </v-dialog>
  </v-container>
</template>

<script setup lang="ts">
import { ref, onMounted, computed, reactive } from 'vue';
import { useStore } from 'vuex';
import type { TourList } from '../types';
import TourCard from '../components/TourCard.vue';

const store = useStore();
const tourLists = computed(() => store.getters.allTourLists);
const isLoading = computed(() => store.getters.isLoading);
const error = computed(() => store.state.error);

const dialog = ref(false);
const dialogPackage = ref(false);
const dialogDelete = ref(false);
const valid = ref(false);
const validPackage = ref(false);
const isEdit = ref(false);
const isSubmitting = ref(false);

const currentTour = ref<TourList | null>(null);

const defaultItem = {
  id: 0,
  city: '',
  country: '',
  about: '',
  packages: []
};

const editedItem = reactive({ ...defaultItem });
const newPackage = reactive({
  name: '',
  price: 0,
  listId: 0
});

onMounted(() => {
  store.dispatch('fetchTourLists');
});

const openCreateDialog = () => {
  isEdit.value = false;
  Object.assign(editedItem, defaultItem);
  dialog.value = true;
};

const openEditDialog = (item: TourList) => {
  isEdit.value = true;
  Object.assign(editedItem, item);
  dialog.value = true;
};

const openAddPackageDialog = (item: TourList) => {
  currentTour.value = item;
  newPackage.listId = item.id;
  newPackage.name = '';
  newPackage.price = 0;
  dialogPackage.value = true;
};

const confirmDelete = (item: TourList) => {
  Object.assign(editedItem, item);
  dialogDelete.value = true;
};

const close = () => {
  dialog.value = false;
};

const closeDelete = () => {
  dialogDelete.value = false;
};

const clearError = () => {
  store.commit('SET_ERROR', null);
};

const save = async () => {
  isSubmitting.value = true;
  try {
    if (isEdit.value) {
      await store.dispatch('updateTourList', editedItem);
    } else {
      await store.dispatch('createTourList', editedItem);
    }
    close();
  } catch (err) {
    console.error(err);
  } finally {
    isSubmitting.value = false;
  }
};

const savePackage = async () => {
  isSubmitting.value = true;
  try {
    await store.dispatch('createTourPackage', newPackage);
    dialogPackage.value = false;
  } catch (err) {
    console.error(err);
  } finally {
    isSubmitting.value = false;
  }
};

const deleteItemConfirm = async () => {
  isSubmitting.value = true;
  try {
    await store.dispatch('deleteTourList', editedItem.id);
    closeDelete();
  } catch (err) {
    console.error(err);
  } finally {
    isSubmitting.value = false;
  }
};
</script>
