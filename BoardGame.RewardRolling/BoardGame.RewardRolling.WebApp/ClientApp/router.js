import Vue from 'vue';
import VueRouter from 'vue-router';

import Home from './Pages/Home.vue'
import CodeManager from './Pages/CodeManager.vue'
import RewardManager from './Pages/RewardManager.vue'
import LuckyWheel from './Pages/LuckyWheel.vue'



const routes = [
    { path: '/admin/', component: Home },  
    { path: '/admin/rolling_codes', component: CodeManager },  
    { path: '/admin/rewards', component: RewardManager },  
    { path: '/admin/lucky_wheel', component: LuckyWheel },  
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