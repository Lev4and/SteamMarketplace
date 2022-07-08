<template>
  <div class="on-imported-event-item event-item">
    <span>Пользователь <a href="#"><strong v-text="eventData.userName"/></a> добавил товар</span>
    <img class="image" :src="eventData.item.steamImg" />
    <a-tooltip placement="topLeft">
      <template slot="title">
        {{ eventData.item.fullName }}
      </template>
      <router-link :to="{ name: 'StoreItem', params: { fullName: eventData.item.fullName } }">
        <strong>{{ eventData.item.fullName }}</strong>
      </router-link>
    </a-tooltip>
  </div>
</template>

<script>
  export default {
    name: 'OnImportedEventItem',

    props: {
      eventData: {
        type: Object,
        required: true,
      },
    },
  }
</script>
