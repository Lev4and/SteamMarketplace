<template>
  <div id="events-monitoring">
    <div class="container">
      <a-row :gutter="[0,8]">
        <template v-for="(event, i) in events">
          <template v-if="event.type === 'imported' || event.type === 'itemimported'">
            <a-col :key="i" :span="24">
              <on-imported-event-item :eventData="event.data" />
            </a-col>
          </template>
          <template v-if="event.type === 'userconnected'">
            <a-col :key="i" :span="24">
              <on-user-connected-event-item :eventData="event.data" />
            </a-col>
          </template>
          <template v-if="event.type === 'userregistered'">
            <a-col :key="i" :span="24">
              <on-registered-event-item :eventData="event.data" />
            </a-col>
          </template>
          <template v-if="event.type === 'itempurchased' || event.type === 'certainitempurchased'">
            <a-col :key="i" :span="24">
              <on-purchased-event-item :eventData="event.data" />
            </a-col>
          </template>
          <template v-if="event.type === 'userdisconnected'">
            <a-col :key="i" :span="24">
              <on-user-disconnected-event-item :eventData="event.data" />
            </a-col>
          </template>
          <template v-if="event.type === 'itemexposedonsale' || event.type === 'certainitemexposedonsale'">
            <a-col :key="i" :span="24">
              <on-exposed-on-sale-event-item :eventData="event.data" />
            </a-col>
          </template>
        </template>
      </a-row>
      <a-empty v-if="events.length === 0" />
    </div>
  </div>
</template>

<script>
  import OnImportedEventItem from './eventsMonitoring/OnImportedEventItem'
  import OnUserConnectedEventItem from './eventsMonitoring/OnUserConnectedEventItem'
  import OnRegisteredEventItem from './eventsMonitoring/OnRegisteredEventItem'
  import OnPurchasedEventItem from './eventsMonitoring/OnPurchasedEventItem'
  import OnUserDisconnectedEventItem from './eventsMonitoring/OnUserDisconnectedEventItem'
  import OnExposedOnSaleEventItem from './eventsMonitoring/OnExposedOnSaleEventItem'

  export default {
    name: 'EventsMonitoring',

    components: {
      OnImportedEventItem,
      OnUserConnectedEventItem,
      OnRegisteredEventItem,
      OnPurchasedEventItem,
      OnUserDisconnectedEventItem,
      OnExposedOnSaleEventItem,
    },

    props: {
      events: {
        type: Array,
        required: true,
      },
    },
  }
</script>

<style>
  .list-events-item {
    transition: all 1s;
    display: inline-block;
    margin-right: 10px;
  }
  .list-events-enter, .list-events-leave-to {
    opacity: 0;
    transform: translateX(100px);
  }
  .list-events-leave-active {
    position: absolute;
  }
</style>

<style scoped>
  .container {
    overflow: auto;
    max-height: 400px;
    padding-right: 12px;
  }
</style>
