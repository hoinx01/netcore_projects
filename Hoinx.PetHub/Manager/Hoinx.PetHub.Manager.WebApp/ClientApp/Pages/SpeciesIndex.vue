<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách loại PET</h1></div>
            <div>
                <b-btn v-b-modal.popup-create-species>Thêm</b-btn>
            </div>
        </div>
        <b-list-group id="main-content" class="router-view-content">
            <b-list-group-item class="item-row">
                <div class="col-index">STT</div>
                <div class="col-name">Tên</div>
                <div class="col-control"></div>
            </b-list-group-item>
            <b-list-group-item 
                               class="item-row"
                               v-for="(item,index) in filter.result.petSpecies"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name">{{item.name}}</div>
                <div class="col-control">
                    <font-awesome-icon :icon="['fa', 'trash-alt']" />
                    <font-awesome-icon :icon="['fa', 'pencil-alt']" />
                </div>
            </b-list-group-item>
        </b-list-group>
        
        <div id="main-footer" class="router-view-footer">
            <pagination 
                        :total="filter.result.pagination.count" 
                        :page-index="filter.result.pagination.page"
                        :page-size="filter.result.pagination.limit" 
                        :paginationSize="5" 
                        :displayPageSizeSelection="true"
                        @page-change="changePage"
                        @page-size-change="changePageSize">

            </pagination>
        </div>
        <popup-create-species v-on:species-added="addSpeciesCompletely"></popup-create-species>
        <popup-edit-species v-on:species-added="editSpeciesCompletely"></popup-edit-species>
    </div>
</template>
<script>
    import speciesRepository from '../Repositories/SpeciesRepository'
    import pagination from '../Components/Shared/_Pagination.vue'
    import popupCreateSpecies from '../Components/PetSpecies/PopupCreate.vue'
    import popupEditSpecies from '../Components/PetSpecies/PopupEdit.vue'
    export default {
        name: "species-index",
        components: {
            pagination,
            popupCreateSpecies,
            popupEditSpecies
        },
        data() {
            return {
                filter: {
                    request: {
                        page: 1,
                        limit: 10
                    },
                    result: {
                        petSpecies: [],
                        pagination: {
                            count: 0,
                            page: 1,
                            limit:20
                        }
                    }
                }
                
            }
        },
        watch: {
            'filter.request.page': async function () {
                await this.doFilter();
            },
            'filter.request.limit': async function () {
                await this.doFilter();
            }
        },
        methods: {
            async doFilter() {
                var filterResult = await speciesRepository.filter(this.filter.request);
                this.filter.result = filterResult;
            },
            changePage(page) {
                this.filter.request.page = page;
            },
            changePageSize(limit) {
                this.filter.request.limit = limit;
            },
            addSpeciesCompletely(species) {
                this.filter.result.petSpecies.unshift(species);
            },
            editSpeciesCompletely(species) {
                //this.filter.result.petSpecies.unshift(species);
            }
        },
        async created() {
            await this.doFilter();
        }
    }
</script>
<style scoped>
    .item-row{
        display:flex;
    }
    .item-row .col-index{
        width:50px;
    }
    .item-row .col-name{
        width: 30%;
    }
</style>