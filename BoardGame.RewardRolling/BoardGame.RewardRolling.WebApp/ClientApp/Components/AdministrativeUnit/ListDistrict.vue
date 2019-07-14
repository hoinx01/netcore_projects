<template>
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
                <div class="col-control">
                    <font-awesome-icon :icon="['fa', 'trash-alt']" />
                    <font-awesome-icon :icon="['fa', 'pencil-alt']" />
                </div>
            </div>
            <list-commune :districtId="item.id" v-if="item.expanded">

            </list-commune>
        </b-list-group-item>

    </b-list-group>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import districtRepository from '../../Repositories/DistrictRepository';
    import ListCommune from '../AdministrativeUnit/ListCommune.vue';

    export default {
        name: 'list-district',
        components: {
            ListCommune
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
                }
            }
        },
        methods: {
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
            var districts = await districtRepository.getByCityId(this.cityId);
            var displayedDistricts = cloneDeep(districts)

            displayedDistricts.forEach(f => {
                f.expanded = false;
            });
            this.administrativeUnits.districts = displayedDistricts;
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