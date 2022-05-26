<template>
  <div class="cs-money-item">
    <a-row :gutter="[0,12]">
      <a-col :span="24" class="image-container">
        <img :src="item.steamImg" />
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            {{ item.fullName }}
          </template>
          <span class="title">{{ item.fullName }}</span>
        </a-tooltip>
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            {{ info }}
          </template>
          <span class="info">{{ info }}</span>
        </a-tooltip>
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            <span style="font-size: 20px;">{{ currentPrice }}
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
            <img :src="sticker.img" class="sticker-image" />
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

<style scoped>
  .cs-money-item {
    padding: 10px;
    border-radius: 10px;
    border: 1px solid black;
  }
  .image-container {
    width: 100%;
    height: 130px;
    display: flex;
    align-items: center;
    vertical-align: middle;
  }
  .image-container img {
    margin: auto;
    max-width: 90%;
    max-height: 90%;
    object-fit: contain;
  }
  .title {
    word-break: break-all;
    flex-grow: 0;
    display: -webkit-box;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
  }
  .info {
    font-size: 12px;
    word-break: break-all;
    flex-grow: 0;
    display: -webkit-box;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
  }
  .actual-price {
    display: block;
    font-size: 20px;
    text-align: left;
    color: rgb(249, 17, 85);
  }
  .original-price::after {
    height: 3px;
    content: "";
    display: block;
    left: 0;
    position: absolute;
    top: calc(50% - 1px);
    transform: rotate(-2deg);
    background-color: red;
    width: 100%;
  }
  .stack-container {
    top: 15px;
    bottom: 0;
    left: 10px;
    width: 35px;
    display: flex;
    position: absolute;
    flex-direction: column;
  }
  .stickers-container {
    top: 10px;
    bottom: 0;
    right: 10px;
    width: 35px;
    display: flex;
    position: absolute;
    flex-direction: column;
  }
  .stickers-container .sticker {
    width: 100%;
    height: 35px;
    display: flex;
  }
  .sticker img {
    width: 100%;
    height: 100%;
    object-fit: contain;
  }
</style>
