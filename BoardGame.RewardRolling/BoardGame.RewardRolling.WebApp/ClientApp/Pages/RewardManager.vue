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

        <div>
            <popup-create-reward @creating-object-change="changeCreating"
                                 @success="createSuccessfully"
                                 @completed="createCompletely"
                                 :visible="currentAction.name=='create'">

            </popup-create-reward>
            <popup-update-reward @editing-object-change="changeUpdating"
                                 @success="updateSuccessfully"
                                 @completed="updateCompletely"
                                 :visible="currentAction.name=='update'"
                                 :object="updatingObject">

            </popup-update-reward>
            <popup-delete-reward :visible="currentAction.name=='delete'"
                                  :object="deletingObject"
                                  @success="deleteSuccessfully"
                                  @completed="deleteCompletely">

            </popup-delete-reward>
        </div>
    </div>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import pagination from '../Components/Shared/_Pagination.vue';
    import PopupCreateReward from '../Components/Reward/PopupCreate.vue';
    import PopupUpdateReward from '../Components/Reward/PopupEdit.vue';
    import PopupDeleteReward from '../Components/Reward/PopupDelete.vue';

    export default {
        name: 'reward-manager',
        components: {
            pagination,
            PopupCreateReward,
            PopupUpdateReward,
            PopupDeleteReward
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
                console.log('call visibleRewards')
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
                    if (this.currentAction.name == 'updating') {
                        if (this.currentAction.object.id == element.id) {
                            console.log('==')
                            element.actionStatus = 'updating';
                            element.name = this.currentAction.object.name;
                            element.cost = this.currentAction.object.cost;
                        }
                    }
                });
                return elements;
            },
            updatingObject: function () {
                if (this.currentAction.name != 'update') {
                    return { id: 0, name: '', cost: ''};
                }
                else {
                    let object = this.currentAction.object;
                    return object;
                }
            },
            deletingObject: function () {
                if (this.currentAction.name != 'delete')
                    return { id: 0, name: '', cost: ''};
                else {
                    let object = cloneDeep(this.currentAction.object);
                    return object;
                }
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
                this.currentAction.status = 'completed';
                this.deaction();
            },
            startUpdating(rewardId) {
                console.log(rewardId)
                this.currentAction.name = 'update';
                this.currentAction.status = 'continuous';
                let object = this.$store.state.reward.filter.result.rewards.filter(f => f.id == rewardId)[0];
                console.log(object)
                this.currentAction.object = cloneDeep(object);
            },
            changeUpdating(reward) {
                console.log(reward.name)
                this.$set(this.currentAction, 'object', reward);
            },
            updateSuccessfully() {
                this.currentAction.status = 'success';
            },
            updateCompletely() {
                this.currentAction.status = 'completed';
                this.deaction();
            },
            startDeleting(id) {
                this.currentAction.name = 'delete';
                this.currentAction.status = 'continuous';
                let object = this.$store.state.reward.filter.result.rewards.filter(f => f.id == id)[0];
                this.currentAction.object = cloneDeep(object);
            },
            deleteSuccessfully() {
                this.$set(this.currentAction, 'status', 'success');
            },
            deleteFailed() {

            },
            deleteCompletely() {
                this.deaction();
            },
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