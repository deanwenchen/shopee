<template>
  <div class="bottom-bar-wrapper">
    <div class="bottom-bar">
      <!-- Shadow -->
      <div class="bar-shadow"></div>

      <!-- Like Button -->
      <button class="action-btn like-btn" @click="handleLike">
      <svg
        v-if="isLiked"
        viewBox="0 0 24 24"
        fill="#ff5790"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"
        />
      </svg>
      <svg
        v-else
        viewBox="0 0 24 24"
        fill="none"
        stroke="#666"
        stroke-width="2"
        xmlns="http://www.w3.org/2000/svg"
      >
        <path
          d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"
        />
      </svg>
    </button>

    <!-- Add to Cart Button -->
    <button class="action-btn cart-btn" @click="handleAddToCart">
      Add to cart
    </button>

    <!-- Buy Now Button -->
    <button class="action-btn buy-btn" @click="handleBuyNow">
      Buy now
    </button>

    <!-- Home Indicator -->
    <div class="home-indicator"></div>
  </div>
</div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits } from 'vue';

interface Props {
  productId?: number | string;
  isLiked?: boolean;
}

interface Emits {
  (e: 'like', isLiked: boolean): void;
  (e: 'add-to-cart'): void;
  (e: 'buy-now'): void;
}

const props = withDefaults(defineProps<Props>(), {
  isLiked: false,
});

const emit = defineEmits<Emits>();

const localIsLiked = ref(props.isLiked);

const handleLike = () => {
  localIsLiked.value = !localIsLiked.value;
  emit('like', localIsLiked.value);
};

const handleAddToCart = () => {
  emit('add-to-cart');
};

const handleBuyNow = () => {
  emit('buy-now');
};
</script>

<style scoped>
.bottom-bar-wrapper {
  position: fixed;
  bottom: 0;
  left: 0;
  right: 0;
  z-index: 100;
  height: 84px;
  background: #fff;
  display: flex;
  justify-content: center;
}

.bottom-bar {
  width: 375px;
  height: 84px;
  background: #fff;
  box-shadow: 0px -1px 1px 0px rgba(0, 0, 0, 0.16);
  display: flex;
  align-items: center;
  padding: 10px 20px 9px;
  position: relative;
}

.bar-shadow {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  height: 1px;
  background: rgba(0, 0, 0, 0.1);
}

.action-btn {
  border: none;
  border-radius: 11px;
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 300;
  font-size: 16px;
  cursor: pointer;
  transition: transform 0.15s ease, opacity 0.15s ease;
}

.action-btn:active {
  transform: scale(0.96);
}

.like-btn {
  width: 48px;
  height: 44px;
  background: #f9f9f9;
  display: flex;
  align-items: center;
  justify-content: center;
  flex-shrink: 0;
}

.like-btn svg {
  width: 24px;
  height: 24px;
}

.cart-btn {
  flex: 1;
  margin: 0 12px;
  height: 44px;
  background: #202020;
  color: #f3f3f3;
  line-height: 25px;
}

.buy-btn {
  flex: 1.5;
  height: 44px;
  background: #004cff;
  color: #f3f3f3;
  line-height: 25px;
}

.home-indicator {
  position: absolute;
  bottom: 9px;
  left: 50%;
  transform: translateX(-50%);
  width: 134px;
  height: 5px;
  background: #000;
  border-radius: 34px;
}
</style>
