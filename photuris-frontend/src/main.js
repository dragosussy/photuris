import Vue from 'vue'
import VueRouter from 'vue-router'

import App from './App.vue'
import Test from './pages/Test.vue'

Vue.config.productionTip = false
Vue.use(VueRouter);

const routes = [
  { path: '/', component: Test },
  // { path: '/login', component: Login },
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
}).$mount('#app')
