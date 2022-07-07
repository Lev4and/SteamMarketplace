<template>
  <div class="grouped-item skin-item">
    <a-row :gutter="[0,12]">
      <a-col :span="24" class="image-container">
        <img :src="item.steamImg" />
      </a-col>
      <a-col :span="24">
        <a-tooltip placement="topLeft">
          <template slot="title">
            {{ item.fullName }}
          </template>
          <router-link :to="{ name: 'StoreItem', params: { fullName: item.fullName } }">
            <span class="title">{{ item.fullName }}</span>
          </router-link>
        </a-tooltip>
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            <span style="font-size: 20px;" v-text="priceFormat">
            </span>
          </template>
          <strong class="actual-price !text-center" v-text="priceFormat" />
        </a-tooltip>
      </a-col>
    </a-row>
    <div class="stack-container">
      <a-badge :count="item.count" :overflowCount="10000" />
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { first as _first } from 'lodash'
  import { getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'GroupedItem',

    props: {
      item: {
        type: Object,
        required: true,
      },
    },

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      exchangeRate() {
        return _first(this.currentUser?.currency?.rates)?.rate || 1
      },
      currency() {
        return this.currentUser?.currency?.literal || 'USD'
      },
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      price() {
        return this.item.minPriceUsd
      },
      priceFormat() {
        return getCurrencyFormat(this.price * this.exchangeRate, this.cultureInfoName, this.currency)
      },
    },
  }
</script>
