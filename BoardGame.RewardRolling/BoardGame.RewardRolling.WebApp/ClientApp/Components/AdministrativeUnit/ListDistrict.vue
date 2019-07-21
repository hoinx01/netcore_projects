<template>
    <b-list-group id="main-content" class="router-view-content">
        <b-list-group-item class="item-row item-row-first">
            <div class="col-index">STT</div>
            <div class="col-expander"></div>
            <div class="col-id">Mã</div>
            <div class="col-name">Tên</div>
            <div class="col-level">Cấp</div>
            <div 
                 :class="{'col-control':true, hovered:hovered}" 
                 @mouseover="hover" 
                 @mouseout="unhover">
                <font-awesome-icon :icon="['fa', 'plus']" @click="createDistrictStart" />
            </div>
        </b-list-group-item>
        <b-list-group-item 
                           v-for="(item, index) in administrativeUnits.districts"
                           :key="item.id">
            <div class="item-row">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-expander">
                    <font-awesome-icon :icon="['fa', 'plus']"
                                       v-if="!item.expanded" 
                                       @click="expandDistrict(item.id)"/>
                    <font-awesome-icon :icon="['fa', 'minus']"
                                       v-if="item.expanded" 
                                       @click="unexpandDistrict(item.id)"/>

                </div>
                <div class="col-id">{{item.id}}</div>
                <div class="col-name">{{item.name}}</div>
                <div class="col-level">{{item.level.name}}</div>
                <div 
                     :class="{'col-control':true, hovered:hovered}" >
                    <font-awesome-icon 
                                       :icon="['fa', 'trash-alt']" 
                                       @click="deleteDistrictStart(item.id)"/>
                    <font-awesome-icon 
                                       :icon="['fa', 'pencil-alt']" 
                                       @click="updateDistrictStart(item.id)"/>
                </div>
            </div>
            <list-commune 
                          :cityId="cityId"
                          :districtId="item.id" 
                          v-if="item.expanded">

            </list-commune>
        </b-list-group-item>
        <div>
            <popup-district-create @success="createDistrictSuccessfully"
                                   @completed="createDistrictCompletely"
                                   v-if="currentAction.name=='createDistrict'"
                                   :visible="currentAction.name=='createDistrict'"
                                   :cityId="cityId">
            </popup-district-create>
            <popup-district-update @success="updateDistrictSuccessfully"
                                   @completed="updateDistrictCompletely"
                                   v-if="currentAction.name=='updateDistrict'"
                                   :visible="currentAction.name=='updateDistrict'"
                                   :object="currentAction.object">
            </popup-district-update>
            <popup-district-delete @success="deleteDistrictSuccessfully"
                                   @completed="deleteDistrictCompletely"
                                   v-if="currentAction.name=='deleteDistrict'"
                                   :visible="currentAction.name=='deleteDistrict'"
                                   :object="currentAction.object">
            </popup-district-delete>
        </div>
    </b-list-group>
</template>
<script>
    import { mapActions } from 'vuex';
    import cloneDeep from 'lodash/cloneDeep';
    import districtRepository from '../../Repositories/DistrictRepository';
    import ListCommune from '../AdministrativeUnit/ListCommune.vue';
    import PopupDistrictCreate from './PopupDistrictCreate.vue';
    import PopupDistrictUpdate from './PopupDistrictUpdate.vue';
    import PopupDistrictDelete from './PopupDistrictDelete.vue';

    export default {
        name: 'list-district',
        components: {
            ListCommune,
            PopupDistrictCreate,
            PopupDistrictUpdate,
            PopupDistrictDelete
        },
        props: {
            cityId: {
                type: String,
                required: true
            }
        },
        data() {

            return {
                administrativeUnits: {
                    districts: []
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
            createDistrictStart() {
                this.currentAction.name = 'createDistrict'
            },
            createDistrictSuccessfully(district) {
                this.administrativeUnits.districts.push(district);
                this.deaction();
            },
            createDistrictCompletely() {
                this.deaction();
            },
            updateDistrictStart(districtId) {
                let object = this.administrativeUnits.districts.filter(f => f.id == districtId)[0];
                console.log(object);
                this.currentAction.object = cloneDeep(object);
                this.currentAction.name = 'updateDistrict';
            },
            updateDistrictSuccessfully(district) {
                for (let i = 0; i < this.administrativeUnits.districts.length; i++) {
                    if (this.administrativeUnits.districts[i].id == district.id)
                        this.administrativeUnits.districts[i] = district;
                }
                this.deaction();
            },
            updateDistrictCompletely() {
                this.deaction();
            },
            deleteDistrictStart(districtId) {
                this.currentAction.name = 'deleteDistrict';
                let object = this.administrativeUnits.districts.filter(f => f.id == districtId)[0];
                this.currentAction.object = cloneDeep(object);
            },
            deleteDistrictSuccessfully() {
                let index = -1;
                for (let i = 0; i < this.administrativeUnits.districts.length; i++) {
                    if (this.administrativeUnits.districts[i].id == this.currentAction.object.id) {
                        index = i;
                        break;
                    }
                }
                this.administrativeUnits.districts.splice(index, 1);
                this.deaction();
            },
            deleteDistrictCompletely() {
                 this.deaction();
            },
            expandDistrict(districtId) {
                var district = this.administrativeUnits.districts.filter(f => f.id == districtId)[0];
                district.expanded = !district.expanded;
            },
            unexpandDistrict(districtId) {
                var district = this.administrativeUnits.districts.filter(f => f.id == districtId)[0];
                district.expanded = !district.expanded;
            },
        },
        async created() {
            try {
                var districts = await districtRepository.getByCityId(this.cityId);
                var displayedDistricts = cloneDeep(districts)

                displayedDistricts.forEach(f => {
                    f.expanded = false;
                });
                this.administrativeUnits.districts = displayedDistricts;
            }
            catch (exception) {

            }
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