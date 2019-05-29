import speciesRepository from '../../../Repositories/SpeciesRepository'
const actions = {
    changeEditingSpecies: async ({ commit, state }, species) => {
        commit('SPECIES_CHANGE_EDITING_SUCCESS', species);
    },
    changePageIndex: async ({ commit, state }, pageIndex) => {
        commit('SPECIES_CHANGE_PAGE_INDEX', pageIndex)
        await actions.doFilter({ commit, state });
    },
    changePageSize: async ({ commit, state }, pageSize) => {
        commit('SPECIES_CHANGE_PAGE_SIZE', pageSize);
        await actions.doFilter({ commit, state });
    },
    doFilter: async ({ commit, state }) => {
        try {
            let species = await speciesRepository.filter(state.filter.request);
            commit('SPECIES_FILTER_SUCCESS', species);
        }
        catch (exception) {
            console.log(exeption)
        }

    }
}

export default actions;