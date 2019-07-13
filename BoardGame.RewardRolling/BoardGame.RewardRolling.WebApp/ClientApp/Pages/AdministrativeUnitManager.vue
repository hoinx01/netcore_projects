<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Quản lý đơn vị hành chính</h1></div>
            <div>
                <b-btn>Nhập file</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-expander"></div>
                <div class="col-name">Tên</div>
                <div class="col-level">Cấp</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item class="item-row"
                               v-for="(city, index) in administrativeUnits.cities"
                               :key="city.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-expander">
                    <span v-if="!city.expanded">+</span>
                    <span v-if="city.expanded">-</span>
                </div>
                <div class="col-name">{{city.name}}</div>
                <div class="col-level">{{city.level.name}}</div>
                <div class="col-control">
                    <font-awesome-icon :icon="['fa', 'trash-alt']"
                                       @click="startDeleting(city.id)" />
                    <font-awesome-icon :icon="['fa', 'pencil-alt']"
                                       @click="startUpdating(city.id)" />
                </div>
            </b-list-group-item>

        </b-list-group>

        <div id="popup-container">

        </div>
    </div>
</template>
<script>
    import cityRepository from '../Repositories/CityRepository.js';

    export default {
        name: 'administrative-unit-manager',
        data() {
            return {
                administrativeUnits: {
                    cities:[]
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

            }
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
        .item-row .col-name {
            width: 30%;
        }
        .item-row .col-level {
            width: 20%;
        }
</style>