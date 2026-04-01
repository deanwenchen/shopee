<template>
  <div class="reviews-section">
    <!-- Rating Summary -->
    <div class="rating-summary">
      <div class="average-rating">
        <span class="rating-number">{{ averageRating }}</span>
        <div class="stars">
          <svg
            v-for="i in 5"
            :key="i"
            viewBox="0 0 24 24"
            :fill="i <= Math.round(averageRating) ? '#FFB800' : '#E0E0E0'"
            class="star-icon"
          >
            <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z" />
          </svg>
        </div>
        <span class="total-reviews">{{ totalReviews }} reviews</span>
      </div>

      <!-- Rating Distribution -->
      <div class="rating-distribution">
        <div
          v-for="star in [5, 4, 3, 2, 1]"
          :key="star"
          class="rating-row"
        >
          <span class="star-label">{{ star }} ★</span>
          <div class="rating-bar">
            <div
              class="rating-bar-fill"
              :style="{ width: `${getRatingPercent(star)}%` }"
            ></div>
          </div>
          <span class="rating-count">{{ getRatingCount(star) }}</span>
        </div>
      </div>
    </div>

    <!-- Filter Tabs -->
    <div class="filter-tabs">
      <button
        v-for="filter in filters"
        :key="filter.value"
        class="filter-tab"
        :class="{ active: currentFilter === filter.value }"
        @click="currentFilter = filter.value"
      >
        {{ filter.label }}
        <span v-if="filter.count" class="filter-count">{{ filter.count }}</span>
      </button>
    </div>

    <!-- Reviews List -->
    <div class="reviews-list">
      <div
        v-for="review in filteredReviews"
        :key="review.id"
        class="review-item"
      >
        <div class="review-header">
          <img :src="review.userAvatar" :alt="review.userName" class="user-avatar" />
          <div class="user-info">
            <span class="user-name">{{ review.userName }}</span>
            <div class="review-meta">
              <div class="review-stars">
                <svg
                  v-for="i in 5"
                  :key="i"
                  viewBox="0 0 24 24"
                  :fill="i <= review.rating ? '#FFB800' : '#E0E0E0'"
                  class="star-icon"
                >
                  <path d="M12 2l3.09 6.26L22 9.27l-5 4.87 1.18 6.88L12 17.77l-6.18 3.25L7 14.14 2 9.27l6.91-1.01L12 2z" />
                </svg>
              </div>
              <span class="review-date">{{ review.date }}</span>
            </div>
          </div>
        </div>

        <p class="review-content">{{ review.content }}</p>

        <!-- Review Images -->
        <div v-if="review.images && review.images.length > 0" class="review-images">
          <img
            v-for="(image, index) in review.images"
            :key="index"
            :src="image"
            :alt="`Review image ${index + 1}`"
            class="review-image"
          />
        </div>

        <!-- Follow Up Review -->
        <div v-if="review.isFollowUp" class="follow-up-review">
          <div class="follow-up-label">Follow-up Review</div>
          <p>{{ review.followUpContent }}</p>
          <span class="follow-up-date">{{ review.followUpDate }}</span>
        </div>

        <!-- Helpful Count -->
        <div class="review-actions">
          <button class="helpful-btn">
            <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
              <path d="M14 9V5a3 3 0 0 0-3-3l-4 9v11h11.28a2 2 0 0 0 2-1.7l1.38-9a2 2 0 0 0-2-2.3zM7 22H4a2 2 0 0 1-2-2v-7a2 2 0 0 1 2-2h3" />
            </svg>
            Helpful ({{ review.helpfulCount || 0 }})
          </button>
        </div>
      </div>
    </div>

    <!-- Write Review Button -->
    <button class="write-review-btn" @click="$emit('write-review')">
      Write a Review
    </button>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, defineProps, defineEmits } from 'vue';

interface Review {
  id: number;
  userName: string;
  userAvatar: string;
  rating: number;
  content: string;
  images?: string[];
  date: string;
  isFollowUp?: boolean;
  followUpContent?: string;
  followUpDate?: string;
  helpfulCount?: number;
}

interface RatingDistribution {
  5: number;
  4: number;
  3: number;
  2: number;
  1: number;
}

interface Props {
  reviews: Review[];
  averageRating?: number;
  ratingDistribution?: RatingDistribution;
}

interface Emits {
  (e: 'write-review'): void;
  (e: 'filter-change', filter: string): void;
}

const props = withDefaults(defineProps<Props>(), {
  averageRating: 0,
  ratingDistribution: () => ({ 5: 0, 4: 0, 3: 0, 2: 0, 1: 0 }),
});

const emit = defineEmits<Emits>();

const currentFilter = ref('all');

const filters = computed(() => [
  { label: 'All', value: 'all', count: props.reviews.length },
  { label: 'With Photos', value: 'with_photos', count: props.reviews.filter(r => r.images?.length).length },
  { label: 'Follow-up', value: 'follow_up', count: props.reviews.filter(r => r.isFollowUp).length },
]);

const filteredReviews = computed(() => {
  switch (currentFilter.value) {
    case 'with_photos':
      return props.reviews.filter(r => r.images?.length);
    case 'follow_up':
      return props.reviews.filter(r => r.isFollowUp);
    default:
      return props.reviews;
  }
});

const totalReviews = computed(() => props.reviews.length);

const getRatingCount = (star: number): number => {
  return props.ratingDistribution?.[star as keyof RatingDistribution] || 0;
};

const getRatingPercent = (star: number): number => {
  const count = getRatingCount(star);
  if (totalReviews.value === 0) return 0;
  return (count / totalReviews.value) * 100;
};

watch(currentFilter, (newFilter) => {
  emit('filter-change', newFilter);
});
</script>

<style scoped>
.reviews-section {
  padding: 20px;
  background: #fff;
}

.rating-summary {
  margin-bottom: 24px;
}

.average-rating {
  text-align: center;
  padding-bottom: 16px;
  border-bottom: 1px solid #f0f0f0;
}

.rating-number {
  display: block;
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 48px;
  color: #000;
  line-height: 1;
}

.stars {
  display: flex;
  justify-content: center;
  gap: 4px;
  margin: 8px 0;
}

.star-icon {
  width: 20px;
  height: 20px;
}

.total-reviews {
  display: block;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #666;
  margin-top: 4px;
}

.rating-distribution {
  padding-top: 16px;
}

.rating-row {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 6px;
}

.star-label {
  font-family: 'Raleway', sans-serif;
  font-size: 13px;
  color: #666;
  width: 20px;
}

.rating-bar {
  flex: 1;
  height: 6px;
  background: #f0f0f0;
  border-radius: 3px;
  overflow: hidden;
}

.rating-bar-fill {
  height: 100%;
  background: linear-gradient(90deg, #ffb800 0%, #ff9500 100%);
  border-radius: 3px;
  transition: width 0.3s ease;
}

.rating-count {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 12px;
  color: #999;
  width: 24px;
  text-align: right;
}

.filter-tabs {
  display: flex;
  gap: 8px;
  margin-bottom: 20px;
  border-bottom: 1px solid #f0f0f0;
  padding-bottom: 12px;
}

.filter-tab {
  flex-shrink: 0;
  padding: 6px 14px;
  border-radius: 20px;
  background: #f9f9f9;
  border: none;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #666;
  cursor: pointer;
  transition: all 0.2s ease;
  display: flex;
  align-items: center;
  gap: 4px;
}

.filter-tab.active {
  background: #004bff;
  color: #fff;
}

.filter-count {
  font-size: 11px;
  opacity: 0.8;
}

.reviews-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.review-item {
  padding-bottom: 20px;
  border-bottom: 1px solid #f0f0f0;
}

.review-header {
  display: flex;
  gap: 12px;
  margin-bottom: 12px;
}

.user-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
}

.user-info {
  flex: 1;
}

.user-name {
  display: block;
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #000;
  margin-bottom: 4px;
}

.review-meta {
  display: flex;
  align-items: center;
  gap: 12px;
}

.review-stars {
  display: flex;
  gap: 2px;
}

.review-stars .star-icon {
  width: 14px;
  height: 14px;
}

.review-date {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 12px;
  color: #999;
}

.review-content {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 14px;
  line-height: 1.5;
  color: #333;
  margin: 0 0 12px 0;
}

.review-images {
  display: flex;
  gap: 8px;
  margin-bottom: 12px;
  overflow-x: auto;
}

.review-image {
  width: 80px;
  height: 80px;
  border-radius: 8px;
  object-fit: cover;
  flex-shrink: 0;
}

.follow-up-review {
  background: #f9f9f9;
  padding: 12px;
  border-radius: 8px;
  margin-bottom: 12px;
}

.follow-up-label {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 12px;
  color: #666;
  margin-bottom: 8px;
}

.follow-up-review p {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  line-height: 1.5;
  color: #333;
  margin: 0 0 6px 0;
}

.follow-up-date {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 11px;
  color: #999;
}

.review-actions {
  display: flex;
  gap: 12px;
}

.helpful-btn {
  display: flex;
  align-items: center;
  gap: 4px;
  padding: 6px 12px;
  border-radius: 20px;
  background: #f9f9f9;
  border: none;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 12px;
  color: #666;
  cursor: pointer;
  transition: all 0.2s ease;
}

.helpful-btn:hover {
  background: #f0f0f0;
}

.helpful-btn svg {
  width: 14px;
  height: 14px;
}

.write-review-btn {
  width: 100%;
  padding: 14px;
  margin-top: 20px;
  border-radius: 12px;
  background: #004bff;
  border: none;
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 600;
  font-size: 16px;
  color: #fff;
  cursor: pointer;
  transition: transform 0.15s ease;
}

.write-review-btn:active {
  transform: scale(0.98);
}
</style>
