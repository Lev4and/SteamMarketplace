<template>
  <div class="my-purchase skin-item">
    <a-row :gutter="[0,12]">
      <a-col :span="24" class="image-container">
        <img :src="item.image.steamImg" />
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
        <a-tooltip placement="topLeft">
          <template slot="title">
            {{ info }}
          </template>
          <span class="info">{{ info }}</span>
        </a-tooltip>
      </a-col>
      <a-col :span="24">
        <div class="purchase-info">
          <div class="seller">
            <a-tooltip placement="topLeft">
              <template slot="title">
                <span>Продавец: {{ seller }}</span>
              </template>
              <a-icon type="user" />
              <span class="seller-title" v-text="seller" />
            </a-tooltip>
          </div>
          <div class="purchase-at">
            <a-tooltip placement="topLeft">
              <template slot="title">
                <span>Куплено в: {{ purchaseAtFormat }}</span>
              </template>
              <a-icon type="clock-circle" />
              <span class="purchase-at-time" v-text="purchaseAtFormat" />
            </a-tooltip>
          </div>
        </div>
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            <span class="text-[20px]" v-text="priceFormat">
            </span>
          </template>
          <strong class="actual-price !text-center" v-text="priceFormat" />
        </a-tooltip>
      </a-col>
    </a-row>
    <div class="stickers-container">
      <template v-for="sticker in stickers">
        <div :key="sticker.id" class="sticker">
          <a-tooltip>
            <template slot="title">
              {{ sticker.nested.fullName }}
            </template>
            <router-link :to="{ name: 'StoreItem', params: { fullName: sticker.nested.fullName } }">
              <img :src="sticker.nested.image.steamImg" class="sticker-image" />
            </router-link>
          </a-tooltip>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
  import moment from 'moment'
  import { mapGetters } from 'vuex'
  import { first as _first, join as _join, filter as _filter } from 'lodash'
  import { getNumberFormat, getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'MyPurchaseItem',

    props: {
      purchase: {
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
        return this.purchase.priceUsd
      },
      priceFormat() {
        return getCurrencyFormat(this.price * this.exchangeRate, this.cultureInfoName, this.currency)
      },
      purchaseAtFormat() {
        moment.locale(this.cultureInfoName)
        return moment.parseZone(this.purchase.purchaseAt).utc(true).local().format('LLLL')
      },
      item() {
        return this.purchase.sale.item
      },
      float() {
        return this.item.float ? getNumberFormat(this.item.float, 6, this.cultureInfoName) : ''
      },
      rarity() {
        return this.item.rarity?.name?.toUpperCase() || ''
      },
      quality() {
        return this.item.quality?.name?.toUpperCase() || ''
      },
      info() {
        return _join(_filter([this.rarity, this.quality, this.float], (info) => info), ' / ') || 'Нет информации'
      },
      seller() {
        return this.purchase.sale.seller.userName
      },
      stickers() {
        return this.item.itemNesteds || []
      },
    },
  }
</script>
