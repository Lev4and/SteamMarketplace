<template>
  <div class="grouped-item">
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
            <span style="font-size: 20px;" v-text="priceFormat">
            </span>
          </template>
          <strong class="actual-price" v-text="priceFormat" />
        </a-tooltip>
      </a-col>
    </a-row>
    <div class="stack-container">
      <a-badge :count="item.count" />
    </div>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
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
      currency() {
        return this.currentUser?.currency?.literal || 'USD'
      },
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      price() {
        return this.item.minPrice
      },
      priceFormat() {
        return getCurrencyFormat(this.price, this.cultureInfoName, this.currency)
      },
    },
  }
</script>

<style scoped>
  .grouped-item {
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
  .actual-price {
    display: block;
    font-size: 20px;
    text-align: center;
    color: rgb(249, 17, 85);
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
</style>
