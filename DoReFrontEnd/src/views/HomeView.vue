<template>
  <div class="home">
    <img alt="Vue logo" src="../assets/logo.png">
    <HelloWorld msg="Welcome to Your Vue.js App"/>
    <h1>Home Page <InfoIcon>Information about the home page</InfoIcon></h1>
    <p>Here is some content on the home page. <InfoIcon>More details about this content</InfoIcon></p>
  </div>
  
</template>

<script>
// @ is an alias to /src
import HelloWorld from '@/components/HelloWorld.vue'
import InfoIcon from '@/components/InfoIcon.vue'

export default {
  name: 'HomeView',
  components: {
    HelloWorld,
    InfoIcon
  }
}
</script>

<style scoped>
.home {
  display: flex;
  flex-direction: column;
  align-items: center;
}
</style>
