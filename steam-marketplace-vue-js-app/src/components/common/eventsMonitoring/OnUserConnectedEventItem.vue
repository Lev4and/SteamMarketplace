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
