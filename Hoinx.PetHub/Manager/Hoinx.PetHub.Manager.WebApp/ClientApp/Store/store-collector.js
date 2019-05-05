import Vue from 'vue'
import Vuex from 'vuex'
import species from './modules/species/store'



Vue.use(Vuex)

export default new Vuex.Store({
    modules: {
        species
    }
})