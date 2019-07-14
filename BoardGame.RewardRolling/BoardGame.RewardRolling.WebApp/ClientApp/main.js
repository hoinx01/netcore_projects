import '@babel/polyfill'
import Vue from 'vue';
import App from './App.vue';
import Router from './router';


import Vuex from 'vuex';
import store from './Store/store-collector'

import BootstrapVue from 'bootstrap-vue'
Vue.use(BootstrapVue);
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import { library } from '@fortawesome/fontawesome-svg-core'
import { faTrashAlt, faPencilAlt, faPlus, faPlusSquare } from '@fortawesome/free-solid-svg-icons'
import { faMinus } from "@fortawesome/free-solid-svg-icons/faMinus";

library.add(faTrashAlt, faPencilAlt, faPlus, faPlusSquare, faMinus);

import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

Vue.component('font-awesome-icon', FontAwesomeIcon)
Vue.config.productionTip = false
Vue.config.devtools = process.env.NODE_ENV === 'development'


var app = new Vue({
    el: '#app',
    store: store,
    template: '<App/>',
    components: { App },
    router: Router,
    created() {
        //let currentAccount = this.$store.state.myAccount.currentAccount;
        //if (currentAccount == null) {
        //    let accountItem = localStorage.getItem('account');
        //    if (accountItem != undefined && accountItem != null) {
        //        currentAccount = JSON.parse(accountItem);
        //        this.$store.dispatch('myAccount/setCurrentAccount', currentAccount);
        //    }
        //}
        //if (currentAccount == null)
        //    this.$router.push('/admin/login')
    }
});

window.__VUE_DEVTOOLS_GLOBAL_HOOK__.Vue = app.constructor;