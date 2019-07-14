<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Quản lý đơn vị hành chính</h1></div>
            <div>
                <b-btn @click="openPopupImportStandardFile">Nhập file</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-expander"></div>
                <div class="col-id">Mã</div>
                <div class="col-name">Tên</div>
                <div class="col-level">Cấp</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item
                               v-for="(city, index) in administrativeUnits.cities"
                               :key="city.id">
                <div class="item-row">
                    <div class="col-index">{{index + 1}}</div>
                    <div class="col-expander">
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
                    <div class="col-control">
                        <font-awesome-icon :icon="['fa', 'trash-alt']"
                                           @click="startDeleting(city.id)" />
                        <font-awesome-icon :icon="['fa', 'pencil-alt']"
                                           @click="startUpdating(city.id)" />
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
    </div>
</template>
<script>
    import cityRepository from '../Repositories/CityRepository.js';
    import districtRepository from '../Repositories/DistrictRepository.js';
    import ListDistrict from '../Components/AdministrativeUnit/ListDistrict.vue';
    import PopupImportStandardFile from '../Components/AdministrativeUnit/PopupImportStandardFile.vue';

    export default {
        name: 'administrative-unit-manager',
        components: {
            ListDistrict,
            PopupImportStandardFile
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
                }
            }
        },
        methods: {
            async getAllCity() {
                var cities = await cityRepository.getAll();
                return cities;
            },
            startDeleting() {

            },
            startUpdating(){

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
        .item-row .col-index {
            width: 50px;
        }
        .item-row .col-expander {
            width: 50px;
        }
        .item-row .col-id {
            width: 50px;
        }
        .item-row .col-name {
            width: 30%;
        }
        .item-row .col-level {
            width: 20%;
        }
</style>