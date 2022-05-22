<template>
  <a-layout-sider class="sider">
    <a-menu
      mode="inline"
      :selectedKeys="selectedMenuItems"
      :style="{ height: '100%', borderRight: 0 }"
    >
      <a-menu-item v-if="isAuthorized" key="home">
        <router-link :to="{ name: 'Home' }">
          <a-icon type="layout" /> 
          <span v-text="'Главная'" />
        </router-link>
      </a-menu-item>
      <a-menu-item v-if="isAuthorized && isAdministrator" key="dashboard">
        <a-icon type="dashboard" /> 
        <span v-text="'Приборная панель'" />
      </a-menu-item>
      <a-sub-menu v-if="isAuthorized && isAdministrator" key="database">
        <span slot="title"><a-icon type="database" /> База данных</span>
      </a-sub-menu>
      <a-menu-item v-if="isAuthorized" key="csmoney">
        <router-link :to="{ name: 'CSMoney' }">
          <a-icon type="shopping-cart" />
          <span v-text="'Магазин CS.Money'" />
        </router-link>
      </a-menu-item>
      <a-menu-item v-if="isAuthorized" key="store">
        <a-icon type="shopping-cart" />
        <span v-text="'Магазин'" />
      </a-menu-item>
      <a-menu-item v-if="!isAuthorized" key="registration">
        <a-icon type="user-add" />
        <span v-text="'Регистрация'" />
      </a-menu-item>
      <a-menu-item v-if="!isAuthorized" key="forgotPassword">
        <a-icon type="key" />
        <span v-text="'Забыли пароль ?'" />
      </a-menu-item>
      <a-sub-menu v-if="isAuthorized" key="account">
        <span slot="title"><a-icon type="user" /> Мой аккаунт</span>
      </a-sub-menu>
      <a-menu-item v-if="isAuthorized" key="exit" @click="logout">
        <a-icon type="close-circle" /> 
        <span v-text="'Выход'" />
      </a-menu-item>
    </a-menu>
  </a-layout-sider>
</template>

<script>
  import { mapGetters } from 'vuex'

  export default {
    name: 'LayoutSider',

    computed: {
      ...mapGetters({
        roleName: 'auth/roleName',
        isAuthorized: 'auth/isAuthorized',
      }),
      isAdministrator() {
        return this.roleName === 'Администратор'
      },
      selectedMenuItems() {
        return [this.$route.name?.toLowerCase() || '']
      },
    },

    methods: {
      async logout() {
        await this.$store.dispatch('auth/logout')
        this.$router.push({ name: 'Login' })
      },
    },
  }
</script>

<style scoped>
  .sider {
    width: 200px;
    background: #fff;
  }
</style>
