import speciesRepository from '../../../Repositories/SpeciesRepository'
export default {
    SPECIES_CHANGE_EDITING: async ({ commit, state }, speciesId) => {
        try {
            let species = await speciesRepository.getById(speciesId);
            commit('changeEditingSpecies', species);
        }
        catch (exception) {

        }

    }
}