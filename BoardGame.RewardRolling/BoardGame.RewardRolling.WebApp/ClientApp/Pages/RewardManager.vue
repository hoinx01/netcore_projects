<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách quà tặng</h1></div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Tên</div>
                <div class="col-name">Giá</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(item,index) in rewards"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name">{{item.name}}</div>
                <div class="col-name">{{item.cost}}</div>
                <div class="col-control">

                </div>
            </b-list-group-item>

        </b-list-group>
        <div id="main-footer" class="router-view-footer">
            <pagination :total="pagination.count"
                        :page-index="pagination.page"
                        :page-size="pagination.limit"
                        :paginationSize="5"
                        :displayPageSizeSelection="true"
                        @page-index-change="changePageIndex"
                        @page-size-change="changePageSize">

            </pagination>
        </div>
    </div>
</template>
<script>
    import pagination from '../Components/Shared/_Pagination.vue';

    export default {
        name: 'reward-manager',
        components: {
            pagination
        },
        data() {
            return {

            }
        },
        computed: {
            rewards() {
                return this.$store.state.reward.filter.result.rewards;
            },
            pagination() {
                return this.$store.state.reward.filter.result.pagination;
            }
        },
        methods: {
            changePageIndex(pageIndex) {

            },
            changePageSize(pageSize) {

            }
        },
        async created() {
            this.$store.dispatch('reward/DO_FILTER');
        }
    }
</script>

<style scoped>
    .item-row {
        display: flex;
    }

        .item-row .col-index {
            width: 50px;
        }

        .item-row .col-name {
            width: 30%;
        }
</style>