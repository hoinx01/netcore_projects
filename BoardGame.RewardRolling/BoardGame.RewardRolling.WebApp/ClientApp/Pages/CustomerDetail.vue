<template>
    <div class="router-view-column-flow">
        <div id="main-header" class="router-view-header">
            <div><h1>Thông tin khách hàng</h1></div>
        </div>
        <div id="main-content" class="router-view-content">
            <div class="prop-group">
                <label class="prop-label">Tên:</label>
                <div class="prop-value">{{customer.fullName}}</div>
            </div>
            <div class="prop-group">
                <label class="prop-label">Số điện thoại:</label>
                <div class="prop-value">{{customer.phoneNumber}}</div>
            </div>
            <div class="prop-group">
                <label class="prop-label">Email:</label>
                <div class="prop-value">{{customer.email}}</div>
            </div>
            <div class="prop-group">
                <label class="prop-label">Ngày sinh:</label>
                <div class="prop-value">{{dob}}</div>
            </div>
            <div class="prop-group">
                <label class="prop-label">Giới tính:</label>
                <div class="prop-value">{{customer.genderName}}</div>
            </div>
            <div class="prop-group">
                <label class="prop-label">Địa chỉ:</label>
                <div class="prop-value">{{customer.displayedAddress}}</div>
            </div>
            <div class="prop-group">
                <label class="prop-label">Quà tặng:</label>
                <div class="prop-value">{{customer.reward.name}}</div>
            </div>
        </div>


    </div>
</template>

<script>
    import customerRepository from '../Repositories/CustomerRepository';
    export default {
        name: 'customer-detail',
        data() {
            return {
                customer: {

                }
            }
        },
        computed: {
            dob() {
                if (this.customer.birthday == null)
                    return '';
                return this.customer.birthday.day + "/" + this.customer.birthday.month + "/" + this.customer.birthday.year;
            }
        },
        async created() {
            let id = this.$route.params.id;
            console.log(id)
            try {
                var customer = await customerRepository.getById(id);
                console.log(customer)
                this.customer = customer;
                
            }
            catch (exception) {
                console.log(exception)
            }
        }
    }
</script>
<style scoped>
    .prop-group{
        display:flex;
    }
    .prop-label{
        width: 120px;
    }
</style>