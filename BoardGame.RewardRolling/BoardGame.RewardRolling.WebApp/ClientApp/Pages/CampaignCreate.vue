<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách đợt tặng quà</h1></div>
        </div>
        <div id="main-content" class="router-view-content">
            <b-form>
                <div id="basic_data-container">
                    <b-form-group id="fieldset-horizontal"
                                  label-cols-sm="2"
                                  label-cols-lg="2"
                                  description="Tên"
                                  label="Tên"
                                  label-for="input-name">
                        <b-form-input id="input-name"
                                      v-model="campaign.name">
                        </b-form-input>
                    </b-form-group>
                    <b-form-group id="fieldset-horizontal"
                                  label-cols-sm="2"
                                  label-cols-lg="2"
                                  description="Ngày bắt đầu"
                                  label="Ngày bắt đầu"
                                  label-for="input-start-date">
                        <datetime format="DD/MM/YYYY" width="300px" v-model="campaign.startedAt"></datetime>
                    </b-form-group>
                    <b-form-group id="fieldset-horizontal"
                                  label-cols-sm="2"
                                  label-cols-lg="2"
                                  description="Ngày kết thúc"
                                  label="Ngày kết thúc"
                                  label-for="input-start-date">
                        <datetime format="DD/MM/YYYY" width="300px" v-model="campaign.endedAt"></datetime>
                    </b-form-group>

                </div>
                <div id="list_reward-container">
                    <div>Hiện có {{campaign.rewards.length}} đầu quà tặng</div>
                    <b-btn @click="startSelectingReward">Thêm</b-btn>
                    <div class="item-row"
                         v-for="(item,index) in campaign.rewards"
                         :key="index">
                        <div class="col-index">{{index + 1}}</div>
                        <div class="col-name">{{item.reward.name}}</div>
                        <div class="col-name">{{item.rate}}%</div>
                    </div>
                </div>
                <div id="wheel-container"></div>
            </b-form>
        </div>
        <div id="main-footer" class="router-view-footer">
            
        </div>

        <div id="popup-container">
            <popup-select-reward :visible="selectReward.doing">

            </popup-select-reward>
        </div>
    </div>
</template>
<script>
    import datetime from 'vuejs-datetimepicker';
    import PopupSelectReward from '../Components/Campaign/PopupSelectReward.vue';

    export default {
        name: 'campaign-create',
        components: {
            datetime,
            PopupSelectReward
        },
        data() {
            return {
                campaign: {
                    name: '',
                    startedAt: '',
                    endedAt: '',
                    rewards:[]
                },
                selectReward: {
                    doing: false
                }
            }
        },
        methods: {
            startSelectingReward() {
                this.selectReward.doing = true;
            }
        }
    }
</script>