<template>
    <div class="list-pagination">
        <div id="page-size-selection">
            <b-form-input id="page-size-input" v-model="currentPageSize"></b-form-input>
            <b-form-select 
                           id="page-size-select" 
                           v-if="displayPageSizeSelection" 
                           v-model="currentPageSize" 
                           :options="pageSizeOptions" 
                           @input="changePageSize">

            </b-form-select>
        </div>
        <b-pagination 
                      :limit="paginationSize" 
                      :total-rows="total" 
                      :per-page="pageSize" 
                      first-text="First"
                      prev-text="Prev"
                      next-text="Next"
                      last-text="Last"
                      v-model="currentPageIndex" 
                      @input="changePageIndex">

        </b-pagination>

    </div>
</template>
<script>
    export default {
        name: "pagination",
        data() {
            return {
                pageSizeOptions: [
                    { value: 5, text: '5' },
                    { value: 10, text: '10' },
                    { value: 20, text: '20' },
                    { value: 50, text: '50' },
                    { value: 100, text: '100' }
                ],
                currentPageIndex: this.pageIndex,
                currentPageSize: this.pageSize
            }
        },
        props: {
            total: {
                type: Number,
                required: true
            },
            pageIndex: {
                type: Number,
                required: true
            },
            pageSize: {
                type: Number,
                required: true
            },
            paginationSize: {
                type: Number,
                required: true
            },
            displayPageSizeSelection: {
                type: Boolean,
                required: true
            }
        },
        methods: {
            changePageIndex: function (pageIndex) {
                this.$emit('page-index-change', pageIndex)
            },
            changePageSize: function (pageSize) {
                this.$emit('page-size-change', pageSize);
            }
        }
    }
</script>
<style scoped>
    #page-size-selection{
        margin-right:20px;
        display:flex;
    }
    #page-size-select{
        width:15px;
    }
</style>