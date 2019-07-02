<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Thông tin cá nhân</h1></div>
        </div>
        <div id="main-content" class="router-view-content">
            <div class="form-group">
                <div class="label-input"><label>Tên đăng nhập:</label></div>
                <div class="input-group">
                    <input type="text" v-model="me.userName" class="textbox" disabled />
                </div>
            </div>
            <div class="form-group">
                <div class="label-input"><label>Họ và tên</label></div>
                <div class="input-group">
                    <input type="text" v-model="me.name" class="textbox" />
                </div>
            </div>

        </div>
        <div id="main-footer" class="router-view-footer">
            <b-btn @click="doUpdate">Đồng ý</b-btn>
        </div>
    </div>
</template>
<script>
    import userRepository from '../Repositories/UserRepository';
    export default {
        name: "MyAccount",
        data() {
            let user = this.$store.state.identity.currentUser;
            console.log(user)
            if (user)
                return {
                    me: {
                        name: user.name,
                        userName: user.userName,
                        password:'******'
                    }
                }
            else
                return {
                    me: {
                        name: '',
                        userName: '',
                        password:'******'
                    }
                }
        },
        async created() {
            var user = await userRepository.getCurrentUser();
            this.me.name = user.name;
            this.me.userName = user.userName;
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