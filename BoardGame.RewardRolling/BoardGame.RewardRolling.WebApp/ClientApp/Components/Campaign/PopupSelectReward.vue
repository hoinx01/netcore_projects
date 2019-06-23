<template>
    <b-modal id="popup-create-species"
             title="Chọn qua tặng"
             size="lg"
             ref="popupSelectReward"
             :visible="visible"
             @hide="complete">
        <div>
            <b-list-group class="router-view-content">
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
                        <font-awesome-icon 
                                           :icon="['fa', 'plus']"
                                           @click="selectReward(item)"
                                           v-if="!rewardExists(item.id)"/>
                        <font-awesome-icon 
                                           :icon="['fa', 'trash-alt']" 
                                           @click="removeReward(item)"
                                           v-if="rewardExists(item.id)"/>

                    </div>
                </b-list-group-item>

            </b-list-group>
            <div class="router-view-footer">
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
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      v-on:click="">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import pagination from '../../Components/Shared/_Pagination.vue';
    import rewardRepository from '../../Repositories/RewardRepository'

    export default {
        name: 'popup-select-reward',
        components: {
            pagination
        },
        props: {
            visible: {
                type: Boolean,
                required: true
            },
            currentRewardIds: {
                type: Array,
                required: true
            }
        },
        data() {
            return {
                filter: {
                    request: {
                        page: 1,
                        limit: 20
                    },
                    result: {
                        rewards: [],
                        pagination: {
                            count: 0,
                            page: 1,
                            limit: 20
                        }
                    }
                }
            }
        },
        watch: {
            'visible': async function (newValue, oldValue) {
                console.log(oldValue);
                console.log(newValue)
                if (newValue) {
                    await this.doFilter();
                }
            }
        },
        computed: {
            visibleRewards: function () {
                let elements = cloneDeep(this.filter.result.rewards);
                return elements;
            },
            pagination() {
                return this.filter.result.pagination;
            }
        },
        methods: {
            changePageIndex(pageIndex) {

            },
            changePageSize(pageSize) {

            },
            rewardExists(id) {
                return this.currentRewardIds.includes(id);
            },
            selectReward(reward) {
                this.$emit('reward-selected', reward);
            },
            removeReward(reward) {
                this.$emit('reward-removed', reward);
            },
            async doFilter() {
                var filterResult = await rewardRepository.filter(this.filter.request);
                console.log(filterResult);
                this.filter.result = filterResult;
            },
            complete() {
                this.$emit('completed');
            }
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