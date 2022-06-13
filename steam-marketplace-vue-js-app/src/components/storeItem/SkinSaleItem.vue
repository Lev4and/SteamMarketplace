<template>
  <div class="skin-sale-item">
    <a-row :gutter="[0,12]">
      <a-col :span="24" class="image-container">
        <img :src="item.image.steamImg" />
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
        <div class="sale-info">
          <div class="seller">
            <a-tooltip>
              <template slot="title">
                <span>Продавец: {{ seller }}</span>
              </template>
              <a-icon type="user" />
              <span class="seller-title" v-text="seller" />
            </a-tooltip>
          </div>
          <div class="exposed-at">
            <a-tooltip>
              <template slot="title">
                <span>Выставлен в: {{ exposedAtFormat }}</span>
              </template>
              <a-icon type="clock-circle" />
              <span class="exposed-at-time" v-text="exposedAtFormat" />
            </a-tooltip>
          </div>
        </div>
      </a-col>
      <a-col :span="24">
        <a-tooltip>
          <template slot="title">
            <span style="font-size: 20px;" v-text="priceFormat">
            </span>
          </template>
          <strong class="actual-price" v-text="priceFormat" />
        </a-tooltip>
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import moment from 'moment'
  import { mapGetters } from 'vuex'
  import { find as _find, join as _join, filter as _filter } from 'lodash'
  import { getNumberFormat, getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'SkinSaleItem',

    props: {
      sale: {
        type: Object,
        required: true,
      },
    },

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      currency() {
        return this.currentUser?.currency?.literal || 'USD'
      },
      exchangeRate() {
        return _find(this.currentUser?.currency?.rates, (rate) => moment(rate.dateTime).isBefore(this.sale.exposedAt))?.rate || 1
      },
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      item() {
        return this.sale.item
      },
      price() {
        return this.sale.priceUsd
      },
      priceFormat() {
        return getCurrencyFormat(this.price * this.exchangeRate, this.cultureInfoName, this.currency)
      },
      exposedAtFormat() {
        moment.locale(this.cultureInfoName)
        return moment.parseZone(this.sale.exposedAt).utc(true).local().format('LLLL')
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
        return this.sale.seller?.userName
      },
    },
  }
</script>

<style scoped>
  .skin-sale-item {
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
  .sale-info {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
  }
  .sale-info .seller,
  .sale-info .exposed-at {
    width: 45%;
    font-size: 12px;
    word-break: break-all;
    flex-grow: 0;
    display: -webkit-box;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
  }
  .seller .seller-title,
  .exposed-at .exposed-at-time {
    margin-left: 5px;
  }
  .actual-price {
    display: block;
    font-size: 20px;
    text-align: center;
    color: rgb(249, 17, 85);
  }
</style>
