<template>
  <div class="on-imported-event-item event-item">
    <span>Пользователь <a href="#"><strong v-text="eventData.userName"/></a> добавил товар</span>
    <img class="image" :src="eventData.item.steamImg" />
    <a-tooltip>
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

<style scoped>
  .event-item {
    gap: 12px;
    height: 50px;
    display: flex;
    padding: 6px 8px;
    border-radius: 5px;
    flex-direction: row;
    align-items: center;
    border: 1px black solid;
  }
  .image {
    width: 30px;
    height: 30px;
    object-fit: contain;
    border: 1px black solid;
  }
  .webkit-text {
    word-break: break-all;
    flex-grow: 0;
    display: -webkit-box;
    overflow: hidden;
    text-overflow: ellipsis;
    -webkit-line-clamp: 1;
    -webkit-box-orient: vertical;
  }
</style>
