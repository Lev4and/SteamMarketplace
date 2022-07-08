<template>
  <div id="skins">
    <a-result v-if="!isStarted" status="warning" title="Вы действительно хотите начать импорт скинов из CS.Money?">
      <template #extra>
        <a-button key="console" type="primary" @click="onClickStart">
          Начать
        </a-button>
      </template>
    </a-result>
    <skins-import v-else />
  </div>
</template>

<script>
  import SkinsImport from '@/components/skins/SkinsImport'

  export default {
    name: 'Skins',

    components: {
      SkinsImport,
    },

    data: () => ({
      isStarted: false,
    }),

    methods: {
      onClickStart() {
        this.isStarted = true
      },
    },
  }
</script>
