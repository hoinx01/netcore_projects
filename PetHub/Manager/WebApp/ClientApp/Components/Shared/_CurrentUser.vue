<template>
    <div class="user-container">
        <div id="avatar-wrapper"></div>
        <div id="name-wrapper">{{user.name}}</div>
        <div>
            <b-dropdown id="user-actions-wrapper" dropup text="" variant="primary" class="m-2">
                <b-dropdown-item href="/admin/my_account">Hồ sơ cá nhân</b-dropdown-item>
                <b-dropdown-item href="#" @click="logout">Thoát</b-dropdown-item>
            </b-dropdown>
        </div>
    </div>
</template>
<script>
    export default {
        name: "User",
        computed: {
            user() {
                let account = this.$store.state.myAccount.currentAccount;
                if (account)
                    return {
                        name: account.name
                    }
                else
                    return {
                        name: ''
                    }
            }
        },
        methods: {
            logout() {
                this.$store.dispatch('myAccount/logout');
                //this.$router.push('/')
                window.location.href = '/'
            }
        }
        
    }
</script>
<style scoped>
    .user-container{
        display:flex;
        flex-flow:row;
    }
</style>