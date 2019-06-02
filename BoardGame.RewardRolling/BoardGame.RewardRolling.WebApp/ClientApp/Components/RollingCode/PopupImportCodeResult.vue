<template>
    <b-modal :visible="displayed"
             title="Kết quả nhập file"
             @hide="hide">
        <div v-if="data">
            <p>Số mã nhập vào: {{data.totalRecords}}</p>
            <p>Nhập thành công: {{data.successRecordCount}}</p>
            <p>Số mã gặp lỗi: {{data.failedRecordCount}}</p>
            <div>
                <p>Danh sách lỗi:</p>
                <ul>
                    <li 
                        v-for="(item,index) in data.errors"
                        :key="index">
                        <p>Dòng {{item.rowIndex}}: {{item.errorMessage}}</p>
                    </li>
                </ul>
            </div>

        </div>
    </b-modal>
</template>
<script>
    export default {
        name: 'popup-import-code-result',
        props: {
            data: {
                type: Object,
                required: true
            },
            displayed: {
                type: Boolean,
                required: true
            }
        },
        methods: {
            hide() {

            }
        }
    }
</script>