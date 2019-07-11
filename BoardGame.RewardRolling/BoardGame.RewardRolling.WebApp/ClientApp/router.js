import Vue from 'vue';
import VueRouter from 'vue-router';

import Home from './Pages/Home.vue'
import CodeManager from './Pages/CodeManager.vue'
import RewardManager from './Pages/RewardManager.vue'
import CampaignManager from './Pages/CampaignManager.vue'
import CampaignCreate from './Pages/CampaignCreate.vue'
import CampaignDetail from './Pages/CampaignDetail.vue'
import LuckyWheel from './Pages/LuckyWheel.vue'
import Login from './Pages/Login.vue'
import MyAccount from './Pages/MyAccount.vue'
import ChangePassword from './Pages/ChangePassword.vue'
import CreateUser from './Pages/CreateUser.vue'
import CustomerManager from './Pages/CustomerManager.vue'




const routes = [
    { path: '/admin/', component: Home },  
    { path: '/admin/rolling_codes', component: CodeManager },  
    { path: '/admin/rewards', component: RewardManager },  
    { path: '/admin/lucky_wheel', component: LuckyWheel },  
    { path: '/admin/campaigns', component: CampaignManager },  
    { path: '/admin/campaigns/:id/detail', component: CampaignDetail, name: 'CampaignDetail' },
    { path: '/admin/campaigns/create', component: CampaignCreate },  
    { path: '/admin/users/login', component: Login },  
    { path: '/admin/me', component: MyAccount },  
    { path: '/admin/change_password', component: ChangePassword },  
    { path: '/admin/users/create', component: CreateUser },  
    { path: '/admin/customers', component: CustomerManager }
    
]
Vue.use(VueRouter);

const router = new VueRouter({ mode:'history', routes: routes });

const publicPaths = [
    '/admin/accounts/login'
]

router.beforeEach((to, from, next) => {
    next();
})
console.log("a");
export default router;