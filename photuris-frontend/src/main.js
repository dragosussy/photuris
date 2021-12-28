import Vue from 'vue'
import VueRouter from 'vue-router'
import VueFormulate from '@braid/vue-formulate'
import VueCookies from 'vue-cookies'

import App from './App.vue'
import Home from './pages/Home.vue'
import Login from './pages/Login.vue'
import Register from './pages/Register.vue'

import './style/formulate.min.css';
import './style/globals.css'
import endpoints from './endpoints.json'

Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(VueFormulate);
Vue.use(VueCookies);

window.endpoints = endpoints;

const routes = [
  { path: '/', component: Home },
  { path: '/home', component: Home },
  { path: '/login', component: Login },
  { path: '/register', component: Register },
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
