<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách đợt tặng quà</h1></div>
            <div>
                <b-btn @click="startCreating">Thêm</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Tên</div>
                <div class="col-date">Ngày bắt đầu</div>
                <div class="col-date">Ngày kết thúc</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(item,index) in visibleRewards"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name"><router-link :to="'/admin/campaigns/' + item.id + '/detail'">{{item.name}}</router-link></div>
                <div class="col-date">{{item.startedAt}}</div>
                <div class="col-date">{{item.endedAt}}</div>
                <div class="col-control">
                    <font-awesome-icon :icon="['fa', 'trash-alt']"
                                       @click="startDeleting(item.id)" />
                    <font-awesome-icon :icon="['fa', 'pencil-alt']"
                                       @click="startUpdating(item.id)" />
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

        <div id="popup-container">
            
        </div>
    </div>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import pagination from '../Components/Shared/_Pagination.vue';

    export default {
        name: 'campaign-manager',
        components: {
            pagination,
        },
        data() {
            return {
                currentAction: {
                    name: '',
                    status:'',
                    object: null
                }
            }
        },
        computed: {
            visibleRewards: function () {
                let elements = cloneDeep(this.$store.state.campaign.filter.result.campaigns);
                if (this.currentAction.name == 'create') {
                    if (this.currentAction.object == null) {
                        let creating = { name: '', cost: '' };                        
                        elements.unshift(creating);
                    }
                    else
                        elements.unshift(this.currentAction.object);
                }
                elements.forEach((element) => {
                    element.actionStatus = 'stable';
                    if (this.currentAction.name == 'updating') {
                        if (this.currentAction.object.id == element.id) {
                            element.actionStatus = 'updating';
                            element.name = this.currentAction.object.name;
                            element.startedAt = this.currentAction.object.startedAt;
                            element.endedAt = this.currentAction.object.endedAt;
                        }
                    }
                });
                return elements;
            },
            pagination() {
                return this.$store.state.campaign.filter.result.pagination;
            }
        },
        methods: {
            changePageIndex(pageIndex) {

            },
            changePageSize(pageSize) {

            },
            startCreating() {
                this.currentAction.name = 'create';
                this.currentAction.status = 'continuous';
                this.$router.push('/admin/campaigns/create') 
            },
        },
        async created() {
            await this.$store.dispatch('campaign/DO_FILTER')
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
        .item-row .col-date {
            width: 10%;
        }
</style>