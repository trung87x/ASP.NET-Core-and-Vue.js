import { createStore } from 'vuex';
import api from '../api/api';
import type { TourList } from '../types';

export default createStore({
  state: {
    tourLists: [] as TourList[],
    loading: false,
    error: null as string | null,
    token: localStorage.getItem('token') || '',
    user: JSON.parse(localStorage.getItem('user') || 'null'),
  },
  mutations: {
    SET_TOUR_LISTS(state, lists: TourList[]) {
      state.tourLists = lists;
    },
    SET_LOADING(state, loading: boolean) {
      state.loading = loading;
    },
    SET_ERROR(state, error: string | null) {
      state.error = error;
    },
    SET_AUTH(state, { token, user }) {
      state.token = token;
      state.user = user;
      localStorage.setItem('token', token);
      localStorage.setItem('user', JSON.stringify(user));
    },
    LOGOUT(state) {
      state.token = '';
      state.user = null;
      localStorage.removeItem('token');
      localStorage.removeItem('user');
    }
  },
  actions: {
    async fetchTourLists({ commit }) {
      commit('SET_LOADING', true);
      try {
        const response = await api.get<TourList[]>('/TourLists');
        commit('SET_TOUR_LISTS', response.data);
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || error.message || 'Failed to fetch tour lists');
      } finally {
        commit('SET_LOADING', false);
      }
    },
    async login({ commit }, credentials) {
      try {
        const response = await api.post('/Account/login', credentials);
        commit('SET_AUTH', { token: response.data.token, user: { email: response.data.email } });
        return response.data;
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.message || 'Login failed');
        throw err;
      }
    },
    logout({ commit }) {
      commit('LOGOUT');
    },
    async createTourList({ commit, dispatch }, list: TourList) {
      try {
        await api.post<number>('/TourLists', list);
        await dispatch('fetchTourLists');
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || 'Failed to create tour list');
        throw err;
      }
    },
    async updateTourList({ commit, dispatch }, list: TourList) {
      try {
        await api.put(`/TourLists/${list.id}`, list);
        await dispatch('fetchTourLists');
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || 'Failed to update tour list');
        throw err;
      }
    },
    async deleteTourList({ commit, dispatch }, id: number) {
      try {
        await api.delete(`/TourLists/${id}`);
        await dispatch('fetchTourLists');
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || 'Failed to delete tour list');
        throw err;
      }
    },
    async createTourPackage({ commit, dispatch }, pkg: any) {
      try {
        await api.post('/TourPackages', pkg);
        await dispatch('fetchTourLists');
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || 'Failed to create tour package');
        throw err;
      }
    },
    async updateTourPackage({ commit, dispatch }, pkg: any) {
      try {
        await api.put(`/TourPackages/${pkg.id}`, pkg);
        await dispatch('fetchTourLists');
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || 'Failed to update tour package');
        throw err;
      }
    },
    async deleteTourPackage({ commit, dispatch }, id: number) {
      try {
        await api.delete(`/TourPackages/${id}`);
        await dispatch('fetchTourLists');
      } catch (err: unknown) {
        const error = err as any;
        commit('SET_ERROR', error.response?.data?.error || 'Failed to delete tour package');
        throw err;
      }
    }
  },
  getters: {
    allTourLists: (state) => state.tourLists,
    isLoading: (state) => state.loading,
    isAuthenticated: (state) => !!state.token,
  },
});
