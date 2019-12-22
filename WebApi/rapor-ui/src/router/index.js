import Vue from 'vue'
import VueRouter from 'vue-router'

import BookCrud from '../pages/book/BookCrud.vue'

// test
// import Test from '../pages/trendreports/Test.vue'

Vue.use(VueRouter)

export default new VueRouter({
  routes: [
    {
      path: '/',
      name: 'rapor ana sayfa',
      // redirect: '/materialactivity/detailedreport'
      component: BookCrud
    }
  ]
})
