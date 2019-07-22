<template>
    <div id="left-menu-container">
        <div id="logo-box">
            <h3>Boardgame Admin</h3>
        </div>
        <div id="menu-item-container">
            <ul>
                <li>
                    <router-link to="/admin/rolling_codes">Quản lý mã quà tặng</router-link>
                </li>
                <li>
                    <router-link to="/admin/rewards">Quản lý quà tặng</router-link>
                </li>
                <li>
                    <router-link to="/admin/campaigns">Quản lý đợt tặng quà</router-link>
                </li>
                <li>
                    <router-link to="/admin/users">Quản lý người dùng</router-link>
                </li>
                <li>
                    <router-link to="/admin/customers">Quản lý khách hàng</router-link>
                </li>
                <li>
                    <router-link to="/admin/administrative_units">Quản lý đơn vị hành chính</router-link>
                </li>
            </ul>
        </div>
        <current-user :class="{user_box: true, hidden: !authenticated}"></current-user>
    </div>
</template>

<script>
    import CurrentUser from './_CurrentUser.vue';
    export default {
        name: "Menu",
        components: {CurrentUser},
        data() {
            return {

            }
        },
        computed: {
            authenticated() {
                return this.$store.state.identity.currentUser.authenticated;
            }
        }
    }
</script>
<style scoped>
    #left-menu-container {
        display: flex;
        flex-direction: column;
        max-height: 100vh;
        height: 100vh;
        background-color: rgba(0,0,0,0.2)
    }
    .user_box{
        flex-basis:10%;
    }
    #menu-item-container{
        margin-top: 20px;
        margin-bottom: 20px;
        flex-grow:1
    }
    #menu-item-container ul{
        list-style-type: none;
        margin-left: 15px;
    }
    #menu-item-container ul li{
        margin-bottom:15px;
        font-weight: bold;
    }
</style>