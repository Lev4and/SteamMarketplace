import { mapGetters } from 'vuex'
import { forEach as _forEach, map as _map, filter as _filter, dropRight as _dropRight } from 'lodash'
import API from '@/api'

export default {
  props: {
    maxEventsLength: {
      type: Number,
      default: 25,
      required: false,
    },
  },

  data: () => ({
    events: [],
    hubsNames: ['salesOnline', 'importOnline'],
  }),

  computed: {
    ...mapGetters({
      currentUser: 'auth/currentUser',
    }),
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
        {
          name: 'userconnected',
          matchUser: {
            hub: 'usersOnline',
            user: this.currentUser,
          },
          matchGroup: {
            hub: 'usersOnline',
            name: '',
          },
        },
        {
          name: 'itempurchased',
          matchUser: {
            hub: 'purchasesOnline',
            user: this.currentUser,
          },
          matchGroup: {
            hub: 'purchasesOnline',
            name: '',
          },
        },
        {
          name: 'userregistered',
          matchUser: {
            hub: 'usersOnline',
            user: this.currentUser,
          },
          matchGroup: {
            hub: 'usersOnline',
            name: '',
          },
        },
        {
          name: 'userdisconnected',
          matchUser: {
            hub: 'usersOnline',
            user: this.currentUser,
          },
          matchGroup: {
            hub: 'usersOnline',
            name: '',
          },
        },
        {
          name: 'itemexposedonsale',
          matchUser: {
            hub: 'salesOnline',
            user: this.currentUser,
          },
          matchGroup: {
            hub: 'salesOnline',
            name: '',
          },
        },
      ]
    },
    eventsMatchUser() {
      return _filter(this.eventsNames, (eventName) => eventName.matchUser && !!eventName.matchUser.user)
    },
    eventsMatchGroup() {
      return _filter(this.eventsNames, (eventName) => eventName.matchGroup && !!eventName.matchGroup.name)
    },
  },

  watch: {
    currentUser: {
      async handler(newValue) {
        if (newValue) {
          await this.connect()
          await this.matchUsers()
          await this.matchGroups()
        } else await this.disconnect()
      },
      immediate: true,
    },
  },

  mounted() {
    _forEach(this.eventsNames, (eventName) => {
      document.addEventListener(eventName.name, this.onReceivedMessage)
    })
  },

  async beforeDestroy() {
    _forEach(this.eventsNames, (eventName) => {
      document.removeEventListener(eventName.name, this.onReceivedMessage)
    })
    await this.disconnect()
  },

  methods: {
    async connect() {
      await Promise.all(_map(this.hubsNames, (hubName) => {
        return API[hubName].connect()
      })).then((responses) => {
        console.log(responses)
      })
    },
    async matchUsers() {
      await Promise.all(_map(this.eventsMatchUser, (event) => {
        API[event.matchUser.hub].matchUser(event.matchUser.user)
      })).then((responses) => {
        console.log(responses)
      })
    },
    async matchGroups() {
      await Promise.all(_map(this.eventsMatchGroup, (event) => {
        API[event.matchGroup.hub].matchGroup(event.matchGroup.name)
      })).then((responses) => {
        console.log(responses)
      })
    },
    async unmatchGroups() {
      await Promise.all(_map(this.eventsMatchGroup, (event) => {
        API[event.matchGroup.hub].unmatchGroup(event.matchGroup.name)
      })).then((responses) => {
        console.log(responses)
      })
    },
    async disconnect() {
      await Promise.all(_map(this.hubsNames, (hubName) => {
        API[hubName].disconnect()
      })).then((responses) => {
        console.log(responses)
      })
    },
    onReceivedMessage(event) {
      if (this.events.length > this.maxEventsLength) this.events = _dropRight(this.events)
      this.events.unshift({ type: event?.type, data: event?.detail?.data?.()})
    },
  },
}