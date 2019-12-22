<template>
  <v-app>
    <v-content>
      <v-container fluid>
        <router-view />
      </v-container>
    </v-content>
    <v-footer app />
  </v-app>
</template>

<script>

export default {
  name: 'App',
  components: {
  },
  data: () => ({
  }),
  computed: {
  },
  methods: {
  }
}
</script>
