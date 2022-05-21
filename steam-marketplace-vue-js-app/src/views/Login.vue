<template>
  <a-spin
    :spinning="isLoading"
  >
    <a-icon
      slot="indicator"
      type="loading"
      style="font-size: 96px"
      spin
    />
    <a-form
      id="components-form-demo-normal-login"
      :form="form"
      class="login-form"
      @submit="handleSubmit"
    >
      <a-row>
        <a-col :span="24">
          <a-form-item>
            <a-input
              v-decorator="[
                'login',
                { rules: [{ required: true, message: 'Пожалуйста, введите свое имя пользователя или e-mail!' }] },
              ]"
              placeholder="Логин или e-mail"
            >
              <a-icon slot="prefix" type="user" style="color: rgba(0,0,0,.25)" />
            </a-input>
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item>
            <a-input
              v-decorator="[
                'password',
                { rules: [{ required: true, message: 'Пожалуйста, введите свой пароль!' }] },
              ]"
              type="password"
              placeholder="Пароль"
            >
              <a-icon slot="prefix" type="lock" style="color: rgba(0,0,0,.25)" />
            </a-input>
          </a-form-item>
        </a-col>
        <a-col :span="24">
          <a-form-item>
            <a-button type="primary" html-type="submit" class="login-form-button">
              Войти
            </a-button>
            или
            <a href="">
              зарегистрируйтесь прямо сейчас!
            </a>
          </a-form-item>
        </a-col>
      </a-row>
    </a-form>
  </a-spin>
</template>

<script>
  import API from '@/api'
  import { Login } from '@/services/utils/modelsUtils'

  export default {
    name: 'Login',

    data: () => ({
      isLoading: false,
    }),

    beforeCreate() {
      this.form = this.$form.createForm(this, { name: 'normal_login' })
    },

    methods: {
      handleSubmit(event) {
        event.preventDefault()
        this.form.validateFields(async (err, values) => {
          if (!err) {
            this.isLoading = true
            const response = await API.authorization.login(new Login(values.login, values.password))
            if (response.status.isSuccessful()) {
              this.$notification['success']({
                message: 'Успех',
                description:
                  'Успешная авторизация.',
              })
            } else {
              this.$notification['error']({
                message: 'Ошибка',
                description:
                  response.status.message,
              })
            }
            this.isLoading = false
          }
        })
      },
    },
  }
</script>

<style>
  .ant-spin.ant-spin-spinning {
    top: 50% !important;
    left: 50% !important;
    z-index: 4 !important;
    width: auto !important;
    height: auto !important;
    position: fixed !important;
    margin-top: -48px !important;
    margin-left: -48px !important;
  }
  #components-form-demo-normal-login .login-form {
    max-width: 300px;
  }
  #components-form-demo-normal-login .login-form-button {
    width: 100%;
  }
</style>

<style scoped>

</style>
