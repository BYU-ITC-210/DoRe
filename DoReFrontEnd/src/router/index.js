import { createRouter, createWebHistory } from 'vue-router'
import ZoneView from '../views/ZoneView.vue'

const routes = [
  {
    path: '/',
    name: 'home',
    component: ZoneView,
  },
  {
    path: '/login', 
    name: 'login',
    component: () => import(/* webpackChunkName: "about" */ '../views/LoginView.vue')
  },
]

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes
})

export default router
