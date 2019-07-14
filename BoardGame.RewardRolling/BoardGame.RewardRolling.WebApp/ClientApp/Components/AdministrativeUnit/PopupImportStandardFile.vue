<template>
    <b-modal :visible="displayed"
             title="Nhập file đơn vị hành chính chuẩn"
             @hide="hide">
        <div>
            <input type="file" @change="chooseFile"/>
        </div>

        <div slot="modal-footer" class="w-100 control_group control_group-right">
            <b-button variant="primary"
                      size="sm"
                      v-on:click="doUpload">
                OK
            </b-button>
        </div>
    </b-modal>
</template>
<script>
    import administrativeUnitRepository from '../../Repositories/AdministrativeUnitRepository.js';

    export default {
        name: 'popup-import-standard-file',
        props: {
            displayed: {
                type: Boolean,
                required: true
            }
        },
        data() {
            return {
                choosenFile: null,
                importResult: {
                    totalRecords: 0,
                    successRecordCount: 0,
                    failedRecordCount: 0,
                    errors:[]
                }
            }
        },
        methods: {
            hide() {
                this.$emit("hide");
            },
            chooseFile(e) {
                var files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                var file = files[0];
                console.log(file)
                this.choosenFile = file;
            },
            async doUpload() {
                try {
                    var uploadResult = await administrativeUnitRepository.uploadStandardFile(this.choosenFile);
                    console.log(uploadResult);
                    this.$emit("success", uploadResult);
                }
                catch (exception) {

                }
                
            }
        }
    }
</script>
<style>
    .control_group{
        display: flex;
        flex-direction:row;
    }
    .control_group-right{
        justify-content:flex-end;
        align-items: center;
    }
</style>