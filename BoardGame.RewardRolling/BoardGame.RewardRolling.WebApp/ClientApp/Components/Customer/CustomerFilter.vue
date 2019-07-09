<template>
    <b-form>
        <div class="datetime_range">
            <div><span>Ngày tạo:</span></div>
            <div class="datetime_range-form_group">
                <div><span>Từ ngày</span></div>
                <datetime format="DD/MM/YYYY" width="300px" v-model="displayedCreatedAtMin.displayedCreatedAtMin"></datetime>
            </div>
            <div class="datetime_range-form_group">
                <div><span>Đến ngày</span></div>
                <datetime format="DD/MM/YYYY" width="300px" v-model="displayedCreatedAtMax.displayedCreatedAtMax"></datetime>
            </div>
        </div>
        <div>
            <b-btn>Tìm kiếm</b-btn>
        </div>
    </b-form>
</template>
<script>
    import dateUtils from '../../Utils/DateExtensions.js';
    import datetime from 'vuejs-datetimepicker';
    export default {
        name: 'customer-filter',
        components: {
            datetime
        },
        computed: {
            displayedCreatedAtMin: {
                get: function () {
                    return dateUtils.format(new Date(), "DD/MM/YYYY");
                }
                
            },
            displayedCreatedAtMax: {
                get: function () {
                    return dateUtils.format(new Date(), "DD/MM/YYYY");
                }
                
            }
        }
    }
</script>
<style scoped>
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
</style>