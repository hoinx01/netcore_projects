<template>
    <b-modal id="popup-create-species"
             title="Tạo mới đơn vị hành chính cấp xã"
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
                              v-model="commune.name"
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
                <b-form-select id="input-level" v-model="commune.levelId" :options="communeLevelOptions"></b-form-select>
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
    import communeRepository from '../../Repositories/CommuneRepository';

    export default {
        name: 'commune-popup-create',
        props: {
            visible: {
                type: Boolean,
                required: true
            },
            cityId: {
                type: String,
                required: true
            },
            districtId:{
                type: String,
                required: true
            }
        },
        data() {
            return {
                commune: {
                    name: '',
                    levelId: 1
                },
                communeLevelOptions: [
                    {value:1, text: "Phường"},
                    {value:2, text: "Thị trấn"},
                    {value:3, text: "Xã"}
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
                return this.commune.name.trim().length >= 1;
            },
        },
        methods: {
            complete() {

            },
            async doCreate() {
                try {
                    var requestModel = cloneDeep(this.commune);
                    requestModel.cityId = this.cityId;
                    requestModel.districtId = this.districtId;
                    var addResult = await communeRepository.add(requestModel);
                    this.$emit('success', addResult);
                }
                catch (exception) {
                    console.log(exception);
                    this.$emit('failed');
                }
            },
            complete() {
                this.$emit('completed');
            }
        }
    }
</script>