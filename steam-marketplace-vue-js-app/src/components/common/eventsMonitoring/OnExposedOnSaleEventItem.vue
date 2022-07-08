<template>
  <div class="on-exposed-on-sale-event-item event-item">
    <span>Пользователь <a href="#"><strong v-text="sellerName"/></a> выставил на продажу товар</span>
    <img class="image" :src="itemImage" />
    <router-link :to="{ name: 'StoreItem', params: { fullName: itemFullName } }">
      <span><strong v-text="itemFullName"/> за <strong class="actual-price" v-text="priceFormat" /></span>
    </router-link>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { first as _first } from 'lodash'
  import { getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'OnExposedOnSaleEventItem',

    props: {
      eventData: {
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
        return this.eventData?.sale?.priceUsd || 0
      },
      priceFormat() {
        return getCurrencyFormat(this.price * this.exchangeRate, this.cultureInfoName, this.currency)
      },
      sellerName() {
        return this.eventData?.seller?.userName || ''
      },
      itemImage() {
        return this.eventData?.item?.itemSteamImage || ''
      },
      itemFullName() {
        return this.eventData?.item?.itemFullName || ''
      },
    },
  }
</script>
