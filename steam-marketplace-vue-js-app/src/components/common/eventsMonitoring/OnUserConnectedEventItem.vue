<template>
  <div class="on-user-connected-event-item event-item">
    <span>Пользователь <a href="#"><strong v-text="eventData.userName"/></a> вошёл в систему</span>
  </div>
</template>

<script>
  export default {
    name: 'OnUserConnectedEventItem',

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
