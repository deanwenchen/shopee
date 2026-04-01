<template>
  <div class="product-info">
    <!-- Price -->
    <div class="price-section">
      <span class="current-price">{{ formattedPrice }}</span>

      <!-- Original Price (if on sale) -->
      <span v-if="originalPrice" class="original-price">
        {{ formattedOriginalPrice }}
      </span>

      <!-- Discount Badge -->
      <span v-if="discountPercent" class="discount-badge">
        -{{ discountPercent }}%
      </span>

      <!-- Flash Sale Timer -->
      <div v-if="isOnSale && saleEndTime" class="flash-sale-timer">
        <img src="/assets/figma/clock-icon.svg" alt="Timer" class="timer-icon" />
        <div class="timer-segments">
          <span class="timer-segment">{{ hoursLeft }}</span>
          <span class="timer-colon">:</span>
          <span class="timer-segment">{{ minutesLeft }}</span>
          <span class="timer-colon">:</span>
          <span class="timer-segment">{{ secondsLeft }}</span>
        </div>
      </div>
    </div>

    <!-- Product Name -->
    <h1 class="product-name">{{ name }}</h1>

    <!-- Description -->
    <p v-if="description" class="product-description">
      {{ description }}
    </p>

    <!-- Rating & Reviews -->
    <div v-if="showRating" class="rating-section">
      <div class="stars">
        <svg
          v-for="i in 5"
          :key="i"
          viewBox="0 0 24 24"
          :fill="i <= rating ? '#FFB800' : '#E0E0E0'"
          class="star-icon"
        >
          <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z" />
        </svg>
      </div>
      <span class="rating-text">
        {{ rating }}/{{ maxRating }}
        <span v-if="reviewCount">({{ reviewCount }} reviews)</span>
      </span>
      <span v-if="salesCount" class="sales-count">
        | {{ salesCount }}+ sold
      </span>
    </div>

    <!-- Variations Selector Preview -->
    <div v-if="hasVariations" class="variations-preview">
      <h2 class="variations-title">Variations</h2>

      <!-- Selected Color -->
      <div v-if="selectedColor" class="variation-chip">
        {{ selectedColor }}
      </div>

      <!-- Selected Size -->
      <div v-if="selectedSize" class="variation-chip">
        {{ selectedSize }}
      </div>

      <!-- Expand Button -->
      <button class="expand-variations-btn" @click="$emit('expand-variations')">
        <img src="/assets/figma/arrow-icon-white.svg" alt="Expand" />
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, defineProps, defineEmits } from 'vue';

interface Props {
  name: string;
  description?: string;
  price: number;
  originalPrice?: number;
  isOnSale?: boolean;
  saleEndTime?: string; // ISO date string
  rating?: number;
  maxRating?: number;
  reviewCount?: number;
  salesCount?: number;
  hasVariations?: boolean;
  selectedColor?: string;
  selectedSize?: string;
  showRating?: boolean;
}

interface Emits {
  (e: 'expand-variations'): void;
}

const props = withDefaults(defineProps<Props>(), {
  description: '',
  isOnSale: false,
  rating: 0,
  maxRating: 5,
  reviewCount: 0,
  salesCount: 0,
  hasVariations: false,
  showRating: true,
});

defineEmits<Emits>();

// Computed properties
const formattedPrice = computed(() => {
  return `$${props.price.toFixed(2).replace('.', ',')}`;
});

const formattedOriginalPrice = computed(() => {
  if (!props.originalPrice) return '';
  return `$${props.originalPrice.toFixed(2).replace('.', ',')}`;
});

const discountPercent = computed(() => {
  if (!props.originalPrice || !props.isOnSale) return null;
  const discount = ((props.originalPrice - props.price) / props.originalPrice) * 100;
  return Math.round(discount);
});

// Timer state
const timeLeft = ref({
  hours: 0,
  minutes: 0,
  seconds: 0,
});

let timerInterval: ReturnType<typeof setInterval> | null = null;

const calculateTimeLeft = () => {
  if (!props.saleEndTime) return;

  const endTime = new Date(props.saleEndTime).getTime();
  const now = new Date().getTime();
  const difference = endTime - now;

  if (difference > 0) {
    timeLeft.value = {
      hours: Math.floor(difference / (1000 * 60 * 60)),
      minutes: Math.floor((difference / (1000 * 60 * 60)) % 60),
      seconds: Math.floor((difference / 1000) % 60),
    };
  } else {
    // Sale ended
    timeLeft.value = { hours: 0, minutes: 0, seconds: 0 };
  }
};

const hoursLeft = computed(() => String(timeLeft.value.hours).padStart(2, '0'));
const minutesLeft = computed(() => String(timeLeft.value.minutes).padStart(2, '0'));
const secondsLeft = computed(() => String(timeLeft.value.seconds).padStart(2, '0'));

onMounted(() => {
  if (props.isOnSale && props.saleEndTime) {
    calculateTimeLeft();
    timerInterval = setInterval(calculateTimeLeft, 1000);
  }
});

onUnmounted(() => {
  if (timerInterval) {
    clearInterval(timerInterval);
  }
});
</script>

<style scoped>
.product-info {
  padding: 20px;
  background: #fff;
}

.price-section {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 8px;
}

.current-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 26px;
  color: #000;
  letter-spacing: -0.26px;
}

.original-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 14px;
  color: #f1aeae;
  text-decoration: line-through;
  letter-spacing: 0.168px;
}

.discount-badge {
  background: linear-gradient(180deg, #ff5790 0%, #f81140 100%);
  color: #fff;
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 13px;
  padding: 2px 6px;
  border-radius: 0 5px 5px 0;
  letter-spacing: -0.13px;
}

.flash-sale-timer {
  display: flex;
  align-items: center;
  gap: 8px;
}

.timer-icon {
  width: 18px;
  height: 18px;
}

.timer-segments {
  display: flex;
  align-items: center;
  gap: 4px;
}

.timer-segment {
  background: #ffebeb;
  color: #202020;
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 17px;
  padding: 3px 8px;
  border-radius: 7px;
  min-width: 28px;
  text-align: center;
}

.timer-colon {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 17px;
  color: #202020;
}

.product-name {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 18px;
  color: #000;
  margin: 0 0 8px 0;
  letter-spacing: -0.18px;
}

.product-description {
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 400;
  font-size: 15px;
  line-height: 20px;
  color: #000;
  margin: 0 0 16px 0;
}

.rating-section {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 16px;
}

.stars {
  display: flex;
  gap: 2px;
}

.star-icon {
  width: 16px;
  height: 16px;
}

.rating-text {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #666;
}

.sales-count {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #999;
}

.variations-preview {
  display: flex;
  align-items: center;
  gap: 12px;
}

.variations-title {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 20px;
  color: #000;
  margin: 0;
  letter-spacing: -0.2px;
}

.variation-chip {
  background: #f9f9f9;
  border-radius: 4px;
  padding: 8px 16px;
  font-family: 'Raleway', sans-serif;
  font-weight: 500;
  font-size: 14px;
  color: #000;
  letter-spacing: -0.14px;
}

.expand-variations-btn {
  margin-left: auto;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: #004bff;
  border: none;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: transform 0.2s ease;
}

.expand-variations-btn:active {
  transform: scale(0.95);
}

.expand-variations-btn img {
  width: 20px;
  height: 20px;
}
</style>
