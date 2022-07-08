<template>
  <div class="cs-money-item skin-item">
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
        <a-tooltip placement="topLeft">
          <template slot="title">
            {{ info }}
          </template>
          <span class="info">{{ info }}</span>
        </a-tooltip>
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            <span class="text-[20px]">{{ currentPrice }}
              <sup v-if="overprice !== 1" class="original-price">
                <small v-text="originalPrice" />
              </sup>
            </span>
          </template>
          <strong class="actual-price" v-text="currentPrice" />
        </a-tooltip>
      </a-col>
    </a-row>
    <div class="stack-container">
      <a-badge :count="stackSize" />
    </div>
    <div class="stickers-container">
      <template v-for="(sticker, i) in stickers">
        <div :key="i" class="sticker">
          <a-tooltip>
            <template slot="title">
              {{ sticker.name }}
            </template>
            <router-link :to="{ name: 'StoreItem', params: { fullName: sticker.name } }">
              <img :src="sticker.img" class="sticker-image" />
            </router-link>
          </a-tooltip>
        </div>
      </template>
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { first as _first, filter as _filter, join as _join } from 'lodash'
  import { getNumberFormat, getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'CSMoneyItem',

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
        return this.item.price
      },
      overprice() {
        return (100 + (this.item.overprice || 0)) / 100
      },
      currentPrice() {
        return getCurrencyFormat(this.price * this.exchangeRate, this.cultureInfoName, this.currency)
      },
      originalPrice() {
        return getCurrencyFormat(this.price * this.overprice * this.exchangeRate, this.cultureInfoName, this.currency)
      },
      stickers() {
        return _filter(this.item.stickers || [], (sticker) => sticker)
      },
      stackSize() {
        return this.item.stackSize || 0
      },
      float() {
        return this.item.float ? getNumberFormat(this.item.float, 6, this.cultureInfoName) : ''
      },
      rarity() {
        return this.item.rarity?.toUpperCase() || ''
      },
      quality() {
        return this.item.quality?.toUpperCase() || ''
      },
      info() {
        return _join(_filter([this.rarity, this.quality, this.float], (info) => info), ' / ') || 'Нет информации'
      },
    },
  }
</script>
