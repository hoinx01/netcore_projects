<template>
    <b-modal id="popup-create-species"
             title="Tạo mới quà tặng"
             size="lg"
             ref="popupEditReward"
             :visible="visible"
             @hide="complete">
        <b-form style="width:60%" :novalidate="true">
            <b-form-group id="fieldset-horizontal"
                          label-cols-sm="2"
                          label-cols-lg="2"
                          description="Tên quà tặng"
                          label="Tên quà tặng"
                          label-for="input-name">
                <b-form-input id="input-name"
                              v-model="reward.name"
                              :state="validateName">
                </b-form-input>
                <b-form-invalid-feedback :state="validateName">
                    {{validationMessages.name.invalidLength}}
                </b-form-invalid-feedback>
            </b-form-group>

            <b-form-group id="fieldset-horizontal"
                          label-cols-sm="2"
                          label-cols-lg="2"
                          description="Giá trị"
                          label="Giá trị"
                          label-for="input-cost">
                <b-form-input id="input-cost"
                              v-model="reward.cost"
                              :state="validateCost">
                </b-form-input>


                <b-form-invalid-feedback :state="validateCost">
                    {{validationMessages.cost.invalidRange}}
                </b-form-invalid-feedback>
            </b-form-group>
            <upload-file label="Chọn ảnh" fileGroup="reward-image" @success="changeImage"></upload-file>
        </b-form>
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      v-on:click="updateReward">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import rewardRepository from '../../Repositories/RewardRepository';
    import UploadFile from '../../Components/Shared/UploadFile.vue';

    export default {
        name: 'popup-update-reward',
        components: {
            UploadFile
        },
        props: {
            visible: {
                type: Boolean,
                required: true
            },
            object: {
                type: Object,
                required: true
            }
        },
        data() {
            return {
                reward: cloneDeep(this.object),
                validationMessages: {
                    name: {
                        invalidLength: 'Tên không được để trống',

                    },
                    cost: {
                        invalidRange: 'Giá trị quà tặng không được âm'
                    }
                }
            }
        },
        computed: {
            validateName() {
                return this.reward.name.trim().length >= 1;
            },
            validateCost() {
                return this.reward.cost >= 0;
            }
        },
        watch: {
            'object': function () {
                this.reward = cloneDeep(this.object);
            },
            'reward.name': function () {
                let object = cloneDeep(this.reward);
                this.$emit('editing-object-change', object);
            },
            'reward.cost': function () {
                let object = cloneDeep(this.reward);
                this.$emit('editing-object-change', object);
            }
        },
        methods: {
            async updateReward() {
                try {
                    var updateResult = await rewardRepository.update(this.reward);
                    this.$emit('success', updateResult);
                }
                catch (error) {
                    console.log(error)
                    this.$emit('failed', error);
                }
            },
            changeImage(image) {
                this.reward.imageSrc = image.path;
            },
            complete() {
                this.$emit('completed');
            }
        }
    }
</script>