<template>
  <div class="my-sale skin-item">
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
        <div class="sale-info">
          <div class="buyer">
            <a-tooltip placement="topLeft">
              <template slot="title">
                <span>Покупатель: {{ buyer }}</span>
              </template>
              <a-icon type="user" />
              <span class="buyer-title" v-text="buyer" />
            </a-tooltip>
          </div>
          <div class="sold-at">
            <a-tooltip placement="topLeft">
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
      stickers() {
        return this.item.itemNesteds || []
      },
    },
  }
</script>
