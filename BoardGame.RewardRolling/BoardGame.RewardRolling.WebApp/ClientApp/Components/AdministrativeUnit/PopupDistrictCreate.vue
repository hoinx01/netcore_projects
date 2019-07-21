<template>
    <b-modal id="popup-create-species"
             title="Tạo mới đơn vị hành chính cấp huyện"
             size="lg"
             :visible="visible"
             @hide="complete">
        <b-form style="width:60%" :novalidate="true">
            <b-form-group id="fieldset-horizontal"
                          label-cols-sm="2"
                          label-cols-lg="2"
                          description="Tên"
                          label="Tên"
                          label-for="input-name">
                <b-form-input id="input-name"
                              v-model="district.name"
                              :state="validateName">
                </b-form-input>
                <b-form-invalid-feedback :state="validateName">
                    {{validationMessages.name.invalidLength}}
                </b-form-invalid-feedback>
            </b-form-group>
            <b-form-group id="fieldset-horizontal"
                          label-cols-sm="2"
                          label-cols-lg="2"
                          label="Cấp"
                          label-for="input-level">
                <b-form-select id="input-level" v-model="district.levelId" :options="districtLevelOptions"></b-form-select>
            </b-form-group>
            
        </b-form>
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      v-on:click="doCreate">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import districtRepository from '../../Repositories/DistrictRepository';

    export default {
        name: 'popup-district-create',
        props: {
            visible: {
                type: Boolean,
                required: true
            },
            cityId: {
                type: String,
                required: true
            }
        },
        data() {
            return {
                district: {
                    name: '',
                    levelId: 1
                },
                districtLevelOptions: [
                    {value:1, text: "Thành phố"},
                    {value:2, text: "Quận"},
                    {value:3, text: "Thị xã"},
                    {value:4, text: "Huyện"}
                ],
                validationMessages: {
                    name: {
                        invalidLength: 'Tên không được để trống',
                       
                    }
                }
            }
        },
        computed: {
            validateName() {
                return this.district.name.trim().length >= 1;
            },
        },
        methods: {
            complete() {

            },
            async doCreate() {
                try {
                    var requestModel = cloneDeep(this.district);
                    requestModel.cityId = this.cityId;
                    var addResult = await districtRepository.add(requestModel);
                    this.$emit('success', addResult);
                }
                catch (exception) {
                    this.$emit('failed');
                }
            },
            complete() {
                this.$emit('completed');
            }
        }
    }
</script>