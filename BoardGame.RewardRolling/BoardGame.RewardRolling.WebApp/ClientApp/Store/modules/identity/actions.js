export default {
    SET_CURRENT_USER: ({ commit, state }, payload) => {
        commit('changeCurrentUser', payload);
    }
}