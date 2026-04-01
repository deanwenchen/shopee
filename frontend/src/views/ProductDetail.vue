<template>
  <div class="product-detail-page">
    <!-- Status Bar -->
    <div class="status-bar">
      <img src="/assets/figma/status-bar-light.svg" alt="Status Bar" />
    </div>

    <!-- Scrollable Content -->
    <div class="scroll-container">
      <!-- Product Gallery -->
      <div
        class="gallery-section"
        @touchstart="handleTouchStart"
        @touchmove="handleTouchMove"
        @touchend="handleTouchEnd"
        @mousedown="handleMouseDown"
        @mousemove="handleMouseMove"
        @mouseup="handleMouseUp"
        @mouseleave="handleMouseLeave"
      >
        <div class="main-image-container">
          <img :src="product.images[currentImageIndex]" :alt="product.name" class="main-image" />
          <button class="share-btn" @click="handleShare">
            <img src="/assets/figma/product-share-icon.svg" alt="Share" />
          </button>
        </div>
        <!-- Gallery Dots -->
        <div class="gallery-dots">
          <div
            v-for="(image, index) in product.images"
            :key="index"
            class="dot"
            :class="{ active: index === currentImageIndex }"
            @click="goToSlide(index)"
          />
        </div>
      </div>

      <!-- Price and Description -->
      <div class="price-section">
        <h1 class="product-price">$17,00</h1>
        <p class="product-description">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam arcu mauris, scelerisque eu mauris id, pretium pulvinar sapien.
        </p>
      </div>

      <!-- Variations -->
      <div class="variations-section">
        <h2 class="section-title">Variations</h2>
        <div class="selected-variations">
          <div class="variation-chip">{{ selectedColor }}</div>
          <div class="variation-chip">{{ selectedSize }}</div>
          <button class="expand-btn" @click="openVariationsSheet">
            <img src="/assets/figma/arrow-icon-white.svg" alt="Expand" />
          </button>
        </div>
      </div>

      <!-- Specifications -->
      <div class="specifications-section">
        <h2 class="section-title">Specifications</h2>
        <div class="spec-row">
          <span class="spec-label">Material</span>
          <div class="spec-tags">
            <span class="spec-tag pink">Cotton 95%</span>
            <span class="spec-tag pink">Nylon 5%</span>
          </div>
        </div>
        <div class="spec-row">
          <span class="spec-label">Origin</span>
          <span class="spec-tag blue">EU</span>
        </div>
        <div class="spec-row">
          <span class="spec-label">Size guide</span>
          <button class="size-guide-btn" @click="openVariationsSheet">
            <img src="/assets/figma/arrow-icon-white.svg" alt="Arrow" />
          </button>
        </div>
      </div>

      <!-- Delivery -->
      <div class="delivery-section">
        <h2 class="section-title">Delivery</h2>
        <div class="delivery-option">
          <div class="delivery-option-border">
            <span class="delivery-name">Standart</span>
            <span class="delivery-price">$3,00</span>
            <span class="delivery-days">5-7 days</span>
          </div>
        </div>
        <div class="delivery-option">
          <div class="delivery-option-border">
            <span class="delivery-name">Express</span>
            <span class="delivery-price">$12,00</span>
            <span class="delivery-days">1-2 days</span>
          </div>
        </div>
      </div>

      <!-- Rating & Reviews -->
      <div class="reviews-section">
        <h2 class="section-title">Rating & Reviews</h2>
        <div class="rating-summary">
          <div class="stars-large">
            <img src="/assets/figma/star-filled.svg" alt="Star" />
            <img src="/assets/figma/star-filled.svg" alt="Star" />
            <img src="/assets/figma/star-filled.svg" alt="Star" />
            <img src="/assets/figma/star-filled.svg" alt="Star" />
            <img src="/assets/figma/star-half.svg" alt="Star" />
          </div>
          <span class="rating-value">4/5</span>
        </div>
        <div class="review-item">
          <img src="/assets/figma/review-avatar-1.png" alt="Veronika" class="review-avatar" />
          <div class="review-content">
            <div class="review-header">
              <span class="reviewer-name">Veronika</span>
              <div class="stars-small">
                <img src="/assets/figma/star-filled.svg" alt="Star" />
                <img src="/assets/figma/star-filled.svg" alt="Star" />
                <img src="/assets/figma/star-filled.svg" alt="Star" />
                <img src="/assets/figma/star-filled.svg" alt="Star" />
                <img src="/assets/figma/star-half.svg" alt="Star" />
              </div>
            </div>
            <p class="review-text">
              Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed ...
            </p>
          </div>
        </div>
        <button class="view-all-reviews-btn">View All Reviews</button>
      </div>

      <!-- Most Popular -->
      <div class="most-popular-section">
        <div class="section-header">
          <h2 class="section-title">Most Popular</h2>
          <button class="see-all-btn">
            <span>See All</span>
            <img src="/assets/figma/arrow-icon-white.svg" alt="Arrow" />
          </button>
        </div>
        <div class="popular-products">
          <div
            v-for="(item, index) in mostPopularItems"
            :key="index"
            class="popular-item"
          >
            <div class="popular-image-container">
              <img :src="item.image" :alt="item.name" class="popular-image" />
            </div>
            <div class="popular-info">
              <span class="popular-price">$17,00</span>
              <img src="/assets/figma/like-icon.svg" alt="Like" class="popular-like-icon" />
              <span class="popular-badge" :class="item.badgeType">{{ item.badge }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- You Might Like -->
      <div class="you-might-like-section">
        <h2 class="section-title">You Might Like</h2>
        <div class="like-grid">
          <div
            v-for="(item, index) in youMightLikeItems"
            :key="index"
            class="like-item"
          >
            <div class="like-image-container">
              <img :src="item.image" :alt="item.name" class="like-image" />
            </div>
            <p class="like-name">{{ item.name }}</p>
            <span class="like-price">{{ item.price }}</span>
          </div>
        </div>
      </div>
    </div>

    <!-- Bottom Bar (fixed) -->
    <BottomBar
      :product-id="product.id"
      :is-liked="isLiked"
      :selected-color="selectedColor"
      :selected-size="selectedSize"
      :quantity="quantity"
      @like="handleLike"
      @add-to-cart="handleAddToCart"
      @buy-now="handleBuyNow"
    />

    <!-- Bottom Sheet Modal for Variations -->
    <Teleport to="body">
      <transition name="bottom-sheet">
        <div v-if="showVariationsSheet" class="bottom-sheet-overlay" @click="closeVariationsSheet">
          <div class="bottom-sheet-content" @click.stop>
            <div class="bottom-sheet-header">
              <h3>Select Options</h3>
              <button class="close-btn" @click="closeVariationsSheet">
                <svg viewBox="0 0 24 24" fill="none" stroke="#666" stroke-width="2">
                  <path d="M6 6l12 12M6 18L18 6" />
                </svg>
              </button>
            </div>

            <!-- Color Selection -->
            <div class="selection-section">
              <h4>Color</h4>
              <div class="color-options">
                <button
                  v-for="color in product.variations"
                  :key="color.id"
                  class="color-option"
                  :class="{ selected: selectedColor === color.name }"
                  @click="selectColor(color)"
                >
                  <img :src="color.image" :alt="color.name" />
                  <div v-if="selectedColor === color.name" class="color-checkmark">
                    <svg viewBox="0 0 24 24" fill="none" stroke="#fff" stroke-width="3">
                      <path d="M5 13l4 4L19 7" />
                    </svg>
                  </div>
                </button>
              </div>
            </div>

            <!-- Size Selection -->
            <div class="selection-section">
              <h4>Size</h4>
              <div class="size-options">
                <button
                  v-for="size in availableSizes"
                  :key="size"
                  class="size-option"
                  :class="{ selected: selectedSize === size }"
                  @click="selectedSize = size"
                >
                  {{ size }}
                </button>
              </div>
            </div>

            <!-- Quantity -->
            <div class="selection-section">
              <h4>Quantity</h4>
              <div class="quantity-selector">
                <button class="qty-btn" @click="decreaseQuantity" :disabled="quantity <= 1">
                  -
                </button>
                <span class="quantity-value">{{ quantity }}</span>
                <button class="qty-btn" @click="increaseQuantity">+</button>
              </div>
            </div>

          </div>
        </div>
      </transition>
    </Teleport>
  </div>
</template>

<script setup lang="ts">
import { ref, onMounted, onUnmounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import BottomBar from '@/components/BottomBar.vue';

interface ProductVariation {
  id: number;
  name: string;
  image: string;
  type: 'color' | 'size';
}

interface Product {
  id: number | string;
  name: string;
  description: string;
  price: number;
  originalPrice?: number;
  isOnSale: boolean;
  saleEndTime?: string;
  images: string[];
  rating: number;
  reviewCount: number;
  salesCount: number;
  hasVariations: boolean;
  variations: ProductVariation[];
}

interface PopularItem {
  image: string;
  name: string;
  price: string;
  badge: string;
  badgeType: 'new' | 'sale' | 'hot';
}

interface LikeItem {
  image: string;
  name: string;
  price: string;
}

const router = useRouter();
const route = useRoute();

// Mock product data
const product = ref<Product>({
  id: route.params.id as string,
  name: 'Summer Collection Top',
  description:
    'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam arcu mauris, scelerisque eu mauris id, pretium pulvinar sapien.',
  price: 17.0,
  originalPrice: undefined,
  isOnSale: false,
  saleEndTime: undefined,
  images: [
    '/assets/figma/product-main.png',
    '/assets/figma/product-variation-1.png',
    '/assets/figma/product-variation-2.png',
    '/assets/figma/product-variation-3.png',
    '/assets/figma/yl-1.png',
    '/assets/figma/yl-2.png',
  ],
  rating: 4.5,
  reviewCount: 128,
  salesCount: 500,
  hasVariations: true,
  variations: [
    { id: 1, name: 'Pink', image: '/assets/figma/product-variation-1.png', type: 'color' },
    { id: 2, name: 'Floral', image: '/assets/figma/product-variation-2.png', type: 'color' },
    { id: 3, name: 'Red', image: '/assets/figma/product-variation-3.png', type: 'color' },
  ],
});

// Most Popular items
const mostPopularItems = ref<PopularItem[]>([
  { image: '/assets/figma/mp-1.png', name: 'New Arrival', price: '$17,00', badge: 'New', badgeType: 'new' },
  { image: '/assets/figma/mp-3.png', name: 'On Sale', price: '$17,00', badge: 'Sale', badgeType: 'sale' },
  { image: '/assets/figma/mp-4.png', name: 'Hot Item', price: '$17,00', badge: 'Hot', badgeType: 'hot' },
  { image: '/assets/figma/mp-2.png', name: 'Hot Item 2', price: '$17,00', badge: 'Hot', badgeType: 'hot' },
]);

// You Might Like items
const youMightLikeItems = ref<LikeItem[]>([
  { image: '/assets/figma/yl-1.png', name: 'Lorem ipsum dolor sit amet consectetur', price: '$21,00' },
  { image: '/assets/figma/yl-2.png', name: 'Lorem ipsum dolor sit amet consectetur', price: '$17,00' },
  { image: '/assets/figma/yl-3.png', name: 'Lorem ipsum dolor sit amet consectetur', price: '$21,00' },
  { image: '/assets/figma/yl-4.png', name: 'Lorem ipsum dolor sit amet consectetur', price: '$17,00' },
]);

// State
const isLiked = ref(false);
const selectedColor = ref<string>('Pink');
const selectedSize = ref<string>('M');
const quantity = ref(1);
const showVariationsSheet = ref(false);
const currentImageIndex = ref(0);
const autoPlayTimer = ref<number | null>(null);

// Touch/Mouse drag state
let touchStartX = 0;
let touchEndX = 0;
let isDragging = false;
const swipeThreshold = 50;

const availableSizes = ['S', 'M', 'L', 'XL', 'XXL', 'XXXL'];

// Auto-play gallery
const startAutoPlay = () => {
  if (autoPlayTimer.value) clearInterval(autoPlayTimer.value);
  autoPlayTimer.value = window.setInterval(() => {
    currentImageIndex.value = (currentImageIndex.value + 1) % product.value.images.length;
  }, 3000);
};

const stopAutoPlay = () => {
  if (autoPlayTimer.value) {
    clearInterval(autoPlayTimer.value);
    autoPlayTimer.value = null;
  }
};

// Touch/Mouse drag handlers
const handleTouchStart = (e: TouchEvent) => {
  touchStartX = e.touches[0].clientX;
  isDragging = true;
  stopAutoPlay();
};

const handleTouchMove = (e: TouchEvent) => {
  if (!isDragging) return;
  touchEndX = e.touches[0].clientX;
};

const handleTouchEnd = () => {
  if (!isDragging) return;
  isDragging = false;
  handleSwipe();
};

const handleMouseDown = (e: MouseEvent) => {
  touchStartX = e.clientX;
  isDragging = true;
  stopAutoPlay();
};

const handleMouseMove = (e: MouseEvent) => {
  if (!isDragging) return;
  touchEndX = e.clientX;
};

const handleMouseUp = () => {
  if (!isDragging) return;
  isDragging = false;
  handleSwipe();
  startAutoPlay();
};

const handleMouseLeave = () => {
  if (isDragging) {
    isDragging = false;
    handleSwipe();
    startAutoPlay();
  }
};

const handleSwipe = () => {
  const diff = touchStartX - touchEndX;

  if (Math.abs(diff) > swipeThreshold) {
    if (diff > 0) {
      // Swipe left - next
      nextSlide();
    } else {
      // Swipe right - prev
      prevSlide();
    }
    // Restart autoplay after swipe
    startAutoPlay();
  }
};

const nextSlide = () => {
  if (currentImageIndex.value < product.value.images.length - 1) {
    currentImageIndex.value++;
  } else {
    currentImageIndex.value = 0; // Loop back to first image
  }
};

const prevSlide = () => {
  if (currentImageIndex.value > 0) {
    currentImageIndex.value--;
  } else {
    currentImageIndex.value = product.value.images.length - 1; // Loop to last image
  }
};

const goToSlide = (index: number) => {
  currentImageIndex.value = index;
  stopAutoPlay();
  startAutoPlay();
};

// Handlers
const handleLike = (liked: boolean) => {
  isLiked.value = liked;
  console.log('Product liked:', liked);
};

const handleShare = () => {
  console.log('Share product');
  // Implement share logic
};

const handleSlideChange = (index: number) => {
  currentImageIndex.value = index;
};

const openVariationsSheet = () => {
  stopAutoPlay();
  showVariationsSheet.value = true;
};

const closeVariationsSheet = () => {
  showVariationsSheet.value = false;
  startAutoPlay();
};

const selectColor = (variation: ProductVariation) => {
  selectedColor.value = variation.name;
};

const decreaseQuantity = () => {
  if (quantity.value > 1) {
    quantity.value--;
  }
};

const increaseQuantity = () => {
  quantity.value++;
};

const handleAddToCart = (data: { color: string; size: string; quantity: number }) => {
  showVariationsSheet.value = false;
  console.log('Add to cart:', data);
  // Implement add to cart logic
};

const handleBuyNow = (data: { color: string; size: string; quantity: number }) => {
  showVariationsSheet.value = false;
  console.log('Buy now:', data);
  // Implement buy now logic
  router.push('/checkout');
};

onMounted(() => {
  startAutoPlay();
});

onUnmounted(() => {
  stopAutoPlay();
});
</script>

<style scoped>
.product-detail-page {
  position: relative;
  width: 375px;
  height: 100vh;
  margin: 0 auto;
  background: #fff;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
  overflow: hidden;
}

.status-bar {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  z-index: 1000;
}

.status-bar img {
  width: 100%;
  height: 44px;
}

.scroll-container {
  flex: 1;
  overflow-y: auto;
  -webkit-overflow-scrolling: touch;
  padding-bottom: 100px;
}

/* Gallery Section */
.gallery-section {
  width: 100%;
  background: #f5f5f5;
}

.main-image-container {
  position: relative;
  width: 100%;
  height: 439px;
}

.main-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.share-btn {
  position: absolute;
  top: 12px;
  right: 12px;
  width: 30px;
  height: 30px;
  background: rgba(255, 240, 240, 0.95);
  border-radius: 50%;
  border: none;
  display: flex;
  align-items: center;
  justify-content: center;
  cursor: pointer;
}

.share-btn img {
  width: 16px;
  height: 16px;
}

.gallery-dots {
  display: flex;
  justify-content: center;
  gap: 10px;
  padding: 12px 0;
  background: #fff;
}

.dot {
  width: 10px;
  height: 10px;
  border-radius: 50%;
  background: #e0e0e0;
  cursor: pointer;
  transition: all 0.3s ease;
}

.dot.active {
  width: 40px;
  border-radius: 5px;
  background: #004cff;
}

/* Price Section */
.price-section {
  padding: 15px 20px;
}

.product-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 26px;
  color: #000;
  letter-spacing: -0.26px;
  margin: 0 0 8px 0;
}

.product-description {
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 400;
  font-size: 15px;
  line-height: 20px;
  color: #000;
  margin: 0;
}

/* Variations Section */
.variations-section {
  padding: 0 20px 16px;
}

.section-title {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 20px;
  color: #000;
  margin: 0 0 12px 0;
  letter-spacing: -0.2px;
}

.selected-variations {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 16px;
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

.expand-btn {
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
}

.expand-btn img {
  width: 20px;
  height: 20px;
}

.variations-thumbnails {
  display: flex;
  gap: 12px;
}

.thumbnail {
  width: 80px;
  height: 80px;
  border-radius: 8px;
  overflow: hidden;
}

.thumbnail img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

/* Specifications Section */
.specifications-section {
  padding: 0 20px 24px;
}

.spec-row {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 12px;
}

.spec-label {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 17px;
  color: #202020;
  min-width: 70px;
}

.spec-tags {
  display: flex;
  gap: 8px;
}

.spec-tag {
  padding: 8px 16px;
  border-radius: 4px;
  font-family: 'Raleway', sans-serif;
  font-weight: 500;
  font-size: 14px;
}

.spec-tag.pink {
  background: #ffebeb;
  color: #000;
}

.spec-tag.blue {
  background: #e5ebfc;
  color: #000;
}

.size-guide-btn {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  background: #004bff;
  border: none;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  margin-left: auto;
}

.size-guide-btn img {
  width: 20px;
  height: 20px;
}

/* Delivery Section */
.delivery-section {
  padding: 0 20px 24px;
}

.delivery-option {
  margin-bottom: 12px;
}

.delivery-option-border {
  position: relative;
  display: flex;
  align-items: center;
  border: 1px solid #004cff;
  border-radius: 10px;
  padding: 10px 12px;
}

.delivery-name {
  font-family: 'Raleway', sans-serif;
  font-weight: 500;
  font-size: 16px;
  color: #000;
}

.delivery-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 16px;
  color: #000;
  margin-left: auto;
  margin-right: 12px;
}

.delivery-days {
  background: #f5f8ff;
  border-radius: 4px;
  padding: 4px 8px;
  font-family: 'Raleway', sans-serif;
  font-weight: 500;
  font-size: 13px;
  color: #004cff;
}

/* Reviews Section */
.reviews-section {
  padding: 0 20px 24px;
}

.rating-summary {
  display: flex;
  align-items: center;
  gap: 12px;
  margin-bottom: 20px;
}

.stars-large {
  display: flex;
  gap: 4px;
}

.stars-large img {
  width: 20px;
  height: 20px;
}

.rating-value {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 14px;
  color: #202020;
  background: #dfe9ff;
  padding: 2px 8px;
  border-radius: 6px;
}

.review-item {
  display: flex;
  gap: 12px;
  margin-bottom: 20px;
}

.review-avatar {
  width: 40px;
  height: 40px;
  border-radius: 50%;
  object-fit: cover;
}

.review-content {
  flex: 1;
}

.review-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 8px;
}

.reviewer-name {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 16px;
  color: #000;
}

.stars-small {
  display: flex;
  gap: 2px;
}

.stars-small img {
  width: 15px;
  height: 15px;
}

.review-text {
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 400;
  font-size: 12px;
  line-height: 18px;
  color: #000;
  margin: 0;
}

.view-all-reviews-btn {
  width: 100%;
  background: #004cff;
  border: none;
  border-radius: 11px;
  padding: 12px;
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 300;
  font-size: 16px;
  color: #f3f3f3;
  cursor: pointer;
}

/* Most Popular Section */
.most-popular-section {
  padding: 0 20px 24px;
}

.section-header {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 16px;
}

.see-all-btn {
  display: flex;
  align-items: center;
  gap: 8px;
  background: none;
  border: none;
  cursor: pointer;
}

.see-all-btn span {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 15px;
  color: #202020;
}

.see-all-btn img {
  width: 14px;
  height: 14px;
}

.popular-products {
  display: flex;
  gap: 10px;
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
}

.popular-item {
  flex-shrink: 0;
  width: 104px;
}

.popular-info {
  display: flex;
  align-items: center;
  gap: 6px;
  margin-top: 8px;
  position: relative;
  height: 20px;
}

.popular-image-container {
  position: relative;
  width: 100%;
  height: 140px;
  border-radius: 9px;
  overflow: hidden;
  box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
  background: #fff;
}

.popular-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.like-btn {
  position: absolute;
  top: 8px;
  right: 8px;
  background: none;
  border: none;
  cursor: pointer;
  padding: 0;
}

.like-btn img {
  width: 24px;
  height: 24px;
}

.popular-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 15px;
  color: #202020;
  white-space: nowrap;
}

.popular-like-icon {
  width: 16px;
  height: 16px;
  flex-shrink: 0;
}

.popular-badge {
  background: #ff5790;
  border-radius: 4px;
  padding: 2px 6px;
  font-family: 'Raleway', sans-serif;
  font-weight: 500;
  font-size: 13px;
  color: #fff;
  white-space: nowrap;
}

.popular-badge.new {
  background: linear-gradient(135deg, #ff9a9e, #fecfef);
}

.popular-badge.sale {
  background: linear-gradient(135deg, #ff5790, #f81140);
}

.popular-badge.hot {
  background: linear-gradient(135deg, #ff6b6b, #ee5a5a);
}

/* You Might Like Section */
.you-might-like-section {
  padding: 0 20px 24px;
}

.like-grid {
  display: grid;
  grid-template-columns: repeat(2, 1fr);
  gap: 10px;
}

.like-item {
  background: #fff;
  border-radius: 9px;
  overflow: hidden;
  box-shadow: 0 5px 10px rgba(0, 0, 0, 0.1);
}

.like-image-container {
  width: 100%;
  height: 171px;
  overflow: hidden;
}

.like-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.like-name {
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 400;
  font-size: 12px;
  line-height: 16px;
  color: #000;
  padding: 8px 10px;
  margin: 0;
  height: 36px;
  overflow: hidden;
}

.like-price {
  display: block;
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 17px;
  color: #202020;
  padding: 0 10px 10px;
  letter-spacing: -0.17px;
}

/* Bottom Sheet Styles */
.bottom-sheet-overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  z-index: 99;
  display: flex;
  align-items: flex-end;
  justify-content: center;
}

.bottom-sheet-content {
  background: #fff;
  border-radius: 20px 20px 0 0;
  width: 100%;
  max-width: 375px;
  max-height: calc(100vh - 84px);
  overflow-y: auto;
  padding: 24px 20px calc(24px + 84px);
  animation: slideUp 0.3s ease;
}

@keyframes slideUp {
  from {
    transform: translateY(100%);
  }
  to {
    transform: translateY(0);
  }
}

.bottom-sheet-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
}

.bottom-sheet-header h3 {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 18px;
  color: #000;
  margin: 0;
}

.close-btn {
  width: 32px;
  height: 32px;
  border-radius: 50%;
  background: #f5f5f5;
  border: none;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
}

.close-btn svg {
  width: 16px;
  height: 16px;
}

.selection-section {
  margin-bottom: 24px;
}

.selection-section:last-of-type {
  margin-bottom: 24px;
}

.selection-section h4 {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 14px;
  color: #000;
  margin: 0 0 12px 0;
}

.color-options {
  display: flex;
  gap: 12px;
  overflow-x: auto;
}

.color-option {
  flex-shrink: 0;
  width: 60px;
  height: 60px;
  border-radius: 8px;
  overflow: hidden;
  position: relative;
  border: 2px solid transparent;
  cursor: pointer;
}

.color-option.selected {
  border-color: #004bff;
}

.color-option img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.color-checkmark {
  position: absolute;
  top: 2px;
  right: 2px;
  width: 18px;
  height: 18px;
  background: #004bff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.color-checkmark svg {
  width: 10px;
  height: 10px;
}

.size-options {
  display: grid;
  grid-template-columns: repeat(6, 1fr);
  gap: 8px;
}

.size-option {
  aspect-ratio: 1;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  background: #fff;
  font-family: 'Raleway', sans-serif;
  font-weight: 500;
  font-size: 14px;
  color: #000;
  cursor: pointer;
  transition: all 0.2s ease;
}

.size-option.selected {
  border-color: #004bff;
  background: #004bff;
  color: #fff;
}

.quantity-selector {
  display: flex;
  align-items: center;
  gap: 16px;
}

.qty-btn {
  width: 40px;
  height: 40px;
  border-radius: 8px;
  border: 1px solid #e0e0e0;
  background: #fff;
  font-size: 20px;
  cursor: pointer;
  transition: all 0.2s ease;
}

.qty-btn:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.qty-btn:not(:disabled):active {
  background: #f5f5f5;
}

.quantity-value {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 18px;
  min-width: 30px;
  text-align: center;
}

/* Bottom Sheet Transition */
.bottom-sheet-enter-active,
.bottom-sheet-leave-active {
  transition: opacity 0.3s ease;
}

.bottom-sheet-enter-active .bottom-sheet-content,
.bottom-sheet-leave-active .bottom-sheet-content {
  transition: transform 0.3s ease;
}

.bottom-sheet-enter-from,
.bottom-sheet-leave-to {
  opacity: 0;
}

.bottom-sheet-enter-from .bottom-sheet-content,
.bottom-sheet-leave-to .bottom-sheet-content {
  transform: translateY(100%);
}
</style>
