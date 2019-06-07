<template>
    <div id="left-menu-container">
        <div id="menu-item-container">
            <ul>
                <li>
                    <router-link to="/admin/rolling_codes">Quản lý mã quà tặng</router-link>
                </li>
                <li>
                    <router-link to="/admin/rewards">Quản lý quà tặng</router-link>
                </li>
                <li>
                    <router-link to="/admin/accounts">Quản lý giống PET</router-link>
                </li>
                <li>
                    <router-link to="/admin/customers">Quản lý khách hàng</router-link>
                </li>
            </ul>
        </div>
        <user v-if="authenticated" id="user-box"></user>
    </div>
</template>

<script>
    import User from './_CurrentUser.vue';
    export default {
        name: "Menu",
        components: {User},
        data() {
            return {

            }
        },
        computed: {
            authenticated() {
                //return this.$store.state.myAccount.currentAccount != null;
                return false;
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
        background-color: rgba(0,0,0,0.4)
    }
    #user-box{
        flex-basis:10%;
    }
    #menu-item-container{
        flex-grow:1
    }
    ul{
        list-style-type: none;
    }
</style>