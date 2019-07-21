<template>
    <b-list-group id="main-content" class="router-view-content">
        <b-list-group-item class="item-row item-row-first">
            <div class="col-index">STT</div>
            <div class="col-id">Mã</div>
            <div class="col-name">Tên</div>
            <div class="col-level">Cấp</div>
            <div :class="{'col-control':true, hovered:hovered}"
                 @mouseover="hover"
                 @mouseout="unhover">
                <font-awesome-icon :icon="['fa', 'plus']" @click="createCommuneStart" />
            </div>
        </b-list-group-item>
        <b-list-group-item class="item-row"
                           v-for="(item, index) in administrativeUnits.communes"
                           :key="item.id">
            <div class="col-index">{{index + 1}}</div>
            <div class="col-id">{{item.id}}</div>
            <div class="col-name">{{item.name}}</div>
            <div class="col-level">{{item.level.name}}</div>
            <div class="col-control">
                <font-awesome-icon 
                                   :icon="['fa', 'trash-alt']" 
                                   @click="deleteCommuneStart(item.id)"/>
                <font-awesome-icon 
                                   :icon="['fa', 'pencil-alt']" 
                                   @click="updateCommuneStart(item.id)"/>
            </div>
        </b-list-group-item>
        <div>
            <commune-popup-create @success="createCommuneSuccessfully"
                                  @completed="createCommuneCompletely"
                                  v-if="currentAction.name=='createCommune'"
                                  :visible="currentAction.name=='createCommune'"
                                  :cityId="cityId"
                                  :districtId="districtId">
            </commune-popup-create>
            <commune-popup-edit @success="updateCommuneSuccessfully"
                                @completed="updateCommuneCompletely"
                                v-if="currentAction.name=='updateCommune'"
                                :visible="currentAction.name=='updateCommune'"
                                :object="currentAction.object">
            </commune-popup-edit>
            <commune-popup-delete @success="deleteCommuneSuccessfully"
                                   @completed="deleteCommuneCompletely"
                                   v-if="currentAction.name=='deleteCommune'"
                                   :visible="currentAction.name=='deleteCommune'"
                                   :object="currentAction.object">
            </commune-popup-delete>
        </div>
        
    </b-list-group>
</template>
<script>
    import { mapActions } from 'vuex';
    import cloneDeep from 'lodash/cloneDeep';
    import communeRepository from '../../Repositories/CommuneRepository';
    import CommunePopupCreate from './CommunePopupCreate.vue';
    import CommunePopupEdit from './CommunePopupEdit.vue';
    import CommunePopupDelete from './CommunePopupDelete.vue';

    export default {
        name: 'list-commune',
        components: {
            CommunePopupCreate,
            CommunePopupEdit,
            CommunePopupDelete
        },
        props: {
            cityId: {
                type: String,
                required: true
            },
            districtId: {
                type: String,
                required: true
            }
        },
        data() {

            return {
                administrativeUnits: {
                    communes: []
                },
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
            createCommuneStart() {
                this.currentAction.name = 'createCommune'
            },
            createCommuneSuccessfully(commune) {
                this.administrativeUnits.communes.push(commune);
                this.deaction();
            },
            createCommuneCompletely() {
                this.deaction();
            },
            updateCommuneStart(communeId) {
                let object = this.administrativeUnits.communes.filter(f => f.id == communeId)[0];
                this.currentAction.object = cloneDeep(object);
                this.currentAction.name = 'updateCommune';
            },
            updateCommuneSuccessfully(commune) {
                for (let i = 0; i < this.administrativeUnits.communes.length; i++) {
                    if (this.administrativeUnits.communes[i].id == commune.id)
                        this.administrativeUnits.communes[i] = commune;
                }
                this.deaction();
            },
            updateCommuneCompletely() {
                this.deaction();
            },
            deleteCommuneStart(communeId) {
                this.currentAction.name = 'deleteCommune';
                let object = this.administrativeUnits.communes.filter(f => f.id == communeId)[0];
                this.currentAction.object = cloneDeep(object);
            },
            deleteCommuneSuccessfully() {
                let index = -1;
                for (let i = 0; i < this.administrativeUnits.communes.length; i++) {
                    if (this.administrativeUnits.communes[i].id == this.currentAction.object.id) {
                        index = i;
                        break;
                    }
                }
                this.administrativeUnits.communes.splice(index, 1);
                this.deaction();
            },
            deleteCommuneCompletely() {
                 this.deaction();
            }
        },
        async created() {
            var communes = await communeRepository.getByDistrictId(this.districtId);
            this.administrativeUnits.communes = communes;
        }
    }
</script>
<style scoped>
    
    .item-row {
        display: flex;
    }
    .item-row-first{
        font-weight:bold
    }
    .item-row .col-index {
            width: 50px;
        }
        .item-row .col-expander {
            width: 50px;
        }
        .item-row .col-id {
            width: 250px;
            overflow-x: hidden;
            margin-right:20px;
        }
        .item-row .col-name {
            width: 30%;
        }
        .item-row .col-level {
            width: 20%;
        }
</style>