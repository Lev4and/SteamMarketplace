<template>
  <div class="my-sale">
    <a-row :gutter="[0,12]">
      <a-col :span="24" class="image-container">
        <img :src="item.image.steamImg" />
      </a-col>
      <a-col :span="24">
        <a-tooltip>
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
            {{ info }}
          </template>
          <span class="info">{{ info }}</span>
        </a-tooltip>
      </a-col>
      <a-col :span="24">
        <div class="sale-info">
          <div class="buyer">
            <a-tooltip>
              <template slot="title">
                <span>Покупатель: {{ buyer }}</span>
              </template>
              <a-icon type="user" />
              <span class="buyer-title" v-text="buyer" />
            </a-tooltip>
          </div>
          <div class="sold-at">
            <a-tooltip>
              <template slot="title">
                <span>Продано в: {{ soldAtFormat }}</span>
              </template>
              <a-icon type="clock-circle" />
              <span class="sold-at-time" v-text="soldAtFormat" />
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
  import { join as _join, filter as _filter } from 'lodash'
  import { getNumberFormat, getCurrencyFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'MySaleItem',

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
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      item() {
        return this.sale.item
      },
      price() {
        return this.sale.price
      },
      priceFormat() {
        return getCurrencyFormat(this.price, this.cultureInfoName, this.currency)
      },
      soldAtFormat() {
        moment.locale(this.cultureInfoName)
        return this.sale.soldAt ? moment.parseZone(this.sale.soldAt).utc(true).local().format('LLLL') : 'Не продано'
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
      buyer() {
        return this.sale.purchase?.buyer?.userName || 'Нет покупателя'
      },
    },
  }
</script>

<style scoped>
  .my-sale {
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
  .sale-info .buyer,
  .sale-info .sold-at {
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
  .buyer .buyer-title,
  .sold-at .sold-at-time {
    margin-left: 5px;
  }
  .actual-price {
    display: block;
    font-size: 20px;
    text-align: center;
    color: rgb(249, 17, 85);
  }
</style>
