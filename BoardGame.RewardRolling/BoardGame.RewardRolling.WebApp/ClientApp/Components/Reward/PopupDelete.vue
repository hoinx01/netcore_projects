<template>
    <b-modal :visible="visible"
             size="lg"
             ref="popupDeleteReward"
             @hide="complete">

        <div>
            Bạn chắc chắn muốn xóa {{object.name}}
        </div>
        <div slot="modal-footer" class="w-100">
            <b-button variant="primary"
                      size="sm"
                      class="float-right"
                      v-on:click="deleteReward">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import rewardRepository from '../../Repositories/RewardRepository';

    export default {
        name: 'PopupDeleteReward',
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
            async deleteReward() {
                try {
                    await rewardRepository.delete(this.object);
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