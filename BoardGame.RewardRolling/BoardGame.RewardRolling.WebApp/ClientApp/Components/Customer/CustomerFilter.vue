<template>
    <div class="customer_filter-container">
        <div class="datetime_range">
            <div class="datetime_range-form_group datetime_range-form_group-from">
                <div class="datetime_range-form_group-title"><span>Từ ngày</span></div>
                <datetime format="DD/MM/YYYY" width="300px" v-model="displayedCreatedAtMin"></datetime>
            </div>
            <div class="datetime_range-form_group datetime_range-form_group-to">
                <div class="datetime_range-form_group-title"><span>Đến ngày</span></div>
                <datetime format="DD/MM/YYYY" width="300px" v-model="displayedCreatedAtMax"></datetime>
            </div>
        </div>
        <div class="btn">
            <b-btn @click="doFilter" size="sm">Tìm kiếm</b-btn>
        </div>
    </div>
</template>
<script>
    import dateUtils from '../../Utils/DateExtensions.js';
    import datetime from 'vuejs-datetimepicker';
    export default {
        name: 'customer-filter',
        components: {
            datetime
        },
        data() {
            return {
                filterRequest: {
                    createdAtMin: new Date(),
                    createdAtMax: new Date()
                }
            }
        },
        computed: {
            displayedCreatedAtMin: {
                get: function () {
                    return dateUtils.format(this.$store.state.customer.filter.request.createdAtMin, "DD/MM/YYYY");
                },
                set: function (value) {
                    console.log(value)
                    var parts = value.split('/');
                    var month = new Number(parts[1]) - 1;
                    var datetime = new Date(parts[2], month, parts[0], 0, 0, 0, 0);
                    this.filterRequest.createdAtMin = datetime;
                    //this.$store.dispatch('customer/CHANGE_FILTER_CREATEDATMIN', datetime)
                }
            },
            displayedCreatedAtMax: {
                get: function () {
                    return dateUtils.format(this.$store.state.customer.filter.request.createdAtMax, "DD/MM/YYYY");
                },
                set: function (value) {
                    var parts = value.split('/');
                    var month = new Number(parts[1]) - 1;
                    var datetime = new Date(parts[2], month, parts[0], 23, 59, 59, 999);
                    this.filterRequest.createdAtMax = datetime;
                    //this.$store.dispatch('customer/CHANGE_FILTER_CREATEDATMAX', datetime)
                }
                
            }
        },
        methods: {
            async doFilter() {
                this.$store.dispatch('customer/CHANGE_FILTER_CREATEDATMIN', this.filterRequest.createdAtMin)
                this.$store.dispatch('customer/CHANGE_FILTER_CREATEDATMAX', this.filterRequest.createdAtMax)
                await this.$store.dispatch('customer/DO_FILTER')
            }
        }
    }
</script>
<style scoped>
    .customer_filter-container{
        display: flex;
        margin-bottom: 10px;
    }
    .datetime_range{
        display:flex;
        justify-content: flex-start;
        align-items: center;
    }
    
    .datetime_range .datetime_range-form_group{
        display:flex;
        justify-content: flex-start;
        align-items: center;
    }

    .datetime_range-form_group-to{
        margin-left: 30px;
    }
    .datetime_range .datetime_range-form_group-title{
        margin-right: 10px;
    }
</style>