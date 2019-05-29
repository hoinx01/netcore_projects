export default {
    SPECIES_CHANGE_EDITING_SUCCESS: (state, species) => {
        console.log(species);
        state.editing = species;
        state.filter.result.petSpecies.forEach(function (element) {
            if (element.id == state.editing.id) {
                element.name = species.name;
            }
        })
    },
    SPECIES_FILTER_SUCCESS: (state, filterResult) => {
        console.log(filterResult)
        state.filter.result = filterResult;
    },
    SPECIES_CHANGE_PAGE_INDEX: (state, pageIndex) => {
        state.filter.request.page = pageIndex;
    },
    SPECIES_CHANGE_PAGE_SIZE: (state, pageSize) => {
        state.filter.request.limit = pageSize;
    }
}