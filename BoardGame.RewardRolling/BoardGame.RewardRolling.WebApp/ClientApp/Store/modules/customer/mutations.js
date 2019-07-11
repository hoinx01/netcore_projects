export default {
    changeFilterResult: (state, filterResult) => {
        console.log(filterResult);
        state.filter.result = filterResult;
    },
    changeFilterCreatedAtMin: (state, datetime) => {
        state.filter.request.createdAtMin = datetime;
    },
    changeFilterCreatedAtMax: (state, datetime) => {
        state.filter.request.createdAtMax = datetime;
    },
    changeFilterPageSize: (state, pageSize) => {
        state.filter.request.limit = pageSize;
    },
    changeFilterPageIndex: (state, pageIndex) => {
        state.filter.request.page = pageIndex;
    }

}