import Vue from 'vue';
import VueRouter from 'vue-router';

import Home from './Pages/Home.vue'
import CodeManager from './Pages/CodeManager.vue'
import LuckyWheel from './Pages/LuckyWheel.vue'



const routes = [
    { path: '/admin/', component: Home },  
    { path: '/admin/rolling-codes', component: CodeManager },  
    { path: '/admin/lucky-wheel', component: LuckyWheel },  
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