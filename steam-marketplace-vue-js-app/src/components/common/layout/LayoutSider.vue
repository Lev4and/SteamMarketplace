<template>
  <a-layout-sider class="sider">
    <a-menu
      mode="inline"
      :selectedKeys="selectedMenuItems"
      class="h-full border-r-[0px]"
    >
      <a-menu-item v-if="isAuthorized" key="home">
        <router-link :to="{ name: 'Home' }">
          <a-icon type="layout" /> 
          <span v-text="'Главная'" />
        </router-link>
      </a-menu-item>
      <a-menu-item v-if="isAuthorized && isAdministrator" key="dashboard">
        <router-link :to="{ name: 'Dashboard' }">
          <a-icon type="dashboard" /> 
          <span v-text="'Приборная панель'" />
        </router-link>
      </a-menu-item>
      <a-menu-item v-if="isAuthorized" key="online">
        <router-link :to="{ name: 'Online' }">
          <a-row type="flex" align="middle" justify="space-between">
            <a-col>
              <a-icon type="global" /> 
              <span v-text="'Онлайн'" />
            </a-col>
            <a-col>
              <a-badge :count="countOnlineUsers" :showZero="true" :overflowCount="1000" />
            </a-col>
          </a-row>
        </router-link>
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
        <router-link :to="{ name: 'Store' }">
          <a-icon type="shopping-cart" />
          <span v-text="'Магазин'" />
        </router-link>
      </a-menu-item>
      <a-menu-item v-if="!isAuthorized" key="registration">
        <a-icon type="user-add" />
        <span v-text="'Регистрация'" />
      </a-menu-item>
      <a-menu-item v-if="!isAuthorized" key="forgotPassword">
        <a-icon type="key" />
        <span v-text="'Забыли пароль ?'" />
      </a-menu-item>
      <a-sub-menu v-if="isAuthorized && isAdministrator" key="import">
        <span slot="title"><a-icon type="import" /> Импорт</span>
        <a-menu-item key="skins">
          <router-link :to="{ name: 'Skins' }">
            <span v-text="'Скины из CS.Money'" />
          </router-link>
        </a-menu-item>
      </a-sub-menu>
      <a-sub-menu v-if="isAuthorized" key="account">
        <span slot="title"><a-icon type="user" /> Мой аккаунт</span>
        <a-menu-item key="myinventory">
          <router-link :to="{ name: 'MyInventory' }">
            <span v-text="'Мой инвентарь'" />
          </router-link>
        </a-menu-item>
        <a-menu-item key="mypurchases">
          <router-link :to="{ name: 'MyPurchases' }">
            <span v-text="'Мои покупки'" />
          </router-link>
        </a-menu-item>
        <a-menu-item key="mysales">
          <router-link :to="{ name: 'MySales' }">
            <a-row type="flex" align="middle" justify="space-between">
              <a-col>
                <span v-text="'Мои продажи'" />
              </a-col>
              <a-col>
                <a-badge :count="countActiveSales" :showZero="true" :overflowCount="50" />
              </a-col>
          </a-row>
          </router-link>
        </a-menu-item>
      </a-sub-menu>
      <a-menu-item v-if="isAuthorized" key="logout">
        <router-link :to="{ name: 'Logout' }">
          <a-icon type="close-circle" /> 
          <span v-text="'Выход'" />
        </router-link>
      </a-menu-item>
    </a-menu>
  </a-layout-sider>
</template>

<script>
  import { mapGetters } from 'vuex'
  import API from '@/api'

  export default {
    name: 'LayoutSider',

    data: () => ({
      countOnlineUsers: 0,
      countActiveSales: 0,
    }),

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

    watch: {
      isAuthorized: {
        async handler(newValue) {
          if (newValue) await this.loadCountActiveSales()
        },
        immediate: true,
      },
    },

    mounted() {
      document.addEventListener('onlinechanged', this.onOnlineChanged)
    },

    beforeDestroy() {
      document.removeEventListener('onlinechanged', this.onOnlineChanged)
    },

    methods: {
      async logout() {
        await this.$store.dispatch('auth/logout')
        this.$router.push({ name: 'Login' })
      },
      onOnlineChanged(event) {
        this.countOnlineUsers = event?.detail?.data?.() || 0
      },
      async loadCountActiveSales() {
        try {
          const response = await API.sales.getCountActiveSales()
          if (response.status.isSuccessful) {
            this.countActiveSales = response.result || 0
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке активных продаж')
        }
      },
    },
  }
</script>
