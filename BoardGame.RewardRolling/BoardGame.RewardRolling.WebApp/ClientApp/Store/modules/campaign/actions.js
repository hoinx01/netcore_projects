﻿import campaignRepository from '../../../Repositories/CampaignRepository'

const actions = {
    DO_FILTER: async ({ commit, state }) => {
        try {
            var filterResult = await campaignRepository.filter(state.filter.request);
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
    }
}

export default actions;