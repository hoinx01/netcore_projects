<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Chi tiết đợt quà</h1></div>
        </div>
        <div id="main-content" class="router-view-content">
            
            <b-form class="create_campaign_form">
                <div>
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
                            <datetime format="DD/MM/YYYY" width="300px" v-model="campaign.displayedStartedAt"></datetime>
                        </b-form-group>
                        <b-form-group id="fieldset-horizontal"
                                      label-cols-sm="2"
                                      label-cols-lg="2"
                                      description="Ngày kết thúc"
                                      label="Ngày kết thúc"
                                      label-for="input-start-date">
                            <datetime format="DD/MM/YYYY" width="300px" v-model="campaign.displayedEndedAt"></datetime>
                        </b-form-group>

                    </div>
                    <b-form-group>
                        <div id="list_reward-container">
                            <div class="list_reward-container-header">
                                <div class="list_reward-container-title">Hiện có {{campaign.rewards.length}} đầu quà tặng</div>
                                <b-btn size="sm" @click="startSelectingReward">Sửa</b-btn>
                            </div>
                            <div class="item-row">
                                <div class="col-index">STT</div>
                                <div class="col-name">Tên</div>
                                <div class="col-rate">Tỉ lệ (%)</div>
                                <div class="col-control"></div>
                            </div>

                            <draggable v-model="campaign.rewards" group="people" @start="drag=true" @end="drag=false">
                                <div class="item-row"
                                     v-for="(item,index) in campaign.rewards"
                                     :key="index">
                                    <div class="col-index">{{index + 1}}</div>
                                    <div class="col-name">{{item.reward.name}}</div>
                                    <div class="col-rate">
                                        <input class="reward_rate-input"
                                               v-model="item.rate" />
                                    </div>
                                    <div class="col-control">
                                        <font-awesome-icon :icon="['fa', 'trash-alt']"
                                                           @click="removeReward(item.reward)" />
                                    </div>
                                </div>
                            </draggable>


                        </div>
                        <b-form-invalid-feedback :state="validateRewardRate">
                            {{validationMessages.invalidRewardRate}}
                        </b-form-invalid-feedback>
                    </b-form-group>
                    
                </div>
                
                <div id="wheel-container">
                    <div class="wheel" v-bind:style="wheelStyle">
                    </div>
                    <div>
                        <upload-file label="Upload vòng quay" fileGroup="lucky-wheel-parts" @success="changeMainRimImage"></upload-file>
                        <upload-file label="Upload tâm" fileGroup="lucky-wheel-parts" @success="changeCenterCircleImage"></upload-file>
                        <upload-file label="Upload giá kim" fileGroup="lucky-wheel-parts" @success="changePointerRackImage"></upload-file>
                        <upload-file label="Upload kim" fileGroup="lucky-wheel-parts" @success="changePointerImage"></upload-file>
                        <b-btn @click="play">Quay thử</b-btn>
                        
                    </div>
                </div>
            </b-form>
        </div>
        <div id="main-footer" class="router-view-footer">
            <b-btn @click="doUpdate">Đồng ý</b-btn>
        </div>

        <div id="popup-container">
            <popup-select-reward :visible="selectReward.doing"
                                 :currentRewardIds="currentRewardIds"
                                 @reward-selected="addReward"
                                 @reward-removed="removeReward"
                                 @completed="stopSelectingReward">

            </popup-select-reward>
        </div>
    </div>
</template>
<script>

    import campaignRepository from '../Repositories/CampaignRepository';
    import rewardRepository from '../Repositories/RewardRepository';
    import datetime from 'vuejs-datetimepicker';
    import PopupSelectReward from '../Components/Campaign/PopupSelectReward.vue';
    import UploadFile from '../Components/Shared/UploadFile.vue';
    import draggable from 'vuedraggable';
    import cloneDeep from 'lodash/cloneDeep'

    export default {
        name: 'campaign-detail',
        components: {
            datetime,
            PopupSelectReward,
            UploadFile,
            draggable
        },
        data() {
            var today = new Date();
            return {
                campaign: {
                    name: '',
                    startedAt: '',
                    endedAt: '',
                    displayedStartedAt: '',
                    displayedEndedAt: '',
                    wheel: {
                        url: '',
                        pointerUrl:''
                    },
                    rewards: [],
                    luckyWheel: {
                        mainRim: {
                            imageSrc:''
                        },
                        centerCircle: {
                            imageSrc: ''
                        },
                        pointer: {
                            imageSrc:''
                        },
                        pointerRack: {
                            imageSrc:''
                        }
                    }
                },
                selectReward: {
                    doing: false
                },
                wheelClass: {
                    wheel: true,
                    spin: false
                },

                wheelState: {
                    degree: 0
                },
                validationMessages: {
                    invalidRewardRate: 'Tổng tỉ lệ trúng giải không được vượt quá 100%'
                }
            }
        },
        computed: {
            currentRewardIds() {
                return this.campaign.rewards.map(m => m.rewardId);
            },
            wheelStyle() {
                return {
                    transform: 'rotate(' + this.wheelState.degree.toString() + 'deg)'
                }
                
            },
            validateRewardRate:{
                get() {
                    if (this.campaign.rewards.length == 0)
                        return true;
                    var totalRate = this.campaign.rewards.reduce((total, reward) => {
                        return total += parseInt(reward.rate);
                    }, 0);
                    console.log(totalRate)
                    return totalRate <= 100;
                }
            }
        },
        methods: {
            startSelectingReward() {
                this.selectReward.doing = true;
            },
            stopSelectingReward() {
                this.selectReward.doing = false;
            },
            addReward(reward) {
                var campaignReward = {
                    rewardId: reward.id,
                    reward: reward,
                    rate: 0,
                    ordinal: this.campaign.rewards.length + 1
                };
                this.campaign.rewards.push(campaignReward);
            },
            removeReward(reward) {
                this.campaign.rewards = this.campaign.rewards.filter(f => f.rewardId != reward.id);
            },
            play() {
                var degree = 1800;

                var noY = 0;
			
			    var c = 0;
                var n = 100;	
                var velocity = 100;
                var that = this;
                var interval = setInterval(function () {
				    c++;				
                    if (c === n) { 
					    clearInterval(interval);				
				    }						
                    if (c <= 10) {
                        
                    }
                    else {
                        if (velocity > 2)
                            velocity -= 2;
                    }
                    that.wheelState.degree += velocity;
					
                }, 100);

            },
            changeMainRimImage(img) {
                this.campaign.luckyWheel.mainRim.imageSrc = img.cdnAddress + img.path;
            },
            changeCenterCircleImage(img) {
                this.campaign.luckyWheel.centerCircle.imageSrc = img.cdnAddress + img.path;
            },
            changePointerRackImage(img) {
                this.campaign.luckyWheel.pointerRack.imageSrc = img.cdnAddress + img.path;
            },
            changePointerImage(img) {
                this.campaign.luckyWheel.pointer.imageSrc = img.cdnAddress + img.path;
            },
            async doUpdate() {
                try {
                    var model = cloneDeep(this.campaign);
                    model.startedAt = this.parseDate(model.displayedStartedAt);
                    model.endedAt = this.parseDate(model.displayedEndedAt);
                    await campaignRepository.update(model);
                    this.$router.push('/admin/campaigns') 
                }
                catch (exception) {
                    console.log(exception)
                    //this.$router.push('/admin/campaigns') 
                }
            },
            parseDate(text) {
                var parts = text.split('/');
                return new Date(parts[2], parts[1], parts[0]);
            },
            formatDateTime(datetime, format) {
                console.log(datetime)
                var year = datetime.getFullYear();
                console.log(year)
                var month = datetime.getMonth();
                var day = datetime.getDay();
                var formated = format.replace("YYYY", year.toString());
                formated = formated.replace("MM", month.toString());
                formated = formated.replace("DD", day.toString());
                return formated;

            }
        },
        async created() {
            let id = this.$route.params.id;
            console.log(id)
            try {
                var campaign = await campaignRepository.getById(id);
                console.log(campaign)
                var data = cloneDeep(campaign);
                data.displayedStartedAt = this.formatDateTime(new Date(campaign.startedAt), "DD/MM/YYYY");
                data.displayedEndedAt = this.formatDateTime(new Date(campaign.endedAt), "DD/MM/YYYY");
                this.campaign = data;
                
            }
            catch (exception) {
                console.log(exception)
            }
        }
    }
</script>
<style scoped>
    .main-content{
        max-width: 100%;
    }
    .form-group{
        width: 100%;
    }
    .list_reward-container-header{
        display:flex;

    }
    .list_reward-container-title{
        margin-right: 10px;
    }
    .item-row{
        display:flex
    }
    .col-index{
        width: 50px;
    }
    .col-name{
        width: 150px;
    }
    .col-rate{
        width: 100px;
    }
    .reward_rate-input{
        width:75px;
    }
    
    .create_campaign_form{
        display:flex;
    }
    #wheel-container{
        width:300px;
        height:300px;
        background-color:red;
    }
    .wheel{
        width:300px;
        height:300px;
        border-radius: 150px;
        background-color: yellow;
        display:flex;
        align-items:center;
        justify-content:center;
        background-image: url(http://static.ekipvn.com/Sites/6637/Data/images/2014/3/v%C3%B2ng%20quay.jpg);
        background-position: center;
        background-size: cover;
    }
    
    .wheel_pointer{
        width:100px;
        height:100px;
        border-radius: 50%;
        background-color: white
    }
    @-webkit-keyframes hh {
      0%, 100%{
        transform: rotate(0deg);
        -webkit-transform: rotate(0deg);
      }

      50%{
        transform: rotate(7deg);
        -webkit-transform: rotate(7deg);
      }
    }

    @keyframes hh {
       0%, 100%{
        transform: rotate(0deg);
        -webkit-transform: rotate(0deg);
      }

      50%{
        transform: rotate(7deg);
        -webkit-transform: rotate(7deg);
      }
    }

    .spin {
      -webkit-animation: hh 0.1s; /* Chrome, Safari, Opera */
        animation: hh 0.1s;
    }
</style>