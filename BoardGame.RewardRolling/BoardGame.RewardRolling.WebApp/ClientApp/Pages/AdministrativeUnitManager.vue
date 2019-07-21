<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Quản lý đơn vị hành chính</h1></div>
            <div>
                <b-btn @click="openPopupImportStandardFile">Nhập file</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row item-row-first">
                <div class="col-index">STT</div>
                <div class="col-expander"></div>
                <div class="col-id">Mã</div>
                <div class="col-name">Tên</div>
                <div class="col-level">Cấp</div>
                <div :class="{'col-control':true, hovered:hovered}" @mouseover="hover" @mouseout="unhover">
                    <font-awesome-icon :icon="['fa', 'plus']" @click="startCreateCity"/>
                </div>
            </b-list-group-item>
            <b-list-group-item
                               v-for="(city, index) in administrativeUnits.cities"
                               :key="city.id">
                <div class="item-row">
                    <div class="col-index">{{index + 1}}</div>
                    <div :class="{'col-expander':true, hovered:hovered}"  @mouseover="hover" @mouseout="unhover">
                        <font-awesome-icon :icon="['fa', 'plus']"
                                           v-if="!city.expanded"
                                           @click="expandCity(city.id)" />
                        <font-awesome-icon :icon="['fa', 'minus']"
                                           v-if="city.expanded"
                                           @click="unexpandCity(city.id)" />

                    </div>
                    <div class="col-id">{{city.id}}</div>
                    <div class="col-name">{{city.name}}</div>
                    <div class="col-level">{{city.level.name}}</div>
                    <div :class="{'col-control':true, hovered:hovered}" @mouseover="hover" @mouseout="unhover">
                        <font-awesome-icon :icon="['fa', 'trash-alt']"
                                           @click="deleteCityStart(city.id)" />
                        <font-awesome-icon :icon="['fa', 'pencil-alt']"
                                           @click="updateCityStart(city.id)" />
                    </div>
                </div>
                <list-district 
                               :cityId="city.id"
                               v-if="city.expanded">

                </list-district>
            </b-list-group-item>

        </b-list-group>

        <div id="popup-container">
            <popup-import-standard-file :displayed="domElementStatuses.popupImportStandardFile.displayed"
                               @success="importStandardFileSuccessfully"
                               @hide="hidePopupImportStandardFile">

            </popup-import-standard-file>
        </div>

        <div>
            <popup-create-city @success="createCitySuccessfully"
                               @completed="createCityCompletely"
                               :visible="currentAction.name=='createCity'">
            </popup-create-city>
            <popup-update-city @success="updateCitySuccessfully"
                               @completed="updateCityCompletely"
                               v-if="currentAction.name=='updateCity'"
                               :visible="currentAction.name=='updateCity'"
                               :object="currentAction.object">
            </popup-update-city>
            <popup-city-delete @success="deleteCitySuccessfully"
                               @completed="deleteCityCompletely"
                               v-if="currentAction.name=='deleteCity'"
                               :visible="currentAction.name=='deleteCity'"
                               :object="currentAction.object">
            </popup-city-delete>
        </div>
    </div>
</template>
<script>
    import { mapActions } from 'vuex';
    import { mapState } from 'vuex';
    import cloneDeep from 'lodash/cloneDeep';
    import cityRepository from '../Repositories/CityRepository.js';
    import districtRepository from '../Repositories/DistrictRepository.js';
    import ListDistrict from '../Components/AdministrativeUnit/ListDistrict.vue';
    import PopupImportStandardFile from '../Components/AdministrativeUnit/PopupImportStandardFile.vue';
    import PopupCreateCity from '../Components/AdministrativeUnit/PopupCreateCity.vue';
    import PopupUpdateCity from '../Components/AdministrativeUnit/PopupUpdateCity.vue';
    import PopupCityDelete from '../Components/AdministrativeUnit/PopupCityDelete.vue';

    export default {
        name: 'administrative-unit-manager',
        components: {
            ListDistrict,
            PopupImportStandardFile,
            PopupCreateCity,
            PopupUpdateCity,
            PopupCityDelete
        },
        data() {
            return {
                administrativeUnits: {
                    cities:[]
                },
                domElementStatuses: {
                    popupImportStandardFile: {
                        displayed: false
                    }
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
            async getAllCity() {
                var cities = await cityRepository.getAll();
                return cities;
            },
            startDeleting() {

            },
            startCreateCity() {
                this.currentAction.name = 'createCity'
            },
            createCitySuccessfully(city) {
                this.currentAction.name = '';
                this.administrativeUnits.cities.push(city);
            },
            createCityCompletely() {
                this.currentAction.name = '';
            },
            updateCityStart(cityId) {
                this.currentAction.name = 'updateCity';
                let object = this.administrativeUnits.cities.filter(f => f.id == cityId)[0];
                this.currentAction.object = cloneDeep(object);
            },
            updateCitySuccessfully(city) {
                for (let i = 0; i < this.administrativeUnits.cities.length; i++) {
                    if (this.administrativeUnits.cities[i].id == city.id)
                        this.administrativeUnits.cities[i] = city;
                }
                this.deaction();
            },
            updateCityCompletely() {
                this.deaction();
            },
            deleteCityStart(cityId) {
                this.currentAction.name = 'deleteCity';
                let object = this.administrativeUnits.cities.filter(f => f.id == cityId)[0];
                this.currentAction.object = cloneDeep(object);
            },
            deleteCitySuccessfully() {
                let index = -1;
                for (let i = 0; i < this.administrativeUnits.cities.length; i++) {
                    if (this.administrativeUnits.cities[i].id == this.currentAction.object.id) {
                        index = i;
                        break;
                    }
                }
                this.administrativeUnits.cities.splice(index, 1);
                this.deaction();
            },
            deleteCityCompletely() {
                this.deaction();
            },
            async expandCity(cityId) {
                var city = this.administrativeUnits.cities.filter(f => f.id == cityId)[0];
                city.expanded = !city.expanded;
            },
            unexpandCity(cityId) {
                var city = this.administrativeUnits.cities.filter(f => f.id == cityId)[0];
                city.expanded = !city.expanded;
            },
            openPopupImportStandardFile() {
                this.domElementStatuses.popupImportStandardFile.displayed = true;
            },
            hidePopupImportStandardFile() {
                this.domElementStatuses.popupImportStandardFile.displayed = false;
            },
            importStandardFileSuccessfully(uploadResult) {
                this.domElementStatuses.popupImportStandardFile.displayed = false;
            },
        },
        async created() {
            var cities = await this.getAllCity();
            cities.forEach(f => {
                f.expanded = false;
            });
            this.administrativeUnits.cities = cities;
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