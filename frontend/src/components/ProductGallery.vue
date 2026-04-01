<template>
  <div class="product-gallery">
    <!-- Main Image Carousel -->
    <div
      class="carousel-container"
      @touchstart="handleTouchStart"
      @touchmove="handleTouchMove"
      @touchend="handleTouchEnd"
    >
      <div class="carousel-slide" :style="{ transform: `translateX(-${currentIndex * 100}%)` }">
        <div
          v-for="(image, index) in images"
          :key="index"
          class="carousel-item"
        >
          <img :src="image" :alt="`Product image ${index + 1}`" />
        </div>
      </div>

      <!-- Dots Indicator -->
      <div class="carousel-dots">
        <div
          v-for="(_, index) in images"
          :key="index"
          class="dot"
          :class="{ active: index === currentIndex }"
        />
      </div>

      <!-- Like Button (Floating) -->
      <button class="like-button" @click="handleLike">
        <svg viewBox="0 0 24 24" fill="none" xmlns="http://www.w3.org/2000/svg">
          <path
            d="M12 21.35l-1.45-1.32C5.4 15.36 2 12.28 2 8.5 2 5.42 4.42 3 7.5 3c1.74 0 3.41.81 4.5 2.09C13.09 3.81 14.76 3 16.5 3 19.58 3 22 5.42 22 8.5c0 3.78-3.4 6.86-8.55 11.54L12 21.35z"
            :fill="isLiked ? '#ff5790' : 'none'"
            :stroke="isLiked ? '#ff5790' : '#666'"
            stroke-width="2"
          />
        </svg>
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, defineProps, defineEmits, onMounted, onUnmounted } from 'vue';

interface Props {
  images: string[];
  autoplay?: boolean;
  autoplayInterval?: number;
}

interface Emits {
  (e: 'like', isLiked: boolean): void;
  (e: 'slide-change', index: number): void;
}

const props = withDefaults(defineProps<Props>(), {
  autoplay: true,
  autoplayInterval: 3000,
});

const emit = defineEmits<Emits>();

const currentIndex = ref(0);
const isLiked = ref(false);
let autoplayTimer: ReturnType<typeof setInterval> | null = null;

// Auto-play functionality
const startAutoplay = () => {
  if (props.autoplay) {
    autoplayTimer = setInterval(() => {
      nextSlide();
    }, props.autoplayInterval);
  }
};

const stopAutoplay = () => {
  if (autoplayTimer) {
    clearInterval(autoplayTimer);
    autoplayTimer = null;
  }
};

// Swipe handling
let touchStartX = 0;
let touchEndX = 0;

const handleTouchStart = (e: TouchEvent) => {
  touchStartX = e.touches[0].clientX;
};

const handleTouchMove = (e: TouchEvent) => {
  touchEndX = e.touches[0].clientX;
};

const handleTouchEnd = () => {
  const diff = touchStartX - touchEndX;
  const threshold = 50;

  if (Math.abs(diff) > threshold) {
    if (diff > 0) {
      // Swipe left - next
      nextSlide();
    } else {
      // Swipe right - prev
      prevSlide();
    }
    // Reset autoplay after manual swipe
    stopAutoplay();
    startAutoplay();
  }
};

const nextSlide = () => {
  if (currentIndex.value < props.images.length - 1) {
    currentIndex.value++;
  } else {
    currentIndex.value = 0; // Loop back to first image
  }
  emit('slide-change', currentIndex.value);
};

const prevSlide = () => {
  if (currentIndex.value > 0) {
    currentIndex.value--;
  } else {
    currentIndex.value = props.images.length - 1; // Loop to last image
  }
  emit('slide-change', currentIndex.value);
};

const goToSlide = (index: number) => {
  currentIndex.value = index;
  emit('slide-change', currentIndex.value);
};

const handleLike = () => {
  isLiked.value = !isLiked.value;
  emit('like', isLiked.value);
};

onMounted(() => {
  startAutoplay();
});

onUnmounted(() => {
  stopAutoplay();
});
</script>

<style scoped>
.product-gallery {
  position: relative;
  width: 100%;
  overflow: visible;
}

.carousel-container {
  position: relative;
  width: 100%;
  height: 439px;
  overflow: hidden;
  background: #f5f5f5;
}

.carousel-slide {
  display: flex;
  transition: transform 0.3s ease-out;
  height: 100%;
}

.carousel-item {
  min-width: 100%;
  height: 100%;
}

.carousel-item img {
  width: 100%;
  height: 100%;
  object-fit: cover; /* Fill container without shrinking */
}

.carousel-dots {
  position: absolute;
  bottom: 16px;
  left: 50%;
  transform: translateX(-50%);
  display: flex;
  gap: 10px;
  z-index: 10;
}

.dot {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: transparent;
  border: 1px solid rgba(0, 0, 0, 0.3);
  cursor: pointer;
  transition: all 0.2s ease;
}

.dot.active {
  background: #004bff;
  border-color: #004bff;
  width: 40px;
  border-radius: 5px;
}

.like-button {
  position: absolute;
  top: 16px;
  right: 16px;
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: rgba(255, 255, 255, 0.9);
  border: none;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 10;
  transition: transform 0.2s ease;
}

.like-button:active {
  transform: scale(0.9);
}

.like-button svg {
  width: 24px;
  height: 24px;
}
</style>
