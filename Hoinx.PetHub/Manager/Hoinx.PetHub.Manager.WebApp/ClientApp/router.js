import Vue from 'vue';
import VueRouter from 'vue-router';

import Home from './Pages/Home.vue'
import SpeciesIndex from './Pages/Species.vue'



const routes = [
    { path: '/', component: Home },  
    { path: '/admin/pet_species', component: SpeciesIndex },  
]
Vue.use(VueRouter);

const router = new VueRouter({ mode:'history', routes: routes });

const publicPaths = [
    '/admin/accounts/login'
]

router.beforeEach((to, from, next) => {
    next();
})

export default router;