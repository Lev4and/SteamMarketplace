<template>
  <div id="cSMoneyMonitoring">
    <events-monitoring :events="events" />
  </div>
</template>

<script>
  import monitoring from '@/services/mixins/monitoring'
  import EventsMonitoring from '@/components/common/EventsMonitoring'

  export default {
    name: 'CSMoneyMonitoring',

    mixins: [monitoring],

    components: {
      EventsMonitoring,
    },

    data: () => ({
      hubsNames: ['importOnline'],
    }),

    computed: {
      eventsNames() {
        return [
          {
            name: 'imported',
            matchUser: {
              hub: 'importOnline',
              user: this.currentUser,
            },
            matchGroup: {
              hub: 'importOnline',
              name: '',
            },
          },
        ]
      },
    },
  }
</script>

<style scoped>

</style>
