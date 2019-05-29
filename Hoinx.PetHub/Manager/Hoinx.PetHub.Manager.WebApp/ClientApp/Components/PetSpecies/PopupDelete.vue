<template>
    <b-modal :visible="visible"
             size="lg"
             ref="popupEditSpecies"
             @hide="complete">

        <div>
            Bạn chắc chắn muốn xóa {{object.name}}
        </div>
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      v-on:click="deleteSpecies">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import speciesRepository from '../../Repositories/SpeciesRepository.js';

    export default {
        name: 'PopupDeleteSpecies',
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
        methods: {
            async deleteSpecies() {
                try {
                    await speciesRepository.delete(this.object);
                    this.$emit("success");
                }
                catch (error) {

                }
            },
            complete() {
                this.$emit('completed');
            }
        }
    }
</script>