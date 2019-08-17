<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách người dùng</h1></div>
            <div>
                <b-btn @click="startCreating">Thêm</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Tên</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(item,index) in users"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name">{{item.name}}</div>
                <div :class="{'col-control':true, hovered:hovered}" @mouseover="hover" @mouseout="unhover">
                    <font-awesome-icon :icon="['fa', 'trash-alt']"
                                       @click="deleteUserStart(item.id)" />
                </div>
            </b-list-group-item>

        </b-list-group>
        <div id="main-footer" class="router-view-footer">
            
        </div>

        <div id="popup-container">
            <user-popup-delete 
                               :visible="currentAction.name=='deleteUser'"
                               :object="currentAction.object"
                               @success="deleteUserSuccessfully"
                               @completed="deleteUserCompletely"
                               v-if="currentAction.name=='deleteUser'">

            </user-popup-delete>
        </div>
    </div>
</template>
<script>
    import { mapActions } from 'vuex';
    import userRepository from '../Repositories/UserRepository.js';
    import UserPopupDelete from '../Components/User/UserPopupDelete.vue';
    import cloneDeep from 'lodash/cloneDeep';

    export default {
        name: 'user-manager',
        components: {
            UserPopupDelete
        },
        data() {
            return {
                users:[],
                currentAction: {
                    name: '', 
                    object: null
                }
            }
        },
        computed: {
            hovered() {
                return this.$store.state.root.hovered;
            }
        },
        methods: {
            ...mapActions({
                hover: 'root/hover',
                unhover: 'root/unhover'
            }),
            deaction() {
                this.currentAction.name = null;
                this.currentAction.object = null;
            },
            deleteUserStart(userId) {
                this.currentAction.name = 'deleteUser';
                let object = this.users.filter(f => f.id == userId)[0];
                console.log(object);
                this.currentAction.object = cloneDeep(object);
            },
            deleteUserSuccessfully() {
                let index = -1;
                for (let i = 0; i < this.users.length; i++) {
                    if (this.users[i].id == this.currentAction.object.id) {
                        index = i;
                        break;
                    }
                }
                this.users.splice(index, 1);
                this.deaction();
            },
            deleteUserCompletely() {
                this.deaction();
            },
            startCreating() {
                this.currentAction.name = 'create';
                this.currentAction.status = 'continuous';
                this.$router.push('/admin/users/create') 
            },
        },
        async created() {
            try {
                var users = await userRepository.filter();
                this.users = users;
            }
            catch (exception) {
                console.log(exception);
            }
        }
    }
</script>

<style scoped>
    .item-row {
        display: flex;
    }

        .item-row .col-index {
            width: 50px;
        }

        .item-row .col-name {
            width: 30%;
        }
</style>