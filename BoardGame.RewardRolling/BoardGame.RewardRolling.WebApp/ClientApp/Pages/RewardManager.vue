<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách quà tặng</h1></div>
            <div>
                <b-btn @click="startCreating">Thêm</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Tên</div>
                <div class="col-name">Giá</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(item,index) in visibleRewards"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name">{{item.name}}</div>
                <div class="col-name">{{item.cost}}</div>
                <div class="col-control">
                    <font-awesome-icon :icon="['fa', 'trash-alt']"
                                       @click="startDeleting(item.id)" />
                    <font-awesome-icon :icon="['fa', 'pencil-alt']"
                                       @click="startEditing(item.id)" />
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
            <popup-create-reward
                                 @creating-object-change="changeCreating"
                                 @success="createSuccessfully"
                                 @completed="createCompletely"
                                 :visible="currentAction.name=='create'">

            </popup-create-reward>
        </div>
    </div>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import pagination from '../Components/Shared/_Pagination.vue';
    import PopupCreateReward from '../Components/Reward/PopupCreate.vue';

    export default {
        name: 'reward-manager',
        components: {
            pagination,
            PopupCreateReward
        },
        data() {
            return {
                currentAction: {
                    name: null,
                    status:'',
                    object: null
                }
            }
        },
        computed: {
            visibleRewards: function () {
                let elements = cloneDeep(this.$store.state.reward.filter.result.rewards);
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
                    if (this.currentAction.name == 'edit') {
                        if (this.currentAction.object.id == element.id) {
                            element.actionStatus = 'editing';
                            element.name = this.currentAction.object.name;
                            element.alias = this.currentAction.object.alias;
                        }
                    }
                });
                return elements;
            },
            pagination() {
                return this.$store.state.reward.filter.result.pagination;
            }
        },
        watch: {
            'currentAction.status': function (newStatus, oldStatus) {
                console.log(oldStatus + ' -> ' + newStatus)
                if (newStatus == 'success') {
                    this.deaction();
                    this.doFilter();
                }
                if (newStatus == 'completed') {
                    this.deaction();
                }   
            }
        },
        methods: {
            changePageIndex(pageIndex) {

            },
            changePageSize(pageSize) {

            },
            deaction() {
                this.currentAction.name = null;
                this.currentAction.status = '';
                this.currentAction.object = null;
            },
            doFilter() {
                this.$store.dispatch('reward/DO_FILTER');
            },
            startCreating() {
                this.currentAction.name = 'create';
                this.currentAction.status = 'continuous';
            },
            changeCreating(reward) {
                this.currentAction.object = reward;
            },
            createSuccessfully() {
                this.currentAction.status = 'success';
            },
            createCompletely() {

            },
            startEditing(rewardId) {

            },
            startDeleting(rewardId) {

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