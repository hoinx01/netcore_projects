<template>
    <div class="user-container">
        <div id="avatar-wrapper"></div>
        <div id="name-wrapper">{{user.name}}</div>
        <div>
            <b-dropdown id="user-actions-wrapper" dropup text="" variant="primary" class="m-2">
                <b-dropdown-item href="/admin/me">Hồ sơ cá nhân</b-dropdown-item>
                <b-dropdown-item href="/admin/change_password">Đổi mật khẩu</b-dropdown-item>
                <b-dropdown-item href="#" @click="logout">Thoát</b-dropdown-item>
            </b-dropdown>
        </div>
    </div>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import userRespository from '../../Repositories/UserRepository';

    export default {
        name: "current-user",
        computed: {
            user() {
                let currentUser = this.$store.state.identity.currentUser;
                return cloneDeep(currentUser);
            }
        },
        methods: {
            logout() {
                //this.$store.dispatch('myAccount/logout');
                //this.$router.push('/')
                localStorage.clear();
                window.location.href = '/admin'
            }
        },
        async created() {
            var currentUser = await userRespository.getCurrentUser();
            this.$store.dispatch('identity/SET_CURRENT_USER', currentUser);
        }
        
    }
</script>
<style scoped>
    .user-container{
        display:flex;
        flex-flow:row;
        justify-content:space-around;
    }
</style>