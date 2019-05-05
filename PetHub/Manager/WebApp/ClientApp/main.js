import '@babel/polyfill'
import Vue from 'vue';
import App from './App.vue';
import Router from './router';


//import Vuex from 'vuex';
//import store from './Store/store'

import BootstrapVue from 'bootstrap-vue'
Vue.use(BootstrapVue);
import 'bootstrap/dist/css/bootstrap.css'
import 'bootstrap-vue/dist/bootstrap-vue.css'

import { library } from '@fortawesome/fontawesome-svg-core'
import { faTrashAlt, faPencilAlt } from '@fortawesome/free-solid-svg-icons'

library.add(faTrashAlt, faPencilAlt)

import { FontAwesomeIcon } from '@fortawesome/vue-fontawesome'

Vue.component('font-awesome-icon', FontAwesomeIcon)
Vue.config.productionTip = false



new Vue({
    el: '#app',
    //store: store,
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