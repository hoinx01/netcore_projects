import Vue from 'vue'
import Vuex from 'vuex'
import rollingCode from './modules/rolling_code/store';
import reward from './modules/reward/store';



Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        rollingCode,
        reward
    }
})