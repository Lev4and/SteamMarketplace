<template>
  <div class="exchange-rates-item">
    <a-card :title="`${exchangeRate.currency}/USD`">
      <router-link slot="extra" :to="{ name: 'CurrencyExchangeRate', params: { literal: exchangeRate.currency } }">
        Больше
      </router-link>
      <a-row type="flex" align="middle" justify="space-between">
        <a-col :span="12">
          <strong class="webkit-text" v-text="currentRateFormat" />
        </a-col>
        <a-col :span="12">
          <a-row type="flex" align="middle" justify="end">
            <a-col>
              <strong class="webkit-text" :class="{ 'text-color-red': growth < 0, 'text-color-green': growth >= 0 }" v-text="growthFormat" />
            </a-col>
          </a-row>
        </a-col>
      </a-row>
    </a-card>
  </div>
</template>

<script>
  import { mapGetters } from 'vuex'
  import { last as _last, reverse as _reverse, take as _take } from 'lodash'
  import { getNumberFormat, getPercentFormat } from '@/services/utils/formatUtils'

  export default {
    name: 'ExchangeRateItem',

    props: {
      exchangeRate: {
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
      currentRate() {
        return _last(this.exchangeRate?.rates)?.rate || 1
      },
      currentRateFormat() {
        return getNumberFormat(this.currentRate, 1, this.cultureInfoName)
      },
      growth() {
        const rates = _take(_reverse(this.exchangeRate.rates), 2)
        return rates.length === 2 ? rates[0].rate / rates[1].rate - 1 : 1
      },
      growthFormat() {
        return getPercentFormat(this.growth, 2, this.cultureInfoName)
      },
    },
  }
</script>

<style scoped>
  .webkit-text {
    word-break: break-all;
    flex-grow: 0;
    display: -webkit-box;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
  }
  .text-color-red {
    color: red;
  }
  .text-color-green {
    color: green;
  }
</style>
