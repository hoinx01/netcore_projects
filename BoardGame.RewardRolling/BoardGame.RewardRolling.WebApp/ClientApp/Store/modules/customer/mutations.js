export default {
    changeFilterResult: (state, filterResult) => {
        console.log(filterResult);
        state.filter.result = filterResult;
    }
}