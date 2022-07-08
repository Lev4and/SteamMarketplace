<template>
  <div class="on-user-disconnected-event-item event-item">
    <span>Пользователь <a href="#"><strong v-text="eventData.userName"/></a> вышел из системы</span>
  </div>
</template>

<script>
  export default {
    name: 'OnUserDisconnectedEventItem',

    props: {
      eventData: {
        type: Object,
        required: true,
      },
    },
  }
</script>
