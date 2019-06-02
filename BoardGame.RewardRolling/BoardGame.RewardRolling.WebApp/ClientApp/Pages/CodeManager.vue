<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách mã quà tặng</h1></div>
            <div>
                <b-btn @click="openPopupImportCode">Nhập file</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Serial</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(item,index) in rollingCodes"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name">{{item.serial}}</div>
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

        <div>
            <popup-import-code :displayed="domElementStatuses.popupImportCode.displayed"
                               @success="uploadSuccessfully"
                               @hide="hidePopupImportCode">

            </popup-import-code>
            <popup-import-code-result 
                                      :displayed="domElementStatuses.popupImportCodeResult.displayed"
                                      :data="domElementStatuses.popupImportCodeResult.data"
                              
                                      @hide="hidePopupImportCodeResult">

            </popup-import-code-result>
        </div>
    </div>
</template>
<script>
    import pagination from '../Components/Shared/_Pagination.vue';
    import popupImportCode from '../Components/RollingCode/PopupImportCode.vue';
    import popupImportCodeResult from '../Components/RollingCode/PopupImportCodeResult.vue';

    export default {
        name: 'code-manager',
        components: {
            pagination,
            popupImportCode,
            popupImportCodeResult
        },
        data() {
            return {
                domElementStatuses: {
                    popupImportCode: {
                        displayed: false
                    },
                    popupImportCodeResult: {
                        displayed: false,
                        data: {}
                    }
                }
            };
        },
        computed: {
            rollingCodes() {
                return this.$store.state.rollingCode.filter.result.rollingCodes;
            },
            pagination() {
                return this.$store.state.rollingCode.filter.result.pagination;
            }
        },
        methods: {
            changePageIndex(pageIndex) {
                console.log(pageIndex);
            },
            changePageSize(pageSize) {
                console.log(pageSize);
            },
            openPopupImportCode() {
                this.domElementStatuses.popupImportCode.displayed = true;
            },
            hidePopupImportCode() {
                this.domElementStatuses.popupImportCode.displayed = false;
            },
            uploadSuccessfully(uploadResult) {
                this.domElementStatuses.popupImportCode.displayed = false;
                this.domElementStatuses.popupImportCodeResult.data = uploadResult;
                this.openPopupImportCodeResult();
            },
            openPopupImportCodeResult() {
                this.domElementStatuses.popupImportCodeResult.displayed = true;
            },
            hidePopupImportCodeResult() {
                this.domElementStatuses.popupImportCodeResult.displayed = false;
            }
        },
        created() {
            this.$store.dispatch('rollingCode/DO_FILTER');
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