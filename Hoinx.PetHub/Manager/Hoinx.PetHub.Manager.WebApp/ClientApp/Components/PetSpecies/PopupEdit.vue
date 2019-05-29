<template>
    <b-modal 
             :visible="visible"
             id="popup-edit-species" 
             :title="'Sửa ' + object.name" 
             size="lg" 
             ref="popupEditSpecies"
             @hide="complete">
        <b-form style="width:60%" :novalidate="true">
            <b-form-group id="fieldset-horizontal"
                          label-cols-sm="2"
                          label-cols-lg="2"
                          description="Tên loài (ví dụ: chó, mèo, chim...)"
                          label="Tên loài"
                          label-for="input-name">
                <b-form-input id="input-name"
                              v-model="species.name"
                              :state="validateSpeciesName">
                </b-form-input>
                <b-form-invalid-feedback :state="validateSpeciesName">
                    {{validationMessages.name.invalidLength}}
                </b-form-invalid-feedback>
            </b-form-group>

            <b-form-group id="fieldset-horizontal"
                          label-cols-sm="2"
                          label-cols-lg="2"
                          label="Alias"
                          label-for="input-alias">
                <b-form-input id="input-alias" v-model="alias"></b-form-input>
            </b-form-group>
        </b-form>
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      @click="editSpecies">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import speciesRepository from '../../Repositories/SpeciesRepository'
    import cloneDeep from 'lodash/cloneDeep';

    export default {
        name: 'popup-edit-species',
        props: {
            object: {
                type: Object,
                required: true
            },
            visible: {
                type: Boolean,
                required: true
            }
        },
        data() {
            return {
                species: {
                    id: this.object.id,
                    name: this.object.name
                },
                validationMessages: {
                    name: {
                        invalidLength: 'Tên loài không được để trống'
                    }
                }
            }
        },
        computed: {
            alias() {
                return this.generateAlias();
            },
            validateSpeciesName() {
                return this.species.name.trim().length >= 1;
            }
        },
        watch: {
            //'speciesId': async function (newId, oldId) {
            //    await this.getEditingSpecies();
            //},
            'object': function () {
                this.species = cloneDeep(this.object);
            },
            'species.name': function () {
                let editing = {
                    id: this.species.id,
                    name: this.species.name
                }
                this.$emit('object-changed', editing);
            }
        },
        methods: {
            async getEditingSpecies() {
                let species = await speciesRepository.getById(this.speciesId);
                this.$set(this.species, 'id', species.id);
                this.$set(this.species, 'name', species.name);
            },
            generateAlias() {
                let ascii = this.species.name.normalize('NFD')
                    .replace(/[\u0300-\u036f]/g, '')
                    .replace(/đ/g, 'd').replace(/Đ/g, 'D');
                ascii = ascii.toLowerCase();
                ascii = ascii.replace(/\s/g, '-');
                ascii = ascii.replace(/[-]{2,}/g, '-');
                return ascii;
            },
            async editSpecies(event) {
                
                let species = this.species;
                species.alias = this.alias;
                species.id = this.object.id;
                try {
                    console.log('click edit')
                    var updatedSpecies = await speciesRepository.update(species);
                    this.$emit("success", updatedSpecies);
                }
                catch (error) {
                    this.$emit("failed", updatedSpecies);
                }
            },
            complete() {
                this.$emit('completed');
            }
        },
        async created() {

        }
    }
</script>