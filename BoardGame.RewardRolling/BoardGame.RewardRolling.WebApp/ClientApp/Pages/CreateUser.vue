<template>
    <div>
        <b-form @submit="onSubmit">
            <b-form-group id="userNameGroup"
                          label="Tên đăng nhập"
                          label-for="userNameBox">
                <b-form-input id="userNameBox"
                              type="text"
                              v-model="model.userName"
                              required
                              placeholder="Tên đăng nhập">
                </b-form-input>
            </b-form-group>
            <b-form-group id="passwordGroup"
                          label="Mật khẩu"
                          label-for="passwordBox">
                <b-form-input id="passwordBox"
                              type="password"
                              v-model="model.password"
                              required
                              placeholder="Mật khẩu">
                </b-form-input>
            </b-form-group>
            <b-form-group id="reEnterPasswordGroup"
                          label="Xác nhận mật khẩu"
                          label-for="reEnterPasswordBox">
                <b-form-input id="reEnterPasswordBox"
                              type="password"
                              v-model="model.rePassword"
                              required
                              placeholder="Nhập lại mật khẩu">
                </b-form-input>
            </b-form-group>
            <b-form-group id="nameGroup"
                          label="Họ tên"
                          label-for="nameBox">
                <b-form-input id="nameBox"
                              type="text"
                              v-model="model.name"
                              required>
                </b-form-input>
            </b-form-group>
            <b-button type="submit" variant="primary">Submit</b-button>
        </b-form>
    </div>
</template>
<script>
    import userRepository from '../Repositories/UserRepository';
    export default {
        name: "create-user",
        data() {
            return {
                model: {
                    userName: '',
                    password: '',
                    rePassword: '',
                    name: ''
                }
            }
        },
        methods: {
            async onSubmit() {
                let account = await userRepository.createUser(this.model);
                this.$router.push("/admin/users")
            }
        }
    }
</script>