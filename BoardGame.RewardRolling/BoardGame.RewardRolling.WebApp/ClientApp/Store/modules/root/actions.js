const actions = {
    hover: ({ commit, state }) => {
        commit('changeHover', true);
    },
    unhover: ({ commit, state }) => {
        commit('changeHover', false);
    }
}

export default actions;