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
    }
}

export default actions;