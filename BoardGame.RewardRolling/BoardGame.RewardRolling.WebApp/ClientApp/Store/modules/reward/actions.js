import rewardRepository from '../../../Repositories/RewardRepository'

const actions = {
    DO_FILTER: async ({ commit, state }) => {
        try {
            var filterResult = await rewardRepository.filter(state.filter.request);
            commit('changeFilterResult', filterResult);
        }
        catch (exception) {
            console.log(exception);
        }
    },
    ADD_SUCCESS: async ({ commit, state }) => {
        await actions.DO_FILTER({ commit, state });
    },
    UPDATE_SUCCESS: async ({ commit, state }) => {
        await actions.DO_FILTER({ commit, state });
    },
    DELETE_SUCCESS: async ({ commit, state }) => {
        await actions.DO_FILTER({ commit, state });
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