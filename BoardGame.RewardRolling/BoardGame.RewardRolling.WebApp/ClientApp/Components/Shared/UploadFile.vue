<template>
    <div class="form-group">
        <div class="label-wrapper"><label>{{label}}</label></div>
        <div><input type="file" @change="doUpload" /></div>
        
    </div>
</template>
<script>
    import fileStorage from '../../Repositories/FileStorage';

    export default {
        name: 'upload-file',
        props: {
            label: {
                type: String,
                required: true
            },
            fileGroup: {
                type: String,
                required: true
            }
        },
        methods: {
            async doUpload(e) {
                let files = e.target.files || e.dataTransfer.files;
                if (!files.length)
                    return;
                var file = files[0];
                var result = await fileStorage.upload(file, this.fileGroup, file.name);
                console.log(result);
            },
        }
    }
</script>
<style scoped>
    .form-group{
        display:flex;
        overflow-x: hidden;
    }
    .label-wrapper{
        min-width: 120px;
    }
</style>
