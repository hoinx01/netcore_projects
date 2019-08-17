import rollingCodeRepository from '../../../Repositories/RollingCodeRepository'
const actions = {
    DO_FILTER: async ({ commit, state }) => {
        try {
            var filterResult = await rollingCodeRepository.filter(state.filter.request);
            commit('changeFilterResult', filterResult);
        }
        catch (exception) {
            console.log(exception);
        }
    },
    CHANGE_FILTER_PAGESIZE: async ({ commit, state }, payload) => {
        commit('changeFilterPageSize', payload);
        await actions.DO_FILTER({ commit, state });

    },
    CHANGE_FILTER_PAGEINDEX: async ({ commit, state }, payload) => {
        commit('changeFilterPageIndex', payload);
        await actions.DO_FILTER({ commit, state });

    }
}

export default actions;