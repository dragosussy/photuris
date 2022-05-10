import Vue from 'vue'
import VueRouter from 'vue-router'
import VueFormulate from '@braid/vue-formulate'
import VueCookies from 'vue-cookies'
import 'bootstrap/dist/css/bootstrap.min.css'
import 'bootstrap/dist/js/bootstrap.min.js'
import { library } from '@fortawesome/fontawesome-svg-core'
import { faCamera } from '@fortawesome/free-solid-svg-icons'
import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

import App from './App.vue'
import Home from './pages/Home.vue'
import Login from './pages/Login.vue'
import Register from './pages/Register.vue'
import UserProfile from './pages/UserProfile.vue'
import TestEncryptPage from './pages/TestEncryptPage'

import './style/formulate.min.css';
import './style/globals.css'
import endpoints from './endpoints.json'

// Font Awesome
library.add(faCamera);
Vue.component('font-awesome-icon', FontAwesomeIcon);

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
  { path: '/user-profile', component: UserProfile },
  { path: '/test-encrypt', component: TestEncryptPage }
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
