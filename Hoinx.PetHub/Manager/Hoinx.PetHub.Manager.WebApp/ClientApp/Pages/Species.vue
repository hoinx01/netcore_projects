<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Danh sách loại PET</h1></div>
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
                               v-for="(item,index) in visibleElements"
                               :key="item.id">
                <div class="col-index">{{index + 1}}</div>
                <div class="col-name">{{item.name}}</div>
                <div class="col-control">
                    <font-awesome-icon :icon="['fa', 'trash-alt']" 
                                       @click="startDeleting(item.id)"/>
                    <font-awesome-icon :icon="['fa', 'pencil-alt']"
                                       @click="startEditing(item.id)"/>
                </div>
            </b-list-group-item>
        </b-list-group>

        <div id="main-footer" class="router-view-footer">
            <pagination :total="filter.result.pagination.count"
                        :page-index="filter.result.pagination.page"
                        :page-size="filter.result.pagination.limit"
                        :paginationSize="5"
                        :displayPageSizeSelection="true"
                        @page-index-change="changePageIndex"
                        @page-size-change="changePageSize">

            </pagination>
        </div>

        <div>
            <popup-create-species @creating-object-change="changeCreating"
                                  @success="createSuccessfully"
                                  @completed="createCompletely"
                                  :visible="currentAction.name=='create'">

            </popup-create-species>

            <popup-edit-species :visible="currentAction.name=='edit'"
                                :object="editingObject"
                                @object-changed="changeEditing"
                                @success="editSuccessfully"
                                @completed="editCompletely">

            </popup-edit-species>

            <popup-delete-species
                                :visible="currentAction.name=='delete'"
                                :object="deletingObject"
                                @success="deleteSuccessfully"
                                @completed="deleteCompletely">

            </popup-delete-species>
        </div>
        


    </div>
</template>
<script>
    import pagination from '../Components/Shared/_Pagination.vue';
    import popupCreateSpecies from '../Components/PetSpecies/PopupCreate.vue';
    import popupEditSpecies from '../Components/PetSpecies/PopupEdit.vue';
    import popupDeleteSpecies from '../Components/PetSpecies/PopupDelete.vue';

    import speciesRepository from '../Repositories/SpeciesRepository';
    import cloneDeep from 'lodash/cloneDeep';

    export default {
        name: "species-index",
        components: {
            pagination,
            popupCreateSpecies,
            popupEditSpecies,
            popupDeleteSpecies
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
                            limit: 10
                        }
                    }
                },
                currentAction: {
                    name: null,
                    status:'',
                    object: null
                }
            }
        },
        watch: {
            'currentAction.status': function (newStatus, oldStatus) {
                console.log(oldStatus + ' -> ' + newStatus)
                if (newStatus == 'success') {
                    this.deaction();
                    this.doFilter();
                }
                if (newStatus == 'completed') {
                    this.deaction();
                }   
            }
        },
        methods: {
            async changePageIndex(page) {
                this.filter.request.page = page;
                await this.doFilter();
            },
            async changePageSize(limit) {
                this.filter.request.limit = limit;
                await this.doFilter();
            },
            async doFilter() {
                this.filter.result = await speciesRepository.filter(this.filter.request);
            },
            deaction() {
                this.currentAction.name = null;
                this.currentAction.status = '';
                this.currentAction.object = null;
            },
            startCreating() {
                this.currentAction.name = 'create';
                this.currentAction.status = 'continuous';
            },
            changeCreating(object) {
                this.currentAction.object = object;
            },
            createFailed() {
                this.currentAction.status = 'failed';
            },
            createSuccessfully(species) {
                this.currentAction.status = 'success';
            },
            createCompletely() {
                this.currentAction.status = 'completed';
                this.deaction();
            },
            startEditing(id) {
                this.currentAction.name = 'edit';
                this.currentAction.status = 'continuous';

                let object = this.filter.result.petSpecies.filter(f => f.id == id)[0];
                this.currentAction.object = cloneDeep(object);
            },
            changeEditing(species) {
                this.currentAction.object = species;
            },            
            editSuccessfully() {
                this.$set(this.currentAction, 'status', 'success');
            },
            editFailed() {

            },
            editCompletely() {
                this.deaction();
            },
            startDeleting(id) {
                this.currentAction.name = 'delete';
                this.currentAction.status = 'continuous';
                let object = this.filter.result.petSpecies.filter(f => f.id == id)[0];
                this.currentAction.object = cloneDeep(object);
            },
            deleteSuccessfully() {
                this.$set(this.currentAction, 'status', 'success');
            },
            deleteFailed() {

            },
            deleteCompletely() {
                this.deaction();
            },
        },
        computed: {
            visibleElements: function () {
                let elements = cloneDeep(this.filter.result.petSpecies);
                if (this.currentAction.name == 'create') {
                    if (this.currentAction.object == null) {
                        let creating = { name: '', alias: '' };                        
                        elements.unshift(creating);
                    }
                    else
                        elements.unshift(this.currentAction.object);
                }
                elements.forEach((element) => {
                    element.actionStatus = 'stable';
                    if (this.currentAction.name == 'edit') {
                        if (this.currentAction.object.id == element.id) {
                            element.actionStatus = 'editing';
                            element.name = this.currentAction.object.name;
                            element.alias = this.currentAction.object.alias;
                        }
                    }
                });
                return elements;
            },
            editingObject: function () {
                if (this.currentAction.name != 'edit')
                    return { id: 0, name: '', alias: ''};
                else {
                    let object = cloneDeep(this.currentAction.object);
                    return object;
                }
            },
            deletingObject: function () {
                if (this.currentAction.name != 'delete')
                    return { id: 0, name: '', alias: ''};
                else {
                    let object = cloneDeep(this.currentAction.object);
                    return object;
                }
            }
        },
        async created() {
            await this.doFilter();
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