<template>
  <div class="my-purchase">
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
        <div class="purchase-info">
          <div class="seller">
            <a-tooltip>
              <template slot="title">
                <span>Продавец: {{ purchase.sale.seller.userName }}</span>
              </template>
              <a-icon type="user" />
              <span class="seller-title" v-text="purchase.sale.seller.userName" />
            </a-tooltip>
          </div>
          <div class="purchase-at">
            <a-tooltip>
              <template slot="title">
                <span>Куплено в: {{ purchase.purchaseAt }}</span>
              </template>
              <a-icon type="clock-circle" />
              <span class="purchase-at-time" v-text="purchase.purchaseAt" />
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
  import { mapGetters } from 'vuex'
  import { first as _first, join as _join, filter as _filter } from 'lodash'

  export default {
    name: 'MyPurchase',

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
        return this.purchase.price
      },
      priceFormat() {
        return new Intl.NumberFormat(this.cultureInfoName, { style: 'currency', currency: this.currency })
          .format(this.price * this.exchangeRate)
      },
      item() {
        return this.purchase.sale.item
      },
      float() {
        return this.item.float ? new Intl.NumberFormat(this.cultureInfoName, { maximumSignificantDigits: 6 })
          .format(this.item.float) : ''
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
    },
  }
</script>

<style scoped>
  .my-purchase {
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
  .purchase-info {
    display: flex;
    flex-direction: row;
    justify-content: space-between;
  }
  .purchase-info .seller,
  .purchase-info .purchase-at {
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
  .purchase-at .purchase-at-time {
    margin-left: 5px;
  }
  .actual-price {
    display: block;
    font-size: 20px;
    text-align: center;
    color: rgb(249, 17, 85);
  }
</style>
