<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Đổi mật khẩu</h1></div>
        </div>
        <div id="main-content" class="router-view-content">
            <div class="form-group">
                <div class="label-input"><label>Mật khẩu hiện tại</label></div>
                <div class="input-group">
                    <input type="password" v-model="changePasswordModel.currentPassword" class="textbox" />
                </div>
            </div>
            <div class="form-group">
                <div class="label-input"><label>Mật khẩu mới</label></div>
                <div class="input-group">
                    <input type="password" v-model="changePasswordModel.newPassword" class="textbox" />
                </div>
            </div>
            <div class="form-group">
                <div class="label-input"><label>Xác nhận mật khẩu mới</label></div>
                <div class="input-group">
                    <input type="password" v-model="changePasswordModel.confirmNewPassword" class="textbox" />
                </div>
            </div>
        </div>
        <div id="main-footer" class="router-view-footer">
            <b-btn @click="doUpdate">Đồng ý</b-btn>
        </div>
    </div>
</template>
<script>
    export default {
        name: "ChangePassword",
        data() {
            return {
                changePasswordModel: {
                    currentPassword: '',
                    newPassword: '',
                    confirmNewPassword:''
                }
            }
        }
    }
</script>
<style scoped>
    .form-group {
        display: flex;
    }
    .label-input {
        flex-basis: 20%;
        min-width: 125px;
    }

    .input-group {
        flex-grow: 1;
    }

        .input-group .textbox {
            width: 250px;
        }
</style>