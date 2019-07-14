<template>
    <div id="root">
        <div id="menu">
            <Menu/>
        </div>
        <div id="router-view-wrapper">
            <router-view></router-view>
        </div>
    </div>
</template>
<script>

    import Menu from "./Components/Shared/_LeftMenu.vue";

    export default {
        data() {
            return {
                message: 'Message from App.vue',
            };
        },
        components: {Menu}
    };
</script>
<style scoped>
    #root{
        display:flex;
        flex-direction:row;
    }
    #menu{
        width:250px; 
        min-width:250px;
    }
    #router-view-wrapper{
        flex-grow:1; 
        flex-shrink:1;
        max-height:100vh;
        height:100vh;
    }
</style>
<style>
    .router-view-column-flow{
        min-height:100%;
        max-height: 100%;
        display:flex;
        flex-direction: column;
        padding-left: 20px;
    }
        .router-view-column-flow .router-view-header {
            flex-basis: 75px;
            padding-right:25px;
            display:flex;
            align-items:center;
            justify-content:space-between;
        }
        .router-view-column-flow .router-view-footer {
            flex-basis: 75px;
            padding-right:25px;
            display:flex;
            align-items:center;
            justify-content:flex-end;

        }
        .router-view-column-flow .router-view-content {
            flex-grow:1;
            overflow-y:scroll;
        }
    .list-pagination{
        display:flex;
    }

</style>