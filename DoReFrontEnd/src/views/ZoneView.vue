<template>
  <div>
    <DNSRecords />
  </div>
</template>
  
  <script>
import DNSRecords from "../components/Zone.vue"; // make sure the path is correct

export default {
  name: "ZoneView",
  components: {
    DNSRecords,
  },
};
</script>
  