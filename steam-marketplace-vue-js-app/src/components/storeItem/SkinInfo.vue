<template>
  <div id="skinInfo">
    <a-row :gutter="[40,0]" type="flex" align="middle">
      <a-col :span="12">
        <div class="image-container">
          <img :src="image" />
        </div>
      </a-col>
      <a-col :span="12">
        <a-row :gutter="[0,17]">
          <a-col :span="24">
            <a-row :gutter="[0,5]">
              <a-col v-if="rarity" :span="24">
                <span v-text="rarity" />
              </a-col>
              <a-col :span="24">
                <a-tooltip placement="topLeft">
                  <template slot="title">
                    {{ fullName }}
                  </template>
                  <strong class="web-kit-text text-[22px]" v-text="fullName" />
                </a-tooltip>
              </a-col>
            </a-row>
          </a-col>
          <a-col :span="24">
            <div class="additional-info">
              <a-row :gutter="[0,0]">
                <a-col :span="24">
                  <a-row :gutter="[0,10]">
                    <a-col :span="24">
                      <a-slider :value="averageFloat" :min="0" :max="1" :step="0.000001" />
                    </a-col>
                    <a-col :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Средний Float'" />
                        </a-col>
                        <a-col>
                          <span v-text="averageFloatFormat" />
                        </a-col>
                      </a-row>
                    </a-col>
                  </a-row>
                </a-col>
                <a-col :span="24">
                  <a-divider />
                </a-col>
                <a-col :span="24">
                  <a-row :gutter="[0,10]">
                    <a-col v-if="type" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Тип'" />
                        </a-col>
                        <a-col>
                          <span v-text="type" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="collection" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Коллекция'" />
                        </a-col>
                        <a-col>
                          <span v-text="collection" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="rarity" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Редкость'" />
                        </a-col>
                        <a-col>
                          <span v-text="rarity" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="quality" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Качество'" />
                        </a-col>
                        <a-col>
                          <span v-text="quality" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="addedAtFormat" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Добавлен в'" />
                        </a-col>
                        <a-col>
                          <span v-text="addedAtFormat" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="countFormat" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Всего в системе'" />
                        </a-col>
                        <a-col>
                          <span v-text="countFormat" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="countOwnersFormat" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Количество владельцев'" />
                        </a-col>
                        <a-col>
                          <span v-text="countOwnersFormat" />
                        </a-col>
                      </a-row>
                    </a-col>
                    <a-col v-if="rarityValueFormat" :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Редкость в %'" />
                        </a-col>
                        <a-col>
                          <span v-text="rarityValueFormat" />
                        </a-col>
                      </a-row>
                    </a-col>
                  </a-row>
                </a-col>
              </a-row>
            </div>
          </a-col>
        </a-row>
      </a-col>
    </a-row>
  </div>
</template>

<script>
  import moment from 'moment'
  import { mapGetters } from 'vuex'
  import API from '@/api'
  import { getNumberFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'SkinInfo',

    data: () => ({
      extendedItem: null,
    }),

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      item() {
        return this.extendedItem?.item
      },
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      fullName() {
        return this.$route.params.fullName
      },
      image() {
        return this.item?.image?.steamImg || ''
      },
      averageFloat() {
        return this.extendedItem?.averageFloat || 0
      },
      averageFloatFormat() {
        return getNumberFormat(this.averageFloat, 6, this.cultureInfoName)
      },
      addedAt() {
        return this.extendedItem?.addedAt || ''
      },
      addedAtFormat() {
        moment.locale(this.cultureInfoName)
        return moment.parseZone(this.addedAt).utc(true).local().format('LLLL')
      },
      count() {
        return this.extendedItem?.count || 0
      },
      countFormat() {
        return getNumberFormat(this.count, 0, this.cultureInfoName)
      },
      countOwners() {
        return this.extendedItem?.countOwners || 0
      },
      countOwnersFormat() {
        return getNumberFormat(this.countOwners, 0, this.cultureInfoName)
      },
      rarityValue() {
        return this.extendedItem?.rarity || 0
      },
      rarityValueFormat() {
        return getNumberFormat(this.rarityValue, 6, this.cultureInfoName) + '%'
      },
      type() {
        return this.item?.type?.csMoneyId || 0
      },
      rarity() {
        return this.item?.rarity?.name || ''
      },
      collection() {
        return this.item?.collection?.name || ''
      },
      quality() {
        return this.item?.quality?.name || ''
      },
    },

    watch: {
      fullName: {
        async handler() {
          await this.loadExtendedItem()
        },
        immediate: true,
      },
    },

    methods: {
      async loadExtendedItem() {
        try {
          const response = await API.items.getExtendedInfo(this.fullName)
          if (response.status.isSuccessful) {
            this.extendedItem = response.result
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке')
        }
      },
    },
  }
</script>
