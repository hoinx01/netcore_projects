const state = {
    editing: {
        id: null
    }
}
const actions = {
    SPECIES_CHANGE_EDITING: ({ commit, state }, speciesId) => {
        commit('changeEditingSpeciesId', speciesId);
    }
}
const mutations = {
    changeEditingSpeciesId: (state, speciesId) => {
        state.editing.id = speciesId;
    }
}
export default {
    namespaced: true,
    state: state,
    actions: actions,
    mutations: mutations
}