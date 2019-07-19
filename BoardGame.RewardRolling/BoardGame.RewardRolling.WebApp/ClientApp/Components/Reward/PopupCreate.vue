<template>
    <b-modal id="popup-create-species"
             title="Tạo mới quà tặng"
             size="lg"
             ref="popupCreateReward"
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
                      v-on:click="addReward">
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
        name: 'popup-create-reward',
        components: {
            UploadFile
        },
        props: {
            visible: {
                type: Boolean,
                required: true
            }
        },
        data() {
            return {
                reward: {
                    name: '',
                    cost:''
                },
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
            'reward.name': function () {
                let creatingObject = cloneDeep(this.reward);
                this.$emit('creating-object-change', creatingObject);
            },
            'reward.cost': function () {
                let creatingObject = cloneDeep(this.reward);
                this.$emit('creating-object-change', creatingObject);
            }
        },
        methods: {
            resetForm() {
                this.reward = {
                    name: '',
                    cost: ''
                }
            },
            async addReward() {
                try {
                    var addedObjects = await rewardRepository.add(this.reward);
                    this.resetForm();
                    this.$emit('success', addedObjects);
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