<template>
  <div class="inventory-item">
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
    </a-row>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { join as _join, filter as _filter } from 'lodash'
  import { getNumberFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'InventoryItem',

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
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
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
    },
  }
</script>

<style scoped>
  .inventory-item {
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
</style>
