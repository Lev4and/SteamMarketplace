<template>
  <div class="on-registered-event-item event-item">
    <span>Зарегистрировался новый пользователь под никнеймом <a href="#"><strong v-text="eventData.userName"/></a></span>
  </div>
</template>

<script>
  export default {
    name: 'OnRegisteredEventItem',

    props: {
      eventData: {
        type: Object,
        required: true,
      },
    },
  }
</script>
