import Vue from 'vue'
import Vuex from 'vuex'
import rollingCode from './modules/rolling_code/store';
import reward from './modules/reward/store';
import campaign from './modules/campaign/store';
import identity from './modules/identity/store';
import customer from './modules/customer/store';



Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        rollingCode,
        reward,
        campaign,
        identity,
        customer
    }
})