<template>
  <div id="storeMonitoring">
    <events-monitoring :events="events" />
  </div>
</template>

<script>
  import monitoring from '@/services/mixins/monitoring'
  import EventsMonitoring from '@/components/common/EventsMonitoring'

  export default {
    name: 'StoreMonitoring',

    mixins: [monitoring],

    components: {
      EventsMonitoring,
    },
  }
</script>
