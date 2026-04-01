<template>
  <div class="product-detail-page">
    <!-- Scrollable Content -->
    <div class="scroll-container">
      <!-- Product Gallery -->
      <div class="gallery-wrapper">
        <ProductGallery
          :images="product.images"
          @like="handleLike"
          @slide-change="handleSlideChange"
        />
      </div>

      <!-- Product Info -->
      <div class="product-info-section">
        <!-- Price and Share -->
        <div class="price-row">
          <span class="current-price">$17,00</span>
          <button class="share-btn" @click="handleShare">
            <img src="/assets/figma/product-share-icon.svg" alt="Share" />
          </button>
        </div>

        <!-- Description -->
        <p class="product-description">
          Lorem ipsum dolor sit amet, consectetur adipiscing elit. Etiam arcu mauris, scelerisque eu mauris id, pretium pulvinar sapien.
        </p>

        <!-- Variations -->
        <div class="variations-section">
          <h3 class="variations-title">Variations</h3>
          <div class="selected-variations">
            <div class="variation-chip">Pink</div>
            <div class="variation-chip">M</div>
            <button class="expand-btn" @click="openVariationsSheet">
              <img src="/assets/figma/arrow-icon-white.svg" alt="Expand" />
            </button>
          </div>
          <div class="variations-grid">
            <div class="variation-item" v-for="i in 3" :key="i">
              <img :src="product.images[i]" :alt="`Variation ${i}`" />
            </div>
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
import { ref, onMounted } from 'vue';
import { useRouter, useRoute } from 'vue-router';
import ProductGallery from '@/components/ProductGallery.vue';
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

const router = useRouter();
const route = useRoute();

// Mock product data - will be replaced with API call
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

// State
const isLiked = ref(false);
const selectedColor = ref<string>('Pink');
const selectedSize = ref<string>('M');
const quantity = ref(1);
const showVariationsSheet = ref(false);

const availableSizes = ['S', 'M', 'L', 'XL', 'XXL', 'XXXL'];

// Handlers
const handleLike = (liked: boolean) => {
  isLiked.value = liked;
  console.log('Product liked:', liked);
};

const handleSlideChange = (index: number) => {
  console.log('Slide changed to:', index);
};

const openVariationsSheet = () => {
  showVariationsSheet.value = true;
};

const closeVariationsSheet = () => {
  showVariationsSheet.value = false;
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
  // TODO: Fetch product data from API
  // For now, using mock data
});
</script>

<style scoped>
.product-detail-page {
  position: relative;
  width: 375px;
  min-height: 100vh;
  margin: 0 auto;
  background: #fff;
  box-sizing: border-box;
  display: flex;
  flex-direction: column;
}

.scroll-container {
  flex: 1;
  overflow-y: auto;
  -webkit-overflow-scrolling: touch;
  padding-bottom: 100px; /* 确保内容可以滚动到不被 BottomBar 遮挡的位置 */
}

.gallery-wrapper {
  width: 100%;
  background: #f5f5f5;
}

.product-info-section {
  padding: 15px 20px 20px;
  width: 100%;
  background: #fff;
}

/* Price Row */
.price-row {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 8px;
}

.current-price {
  font-family: 'Raleway', sans-serif;
  font-weight: 800;
  font-size: 26px;
  color: #000;
  letter-spacing: -0.26px;
}

.share-btn {
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

/* Description */
.product-description {
  font-family: 'Nunito Sans', sans-serif;
  font-weight: 400;
  font-size: 15px;
  line-height: 20px;
  color: #000;
  margin: 0 0 24px 0;
}

/* Variations Section */
.variations-section {
  margin-top: 16px;
}

.variations-title {
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

.variations-grid {
  display: flex;
  gap: 12px;
}

.variation-item {
  flex-shrink: 0;
  width: 80px;
  height: 80px;
  border-radius: 8px;
  overflow: hidden;
}

.variation-item img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.section-title {
  font-family: 'Raleway', sans-serif;
  font-weight: 700;
  font-size: 16px;
  color: #000;
  margin: 0 0 16px 0;
}

.variation-thumbnails {
  display: flex;
  gap: 12px;
  overflow-x: auto;
  -webkit-overflow-scrolling: touch;
}

.checkmark {
  position: absolute;
  top: 4px;
  right: 4px;
  width: 20px;
  height: 20px;
  background: #004bff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.checkmark svg {
  width: 12px;
  height: 12px;
}

/* Bottom Sheet Styles */
.bottom-sheet-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  z-index: 99; /* 低于 BottomBar (z-index: 100) */
  display: flex;
  align-items: flex-end;
  justify-content: center;
}

.bottom-sheet-content {
  background: #fff;
  border-radius: 20px 20px 0 0;
  width: 100%;
  max-width: 375px;
  max-height: calc(100vh - 84px); /* 视口高度减去 BottomBar 高度 */
  overflow-y: auto;
  padding: 24px 20px calc(24px + 84px); /* 底部 padding 增加 84px 避免被 BottomBar 遮挡 */
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
