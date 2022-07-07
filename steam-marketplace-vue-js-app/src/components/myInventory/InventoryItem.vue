<template>
  <div class="inventory-item skin-item">
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
      stickers() {
        return this.item.itemNesteds || []
      },
    },
  }
</script>
