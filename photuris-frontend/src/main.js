import Vue from 'vue'
import VueRouter from 'vue-router'
import VueFormulate from '@braid/vue-formulate'
import VueCookies from 'vue-cookies'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'

import App from './App.vue'
import Home from './pages/Home.vue'
import Login from './pages/Login.vue'
import Register from './pages/Register.vue'
import UserProfile from './pages/UserProfile.vue'

import './style/formulate.min.css';
import './style/globals.css'
import iView from 'iview';
import 'iview/dist/styles/iview.css';
import locale from 'iview/dist/locale/en-US';

import endpoints from './endpoints.json'

Vue.config.productionTip = false;
Vue.use(VueRouter);
Vue.use(VueFormulate);
Vue.use(VueCookies);
Vue.use(iView, {locale: locale});

window.endpoints = endpoints;

const routes = [
  { path: '/', component: Home },
  { path: '/home', component: Home },
  { path: '/login', component: Login },
  { path: '/register', component: Register },
  { path: '/user-profile', component: UserProfile },
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
