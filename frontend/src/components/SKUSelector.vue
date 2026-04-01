<template>
  <div class="sku-selector">
    <!-- Color Selection -->
    <div class="selection-section" v-if="colors.length > 0">
      <h4 class="section-title">Color</h4>
      <div class="color-options">
        <button
          v-for="color in colors"
          :key="color.id"
          class="color-option"
          :class="{ selected: selectedColor?.id === color.id }"
          @click="selectColor(color)"
        >
          <img :src="color.image" :alt="color.name" class="color-image" />
          <div v-if="selectedColor?.id === color.id" class="color-checkmark">
            <svg viewBox="0 0 24 24" fill="none" stroke="#fff" stroke-width="3">
              <path d="M5 13l4 4L19 7" />
            </svg>
          </div>
        </button>
      </div>
    </div>

    <!-- Size Selection -->
    <div class="selection-section" v-if="sizes.length > 0">
      <h4 class="section-title">Size</h4>
      <div class="size-options">
        <button
          v-for="size in sizes"
          :key="size"
          class="size-option"
          :class="{
            selected: selectedSize === size,
            outOfStock: isSizeOutOfStock(size)
          }"
          @click="selectSize(size)"
          :disabled="isSizeOutOfStock(size)"
        >
          {{ size }}
        </button>
      </div>
    </div>

    <!-- Quantity Selection -->
    <div class="selection-section">
      <h4 class="section-title">Quantity</h4>
      <div class="quantity-selector">
        <button
          class="quantity-btn"
          @click="decreaseQuantity"
          :disabled="quantity <= 1"
        >
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M5 12h14" />
          </svg>
        </button>
        <span class="quantity-value">{{ quantity }}</span>
        <button
          class="quantity-btn"
          @click="increaseQuantity"
          :disabled="quantity >= maxQuantity"
        >
          <svg viewBox="0 0 24 24" fill="none" stroke="currentColor" stroke-width="2">
            <path d="M12 5v14M5 12h14" />
          </svg>
        </button>
      </div>
      <span class="stock-status" :class="{ low: remainingStock <= 10 }">
        <span v-if="remainingStock > 10">In Stock</span>
        <span v-else-if="remainingStock > 0">Only {{ remainingStock }} left!</span>
        <span v-else>Out of Stock</span>
      </span>
    </div>

    <!-- Selected Summary -->
    <div class="selected-summary">
      <span>Selected:</span>
      <strong>{{ selectedColor?.name || '-' }}</strong>
      <span>/</span>
      <strong>{{ selectedSize || '-' }}</strong>
      <span>/</span>
      <strong>{{ quantity }}</strong>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, defineProps, defineEmits, watch } from 'vue';

interface ColorOption {
  id: number;
  name: string;
  image: string;
}

interface StockData {
  [key: string]: {
    [key: string]: number; // color -> size -> stock
  };
}

interface Props {
  colors: ColorOption[];
  sizes: string[];
  stock?: StockData;
  maxQuantityPerOrder?: number;
  defaultColor?: number;
  defaultSize?: string;
}

interface Emits {
  (e: 'change', selection: { colorId: number | null; size: string | null; quantity: number }): void;
  (e: 'color-change', colorId: number): void;
  (e: 'size-change', size: string): void;
  (e: 'quantity-change', quantity: number): void;
}

const props = withDefaults(defineProps<Props>(), {
  maxQuantityPerOrder: 10,
  defaultColor: undefined,
  defaultSize: undefined,
});

const emit = defineEmits<Emits>();

const selectedColor = ref<ColorOption | null>(
  props.colors.find(c => c.id === props.defaultColor) || null
);
const selectedSize = ref<string>(props.defaultSize || '');
const quantity = ref(1);

// Compute remaining stock for current selection
const remainingStock = computed(() => {
  if (!selectedColor.value || !selectedSize.value) return 0;
  return props.stock?.[selectedColor.value.name]?.[selectedSize.value] || 0;
});

const maxQuantity = computed(() => {
  return Math.min(props.maxQuantityPerOrder, remainingStock.value || props.maxQuantityPerOrder);
});

const isSizeOutOfStock = (size: string) => {
  if (!selectedColor.value) return false;
  const stock = props.stock?.[selectedColor.value.name]?.[size] || 0;
  return stock === 0;
};

const selectColor = (color: ColorOption) => {
  selectedColor.value = color;
  emit('color-change', color.id);
  emitChange();
};

const selectSize = (size: string) => {
  if (isSizeOutOfStock(size)) return;
  selectedSize.value = size;
  emit('size-change', size);
  emitChange();
};

const decreaseQuantity = () => {
  if (quantity.value > 1) {
    quantity.value--;
    emit('quantity-change', quantity.value);
    emitChange();
  }
};

const increaseQuantity = () => {
  if (quantity.value < maxQuantity.value) {
    quantity.value++;
    emit('quantity-change', quantity.value);
    emitChange();
  }
};

const emitChange = () => {
  emit('change', {
    colorId: selectedColor.value?.id || null,
    size: selectedSize.value || null,
    quantity: quantity.value,
  });
};

// Watch for default changes
watch(() => props.defaultColor, (newVal) => {
  if (newVal) {
    selectedColor.value = props.colors.find(c => c.id === newVal) || null;
  }
});

watch(() => props.defaultSize, (newVal) => {
  if (newVal) {
    selectedSize.value = newVal;
  }
});

// Emit initial state
emitChange();
</script>

<style scoped>
.sku-selector {
  padding: 20px;
  background: #fff;
}

.selection-section {
  margin-bottom: 24px;
}

.section-title {
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
  -webkit-overflow-scrolling: touch;
  padding-bottom: 4px;
}

.color-option {
  flex-shrink: 0;
  width: 75px;
  height: 75px;
  border-radius: 8px;
  overflow: hidden;
  position: relative;
  border: 2px solid transparent;
  cursor: pointer;
  transition: border-color 0.2s ease, transform 0.15s ease;
}

.color-option:active {
  transform: scale(0.95);
}

.color-option.selected {
  border-color: #004bff;
}

.color-image {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.color-checkmark {
  position: absolute;
  top: 4px;
  right: 4px;
  width: 24px;
  height: 24px;
  background: #004bff;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
}

.color-checkmark svg {
  width: 14px;
  height: 14px;
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

.size-option:hover:not(:disabled) {
  border-color: #004bff;
}

.size-option.selected {
  border-color: #004bff;
  background: #004bff;
  color: #fff;
}

.size-option.outOfStock {
  opacity: 0.4;
  cursor: not-allowed;
  background: #f5f5f5;
}

.size-option:disabled {
  cursor: not-allowed;
}

.quantity-selector {
  display: flex;
  align-items: center;
  gap: 12px;
}

.quantity-btn {
  width: 40px;
  height: 40px;
  border-radius: 8px;
  border: 1px solid #e0e0e0;
  background: #fff;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.2s ease;
}

.quantity-btn:hover:not(:disabled) {
  border-color: #004bff;
  color: #004bff;
}

.quantity-btn:active:not(:disabled) {
  background: #f5f5f5;
}

.quantity-btn:disabled {
  opacity: 0.4;
  cursor: not-allowed;
}

.quantity-btn svg {
  width: 16px;
  height: 16px;
}

.quantity-value {
  font-family: 'Raleway', sans-serif;
  font-weight: 600;
  font-size: 18px;
  min-width: 40px;
  text-align: center;
  color: #000;
}

.stock-status {
  display: block;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 12px;
  color: #666;
  margin-top: 8px;
}

.stock-status.low {
  color: #ff5790;
  font-weight: 600;
}

.selected-summary {
  display: flex;
  align-items: center;
  gap: 8px;
  padding-top: 16px;
  border-top: 1px solid #f0f0f0;
  font-family: 'Nunito Sans', sans-serif;
  font-size: 13px;
  color: #666;
}

.selected-summary strong {
  color: #000;
  font-weight: 600;
}
</style>
