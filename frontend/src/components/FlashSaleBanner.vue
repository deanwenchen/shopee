<template>
  <div class="flash-sale-banner">
    <!-- Banner Background -->
    <div class="banner-content">
      <!-- Timer Icon -->
      <div class="timer-icon-wrapper">
        <img src="/assets/figma/clock-icon.svg" alt="Flash Sale" class="timer-icon" />
      </div>

      <!-- Countdown Timer -->
      <div class="countdown-timer">
        <span class="time-segment">{{ hours }}</span>
        <span class="time-colon">:</span>
        <span class="time-segment">{{ minutes }}</span>
        <span class="time-colon">:</span>
        <span class="time-segment">{{ seconds }}</span>
      </div>

      <!-- Discount Badge -->
      <div class="discount-badge">
        -{{ discountPercent }}%
      </div>
    </div>

    <!-- Progress Bar (Optional - shows how much has been sold) -->
    <div v-if="soldPercent !== undefined" class="progress-section">
      <div class="progress-bar">
        <div class="progress-fill" :style="{ width: `${soldPercent}%` }"></div>
      </div>
      <p class="progress-text">
        Already sold: <span class="sold-count">{{ soldCount }}/{{ totalStock }}</span>
      </p>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, onUnmounted, defineProps } from 'vue';

interface Props {
  saleEndTime: string; // ISO date string
  discountPercent?: number;
  soldCount?: number;
  totalStock?: number;
}

const props = withDefaults(defineProps<Props>(), {
  discountPercent: 20,
  soldCount: 0,
  totalStock: 0,
});

const timeLeft = ref({
  hours: 0,
  minutes: 0,
  seconds: 0,
});

let timerInterval: ReturnType<typeof setInterval> | null = null;

const calculateTimeLeft = () => {
  const endTime = new Date(props.saleEndTime).getTime();
  const now = new Date().getTime();
  const difference = endTime - now;

  if (difference > 0) {
    timeLeft.value = {
      hours: Math.floor((difference / (1000 * 60 * 60)) % 24),
      minutes: Math.floor((difference / 1000 / 60) % 60),
      seconds: Math.floor((difference / 1000) % 60),
    };
  } else {
    // Sale ended
    timeLeft.value = { hours: 0, minutes: 0, seconds: 0 };
  }
};

const hours = computed(() => String(timeLeft.value.hours).padStart(2, '0'));
const minutes = computed(() => String(timeLeft.value.minutes).padStart(2, '0'));
const seconds = computed(() => String(timeLeft.value.seconds).padStart(2, '0'));

const soldPercent = computed(() => {
  if (!props.totalStock || props.totalStock === 0) return 0;
  return Math.min(100, Math.round((props.soldCount / props.totalStock) * 100));
});

onMounted(() => {
  calculateTimeLeft();
  timerInterval = setInterval(calculateTimeLeft, 1000);
});

onUnmounted(() => {
  if (timerInterval) {
    clearInterval(timerInterval);
  }
});
</script>

<style scoped>
.flash-sale-banner {
  display: flex;
  flex-direction: column;
  gap: 12px;
  margin-bottom: 16px;
}

.banner-content {
  display: flex;
  align-items: center;
  gap: 12px;
}

.timer-icon-wrapper {
  display: flex;
  align-items: center;
  justify-content: center;
}

.timer-icon {
  width: 20px;
  height: 20px;
}

.countdown-timer {
  display: flex;
  align-items: center;
  gap: 6px;
}

.time-segment {
  background: #ffebeb;
  color: #202020;
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 17px;
  padding: 4px 10px;
  border-radius: 7px;
  min-width: 32px;
  text-align: center;
  letter-spacing: -0.17px;
}

.time-colon {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 17px;
  color: #202020;
  margin-right: 2px;
}

.discount-badge {
  background: linear-gradient(135deg, #ff5790 0%, #f81140 100%);
  color: #fff;
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 13px;
  padding: 4px 10px;
  border-radius: 5px;
  letter-spacing: -0.13px;
  margin-left: auto;
}

.progress-section {
  width: 100%;
}

.progress-bar {
  width: 100%;
  height: 6px;
  background: #f0f0f0;
  border-radius: 3px;
  overflow: hidden;
}

.progress-fill {
  height: 100%;
  background: linear-gradient(90deg, #ff5790 0%, #f81140 100%);
  border-radius: 3px;
  transition: width 0.3s ease;
}

.progress-text {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 12px;
  color: #666;
  margin: 8px 0 0 0;
  text-align: right;
}

.sold-count {
  font-weight: 600;
  color: #333;
}
</style>
