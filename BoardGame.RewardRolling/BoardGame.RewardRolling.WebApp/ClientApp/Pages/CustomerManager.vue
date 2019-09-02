<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách khách hàng</h1></div>
            <div>
                <b-btn @click="exportExcel">Xuất file</b-btn>
            </div>
        </div>
        <customer-filter></customer-filter>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Tên</div>
                <div class="col-date">Ngày tạo</div>
                <div class="col-reward_name">Quà tặng</div>

            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(item,index) in customers"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name"><router-link :to="'/admin/customers/' + item.id + '/detail'">{{item.fullName}}</router-link></div>
                <div class="col-date">{{item.displayedCreatedAt}}</div>
                <div class="col-reward_name">{{item.reward.name}}</div>

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

        <div id="popup-container">

        </div>
    </div>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import pagination from '../Components/Shared/_Pagination.vue';
    import dateUtils from '../Utils/DateExtensions.js';
    import customerFilter from '../Components/Customer/CustomerFilter.vue';
    import customerRepository from '../Repositories/CustomerRepository';

    export default {
        name: 'campaign-manager',
        components: {
            pagination,
            customerFilter
        },
        data() {
            return {
            }
        },
        computed: {
            customers() {
                let storeCustomers = this.$store.state.customer.filter.result.customers;
                var elements = cloneDeep(storeCustomers);

                elements.forEach(element => {
                    element.displayedCreatedAt = dateUtils.format(new Date(element.createdAt), "DD/MM/YYYY");
                })
                

                return elements;
            },
            pagination() {
                return this.$store.state.customer.filter.result.pagination;
            }
        },
        methods: {
            changePageIndex(pageIndex) {
                this.$store.dispatch('customer/CHANGE_FILTER_PAGEINDEX', pageIndex);
            },
            changePageSize(pageSize) {
                this.$store.dispatch('customer/CHANGE_FILTER_PAGESIZE', pageSize);
            },
            async exportExcel() {
                var filterRequest = this.$store.state.customer.filter.request;
                var filterModel = {
                    templateName : 'nhanh',
                    createdAtMin : filterRequest.createdAtMin,
                    createdAtMax : filterRequest.createdAtMax
                };
                var exportResult = await customerRepository.exportExcel(filterModel);
                console.log(exportResult);
                window.open(exportResult.url, '_blank');
            },
            getDisplayedDate(date) {

            }
        },
        async created() {
            await this.$store.dispatch('customer/DO_FILTER')
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
            width: 200px;
        }

        .item-row .col-date {
            width: 10%;
        }
        .item-row .col-address{

            max-width: 200px;
        }
        .item-row .col-reward_name{
            max-height:200px;
        }

</style>