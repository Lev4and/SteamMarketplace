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
                <a-tooltip>
                  <template slot="title">
                    {{ fullName }}
                  </template>
                  <strong class="full-name" v-text="fullName" />
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
                      <a-slider :value="float" :min="0" :max="1" :step="0.000001" />
                    </a-col>
                    <a-col :span="24">
                      <a-row type="flex" justify="space-between">
                        <a-col>
                          <span v-text="'Float'" />
                        </a-col>
                        <a-col>
                          <span v-text="floatFormat" />
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
      item: null,
    }),

    computed: {
      ...mapGetters({
        currentUser: 'auth/currentUser',
      }),
      cultureInfoName() {
        return this.currentUser?.currency?.cultureInfoName || 'us-US'
      },
      fullName() {
        return this.$route.params.fullName
      },
      image() {
        return this.item?.image?.steamImg || ''
      },
      float() {
        return this.item?.float || 0
      },
      floatFormat() {
        return getNumberFormat(this.float, 6, this.cultureInfoName)
      },
      addedAt() {
        return this.item?.addedAt || ''
      },
      addedAtFormat() {
        moment.locale(this.cultureInfoName)
        return moment.parseZone(this.addedAt).utc(true).local().format('LLLL')
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
          await this.loadItem()
        },
        immediate: true,
      },
    },

    methods: {
      async loadItem() {
        try {
          const response = await API.items.getItemByFullName(this.fullName)
          if (response.status.isSuccessful()) {
            this.item = response.result
          }
        } catch (exception) {
          this.$error(exception.message, 'Ошибка при загрузке')
        }
      },
    },
  }
</script>

<style scoped>
  #skinInfo .image-container {
    height: 350px;
    display: flex;
    align-items: center;
  }
  .image-container img {
    width: 100%;
    height: 100%;
    object-fit: contain;
  }
  #skinInfo .additional-info {
    width: 100%;
    padding: 20px;
    border: 1px solid black;
    border-radius: 5px;
  }
  .full-name {
    word-break: break-all;
    flex-grow: 0;
    display: -webkit-box;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
    font-size: 22px;
  }
</style>
