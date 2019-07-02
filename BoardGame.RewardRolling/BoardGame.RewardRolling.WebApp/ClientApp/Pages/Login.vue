<template>
    <div id="container">
        <b-form @submit="onSubmit" id="form" class="border border-primary rounded">
            <div class="form-group">
                <div class="label-input"><label>Tên đăng nhập:</label></div>
                <div class="input-group">
                    <input type="text" v-model="loginModel.userName" class="textbox" />
                </div>
            </div>
            <div class="form-group">
                <div class="label-input"><label>Password:</label></div>
                <div class="input-group">
                    <input type="password" v-model="loginModel.password" class="textbox" />
                </div>
            </div>
            <div id="btn-group">
                <b-button type="submit" variant="primary">Đăng nhập</b-button>
            </div>
        </b-form>
    </div>
</template>
<script>
    import userRepository from '../Repositories/UserRepository';
    export default {
        name: 'LoginForm',
        data() {
            return {
                loginModel: {
                    userName: '',
                    password: ''
                }
            }
        },
        methods: {
            async onSubmit(evt) {
                evt.preventDefault();
                try {
                    var loginResult = await userRepository.login(this.loginModel);
                    console.log(loginResult);
                    localStorage.setItem('bearerToken', loginResult.token);
                    this.$router.push('/');
                }
                catch (exception) {
                    alert(exception.messages[0])
                }
            }
        }
    }
</script>
<style scoped>
    #container {
        display: flex;
        align-items: center;
        justify-content: center;
        height: 100%;
    }

    #form {
        width: 30%;
        min-width: 350px;
        padding: 20px 10px;
        background-color: rgba(0,0,0,0.4)
    }

    .form-group {
        display: flex;
    }

    .label-input {
        flex-basis: 20%;
        min-width: 125px;
    }

    .input-group {
        flex-grow: 1;
    }

        .input-group .textbox {
            width: 100%;
        }
</style>