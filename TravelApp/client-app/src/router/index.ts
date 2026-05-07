import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router';
import TourListsView from '../views/TourListsView.vue';

const routes: Array<RouteRecordRaw> = [
  {
    path: '/',
    name: 'TourLists',
    component: TourListsView,
  },
  {
    path: '/login',
    name: 'Login',
    component: () => import('../views/LoginView.vue'),
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
