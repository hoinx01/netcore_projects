<template>
    <b-modal :visible="visible"
             size="lg"
             @hide="complete">

        <div>
            Bạn chắc chắn muốn xóa {{object.name}}
        </div>
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      v-on:click="doDelete">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import userRepository from '../../Repositories/UserRepository';
    import cloneDeep from 'lodash/cloneDeep';

    export default {
        name: 'user-popup-delete',
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
            async doDelete() {
                try {
                    await userRepository.delete(this.object);
                    this.$emit("success");
                }
                catch (error) {
                    this.$emit("failed");
                }
            },
            complete() {
                this.$emit('completed');
            }
        }
    }
</script>