<template>
  <div id="skinMonitoring">
    <events-monitoring :events="events" />
  </div>
</template>

<script>
  import monitoring from '@/services/mixins/monitoring'
  import EventsMonitoring from '@/components/common/EventsMonitoring'

  export default {
    name: 'SkinMonitoring',

    mixins: [monitoring],

    components: {
      EventsMonitoring,
    },

    data: () => ({
    hubsNames: ['salesOnline', 'purchasesOnline', 'importOnline'],
  }),

    computed: {
      fullName() {
        return this.$route.params.fullName
      },
      eventsNames() {
        return [
          {
            name: 'itemimported',
            matchUser: {
              hub: 'importOnline',
              user: this.currentUser,
            },
            matchGroup: {
              hub: 'importOnline',
              name: this.fullName,
            },
          },
          {
            name: 'certainitemexposedonsale',
            matchUser: {
              hub: 'salesOnline',
              user: this.currentUser,
            },
            matchGroup: {
              hub: 'salesOnline',
              name: this.fullName,
            },
          },
          {
            name: 'certainitempurchased',
            matchUser: {
              hub: 'purchasesOnline',
              user: this.currentUser,
            },
            matchGroup: {
              hub: 'purchasesOnline',
              name: this.fullName,
            },
          },
        ]
      },
    },
  }
</script>

<style scoped>

</style>
