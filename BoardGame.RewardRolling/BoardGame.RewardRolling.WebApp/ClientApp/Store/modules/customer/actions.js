import customerRepository from '../../../Repositories/CustomerRepository.js'

export default {
    DO_FILTER: async ({ commit, state }) => {
        try {
            var filterResult = await customerRepository.filter(state.filter.request);
            commit('changeFilterResult', filterResult);
        }
        catch (exception) {
            console.log(exception);
        }
    }
}