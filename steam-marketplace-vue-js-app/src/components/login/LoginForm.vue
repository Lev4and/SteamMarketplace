<template>
  <div id="loginForm">
    <spinner :loading="isLoading">
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
                <a-button type="primary" html-type="submit" class="login-form-button !w-full">
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
      </spinner>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { Login } from '@/services/utils/modelsUtils'
  import Spinner from '@/components/common/Spinner'

  export default {
    name: 'LoginForm',

    components: {
      Spinner,
    },

    data: () => ({
      isLoading: false,
    }),

    beforeCreate() {
      this.form = this.$form.createForm(this, { name: 'normal_login' })
    },

    computed: {
      ...mapGetters({
        isAuthorized: 'auth/isAuthorized',
      }),
    },

    methods: {
      handleSubmit(event) {
        event.preventDefault()
        this.form.validateFields(async (err, values) => {
          if (!err) {
            this.isLoading = true
            const result = await this.$store.dispatch('auth/login', new Login(values.login, values.password))
            if (result) this.$router.push(this.$route.query.redirectFrom || { name: 'Home' })
            this.isLoading = false
          }
        })
      },
    },
  }
</script>
