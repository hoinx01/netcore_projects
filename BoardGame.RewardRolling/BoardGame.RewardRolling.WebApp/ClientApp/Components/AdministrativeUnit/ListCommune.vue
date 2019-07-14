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
        <b-list-group-item class="item-row"
                           v-for="(item, index) in administrativeUnits.communes"
                           :key="item.id">
            <div class="col-index">{{index + 1}}</div>
            <div class="col-expander">
                <font-awesome-icon :icon="['fa', 'plus']"
                                   v-if="!item.expanded"/>
                <font-awesome-icon :icon="['fa', 'minus']"
                                   v-if="item.expanded"/>

            </div>
            <div class="col-id">{{item.id}}</div>
            <div class="col-name">{{item.name}}</div>
            <div class="col-level">{{item.level.name}}</div>
            <div class="col-control">
                <font-awesome-icon :icon="['fa', 'trash-alt']"/>
                <font-awesome-icon :icon="['fa', 'pencil-alt']"/>
            </div>
        </b-list-group-item>

    </b-list-group>
</template>
<script>
    import cloneDeep from 'lodash/cloneDeep';
    import communeRepository from '../../Repositories/CommuneRepository';

    export default {
        name: 'list-commune',
        props: {
            districtId: {
                type: String,
                required: true
            }
        },
        data() {

            return {
                administrativeUnits: {
                    communes: []
                }
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