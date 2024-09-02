<template>
  <div class="info-icon" ref="infoIcon">
    <span @click="toggleInfo" class="icon">❔</span>
    <div v-if="showInfo" class="info-content" ref="infoContent" v-closable="{ handler: 'onClose', exclude: ['infoIcon', 'infoContent'] }">
      <slot></slot>
    </div>
  </div>
</template>

<script>
import { defineComponent, onMounted, onBeforeUnmount } from 'vue';

const closableDirective = {
  mounted(el, binding) {
    const handleOutsideClick = (e) => {
      const { handler, exclude } = binding.value;
      let clickedOnExcludedEl = false;
      exclude.forEach(refName => {
        const excludedEl = binding.instance.$refs[refName];
        if (excludedEl && excludedEl.contains(e.target)) {
          clickedOnExcludedEl = true;
        }
      });
      if (!el.contains(e.target) && !clickedOnExcludedEl) {
        binding.instance[handler]();
      }
    };
    setTimeout(() => {
      document.addEventListener('click', handleOutsideClick);
      document.addEventListener('touchstart', handleOutsideClick);
    }, 100); // Add a brief delay

    el._handleOutsideClick = handleOutsideClick; // Store the handler for removal
  },
  unmounted(el) {
    document.removeEventListener('click', el._handleOutsideClick);
    document.removeEventListener('touchstart', el._handleOutsideClick);
  }
};

export default defineComponent({
  name: 'InfoIcon',
  data() {
    return {
      showInfo: false,
    };
  },
  methods: {
    toggleInfo() {
      this.showInfo = !this.showInfo;
    },
    onClose() {
      this.showInfo = false;
    }
  },
  directives: {
    closable: closableDirective
  },
  mounted() {
    this.$nextTick(() => {
      if (this.showInfo) {
        this.$refs.infoContent.focus();
      }
    });
  }
});
</script>

<style scoped>
.info-icon {
  position: relative;
  display: inline-block;
  cursor: pointer;
}

.icon {
  font-size: 15px;
  color: #309e2d;
  padding-bottom: 30px;
  border: 50px;
}

.info-content {
  position: absolute;
  top: 25px;
  left: 0;
  background: rgb(255, 255, 255);
  border: 1px solid #ccc;
  border-radius: 15px;
  padding: 10px;
  width: 200px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  z-index: 1000;
  font-size: 14px;
}
</style>
