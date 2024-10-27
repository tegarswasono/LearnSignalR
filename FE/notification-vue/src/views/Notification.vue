<script setup lang="ts">
  import { ref, onMounted } from "vue";
  import * as SignalR from "@microsoft/signalr";

  const notifications = ref<String[]>([]);
  onMounted(() => {
    const connection = new SignalR.HubConnectionBuilder()
      .withUrl("https://localhost:7256/notificationHub")
      .build();

    connection.on("ReceiveNotification", (message: string) => {
      notifications.value.push(message);
    });

    connection.start().catch(err => console.error("Connection error: ", err));
  });
</script>
<template>
  <v-container>
    <v-alert
      v-for="(notif, index) in notifications"
      :key="index"
      type="info"
      dismissable
    >
      {{ notif }}
    </v-alert>
  </v-container>
</template>
<style scoped>
  v-alert {
    margin-bottom: 10px;
  }
</style>
