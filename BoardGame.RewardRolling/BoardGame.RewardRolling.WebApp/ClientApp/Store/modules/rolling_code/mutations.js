export default {
    changeFilterResult: (state, filterResult) => {
        console.log(filterResult);
        state.filter.result = filterResult;
    },
    changeFilterPageSize: (state, pageSize) => {
        state.filter.request.limit = pageSize;
    },
    changeFilterPageIndex: (state, pageIndex) => {
        state.filter.request.page = pageIndex;
    }
}