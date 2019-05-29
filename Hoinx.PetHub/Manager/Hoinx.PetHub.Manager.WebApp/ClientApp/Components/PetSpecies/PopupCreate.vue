<template>
    <b-modal 
             id="popup-create-species" 
             title="Tạo mới loài" 
             size="lg" 
             ref="popupCreateSpecies" 
             :visible="visible"
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
                      v-on:click="addSpecies"
                      >
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import speciesRepository from '../../Repositories/SpeciesRepository' 

    export default {
        name: 'popup-create-species',
        props: {
            visible: {
                type: Boolean,
                required: true
            }
        },
        data() {
            return {
                species: {
                    name: '',
                    alias:''
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
            'species.name': function () {
                let creating = {
                    name: this.species.name
                }
                this.$emit('creating-object-change', creating);
            }
        },
        methods: {
            generateAlias() {
                let ascii = this.species.name.normalize('NFD')
                    .replace(/[\u0300-\u036f]/g, '')
                    .replace(/đ/g, 'd').replace(/Đ/g, 'D');
                ascii = ascii.toLowerCase();
                ascii = ascii.replace(/\s/g, '-');
                ascii = ascii.replace(/[-]{2,}/g, '-');
                return ascii;
            },
            async addSpecies() {
                let species = this.species;
                species.alias = this.alias;
                try {
                    var addedSpecies = await speciesRepository.add(species);
                    this.$emit('success', addedSpecies);
                }
                catch (error) {
                    this.$emit('failed', error);
                }
            },
            complete() {
                this.$emit('completed');
            }
        }
    }
</script>