import Vue from 'vue'
import VueRouter from 'vue-router'
import VueFormulate from '@braid/vue-formulate'

import App from './App.vue'
import Login from './pages/Login.vue'

import './style/formulate.min.css';
import endpoints from './endpoints.json'

Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(VueFormulate);

window.endpoints = endpoints;

const routes = [
  { path: '/', component: Login },
  { path: '/login', component: Login },
];
const router = new VueRouter({
  routes: routes,
  mode: 'history'
});

new Vue({
  router,
  components: {
    App
  },
  render: h => h(App),
}).$mount('#app');
