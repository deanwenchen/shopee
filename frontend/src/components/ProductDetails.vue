<template>
  <div class="product-details">
    <!-- Tabs -->
    <div class="tabs">
      <button
        v-for="tab in tabs"
        :key="tab.value"
        class="tab-btn"
        :class="{ active: activeTab === tab.value }"
        @click="activeTab = tab.value"
      >
        {{ tab.label }}
      </button>
    </div>

    <!-- Tab Content -->
    <div class="tab-content">
      <!-- Description Tab -->
      <div v-if="activeTab === 'description'" class="description-content">
        <div v-html="productDescription" class="rich-text"></div>

        <!-- Product Features -->
        <div class="features-section">
          <h3>Product Features</h3>
          <ul class="features-list">
            <li v-for="(feature, index) in features" :key="index">
              <span class="feature-icon">✓</span>
              {{ feature }}
            </li>
          </ul>
        </div>

        <!-- Material & Care -->
        <div class="material-care">
          <h3>Material & Care</h3>
          <div class="info-grid">
            <div class="info-item" v-if="material">
              <span class="info-label">Material:</span>
              <span class="info-value">{{ material }}</span>
            </div>
            <div class="info-item" v-if="care">
              <span class="info-label">Care:</span>
              <span class="info-value">{{ care }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- Specifications Tab -->
      <div v-if="activeTab === 'specifications'" class="specifications-content">
        <table class="specs-table">
          <tr v-for="(value, key) in specifications" :key="key">
            <td class="spec-label">{{ formatSpecLabel(key) }}</td>
            <td class="spec-value">{{ value }}</td>
          </tr>
        </table>
      </div>

      <!-- Reviews Tab (Preview) -->
      <div v-if="activeTab === 'reviews'" class="reviews-preview">
        <div class="reviews-header">
          <h3>Customer Reviews</h3>
          <span class="review-count">{{ totalReviews }} reviews</span>
        </div>

        <!-- Quick Rating Summary -->
        <div class="quick-rating">
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
        </div>

        <!-- Review Highlights -->
        <div class="review-highlights">
          <div v-for="review in highlightedReviews" :key="review.id" class="highlight-review">
            <div class="reviewer-info">
              <img :src="review.userAvatar" :alt="review.userName" class="avatar" />
              <div class="reviewer-name">{{ review.userName }}</div>
            </div>
            <p class="review-text">{{ review.content }}</p>
            <div class="review-rating">
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
          </div>
        </div>

        <button class="view-all-reviews-btn" @click="$emit('view-all-reviews')">
          View All {{ totalReviews }} Reviews
        </button>
      </div>
    </div>

    <!-- Delivery Information -->
    <div class="delivery-info">
      <h3>Delivery Information</h3>
      <div class="delivery-options">
        <div class="delivery-option">
          <div class="option-header">
            <span class="option-name">Standard Delivery</span>
            <span class="option-price">$3.00</span>
          </div>
          <span class="option-time">5-7 business days</span>
        </div>
        <div class="delivery-option express">
          <div class="option-header">
            <span class="option-name">Express Delivery</span>
            <span class="option-price">$12.00</span>
          </div>
          <span class="option-time">1-2 business days</span>
        </div>
      </div>
    </div>

    <!-- After Sales Service -->
    <div class="after-sales">
      <h3>After-Sales Service</h3>
      <ul class="after-sales-list">
        <li>
          <span class="icon">🔄</span>
          <span>30-day free returns</span>
        </li>
        <li>
          <span class="icon">🛡️</span>
          <span>1-year warranty</span>
        </li>
        <li>
          <span class="icon">💬</span>
          <span>24/7 customer support</span>
        </li>
      </ul>
    </div>
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
}

interface Props {
  description?: string;
  specifications?: Record<string, string>;
  material?: string;
  care?: string;
  features?: string[];
  reviews?: Review[];
  averageRating?: number;
}

interface Emits {
  (e: 'view-all-reviews'): void;
}

const props = withDefaults(defineProps<Props>(), {
  description: '',
  specifications: () => ({}),
  material: '',
  care: '',
  features: () => [],
  reviews: () => [],
  averageRating: 0,
});

defineEmits<Emits>();

const activeTab = ref('description');

const tabs = [
  { label: 'Description', value: 'description' },
  { label: 'Specs', value: 'specifications' },
  { label: 'Reviews', value: 'reviews' },
];

const productDescription = computed(() => props.description || '<p>No description available.</p>');

const totalReviews = computed(() => props.reviews.length);

const highlightedReviews = computed(() => {
  // Return top 3 reviews
  return props.reviews.slice(0, 3);
});

const formatSpecLabel = (key: string): string => {
  return key
    .replace(/([A-Z])/g, ' $1')
    .replace(/^./, (str) => str.toUpperCase());
};
</script>

<style scoped>
.product-details {
  padding: 20px;
  background: #fff;
}

.tabs {
  display: flex;
  gap: 0;
  border-bottom: 1px solid #e0e0e0;
  margin-bottom: 20px;
}

.tab-btn {
  flex: 1;
  padding: 12px;
  background: transparent;
  border: none;
  border-bottom: 2px solid transparent;
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #666;
  cursor: pointer;
  transition: all 0.2s ease;
}

.tab-btn.active {
  color: #004bff;
  border-bottom-color: #004bff;
}

.tab-content {
  min-height: 200px;
}

/* Description Content */
.description-content h3 {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 16px;
  color: #000;
  margin: 24px 0 12px 0;
}

.rich-text {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 14px;
  line-height: 1.6;
  color: #333;
}

.rich-text :deep(p) {
  margin-bottom: 12px;
}

.rich-text :deep(img) {
  width: 100%;
  border-radius: 8px;
  margin: 12px 0;
}

.features-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.features-list li {
  display: flex;
  align-items: center;
  gap: 8px;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 14px;
  color: #333;
  margin-bottom: 8px;
}

.feature-icon {
  color: #004bff;
  font-weight: 700;
}

.info-grid {
  display: grid;
  gap: 12px;
}

.info-item {
  display: flex;
  gap: 8px;
}

.info-label {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #666;
  min-width: 100px;
}

.info-value {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 14px;
  color: #333;
}

/* Specifications Table */
.specs-table {
  width: 100%;
  border-collapse: collapse;
}

.specs-table tr {
  border-bottom: 1px solid #f0f0f0;
}

.specs-table tr:last-child {
  border-bottom: none;
}

.specs-table td {
  padding: 12px 0;
}

.spec-label {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #666;
  width: 40%;
}

.spec-value {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 14px;
  color: #333;
}

/* Reviews Preview */
.reviews-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 16px;
}

.reviews-header h3 {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 16px;
  color: #000;
  margin: 0;
}

.review-count {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #999;
}

.quick-rating {
  display: flex;
  align-items: center;
  gap: 12px;
  padding: 16px;
  background: #f9f9f9;
  border-radius: 12px;
  margin-bottom: 20px;
}

.quick-rating .rating-number {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 36px;
  color: #000;
}

.quick-rating .stars {
  display: flex;
  gap: 2px;
}

.quick-rating .star-icon {
  width: 20px;
  height: 20px;
}

.review-highlights {
  display: flex;
  flex-direction: column;
  gap: 16px;
  margin-bottom: 20px;
}

.highlight-review {
  padding: 16px;
  background: #f9f9f9;
  border-radius: 12px;
}

.reviewer-info {
  display: flex;
  align-items: center;
  gap: 10px;
  margin-bottom: 10px;
}

.reviewer-info .avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  object-fit: cover;
}

.reviewer-name {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 13px;
  color: #000;
}

.review-text {
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  line-height: 1.5;
  color: #333;
  margin-bottom: 10px;
}

.review-rating .star-icon {
  width: 14px;
  height: 14px;
}

.view-all-reviews-btn {
  width: 100%;
  padding: 12px;
  border-radius: 10px;
  background: transparent;
  border: 1px solid #004bff;
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #004bff;
  cursor: pointer;
  transition: all 0.2s ease;
}

.view-all-reviews-btn:hover {
  background: #004bff;
  color: #fff;
}

/* Delivery Info */
.delivery-info {
  margin-top: 24px;
  padding-top: 20px;
  border-top: 1px solid #f0f0f0;
}

.delivery-info h3 {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 16px;
  color: #000;
  margin: 0 0 12px 0;
}

.delivery-options {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.delivery-option {
  padding: 12px 16px;
  background: #f9f9f9;
  border-radius: 10px;
}

.delivery-option.express {
  background: linear-gradient(135deg, rgba(0, 75, 255, 0.1) 0%, rgba(0, 75, 255, 0.05) 100%);
  border: 1px solid rgba(0, 75, 255, 0.2);
}

.option-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 4px;
}

.option-name {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #333;
}

.option-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 14px;
  color: #000;
}

.option-time {
  display: block;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #666;
}

/* After Sales */
.after-sales {
  margin-top: 24px;
  padding-top: 20px;
  border-top: 1px solid #f0f0f0;
}

.after-sales h3 {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 16px;
  color: #000;
  margin: 0 0 12px 0;
}

.after-sales-list {
  list-style: none;
  padding: 0;
  margin: 0;
}

.after-sales-list li {
  display: flex;
  align-items: center;
  gap: 12px;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 14px;
  color: #333;
  margin-bottom: 10px;
}

.after-sales-list li .icon {
  font-size: 18px;
}
</style>
