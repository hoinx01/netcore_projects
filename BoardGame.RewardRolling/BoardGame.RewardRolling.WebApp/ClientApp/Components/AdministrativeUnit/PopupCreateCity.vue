<template>
    <b-modal id="popup-create-species"
             title="Tạo mới đơn vị hành chính cấp tỉnh"
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
                              v-model="city.name"
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
                <b-form-select id="input-level" v-model="city.levelId" :options="cityLevelOptions"></b-form-select>
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
    import cityRepository from '../../Repositories/CityRepository';
    export default {
        name: 'popup-create-city',
        props: {
            visible: {
                type: Boolean,
                required: true
            }
        },
        data() {
            return {
                city: {
                    name: '',
                    levelId: 1
                },
                cityLevelOptions: [
                    {value:1, text: "Thành phố trực thuộc trung ương"},
                    {value:2, text: "Tỉnh"}
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
                return this.city.name.trim().length >= 1;
            },
        },
        methods: {
            complete() {

            },
            async doCreate() {
                try {
                    var addCityResult = await cityRepository.add(this.city);
                    this.$emit('success', addCityResult);
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