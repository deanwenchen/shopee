# Shoppe Start Page Design Specification

## Overview

Implement the Start page (node-id: 0:12855) from Figma design as a Vue 3 component with pixel-perfect accuracy.

## Design Specifications

### Page Dimensions
- Width: 375px
- Height: 817px
- Background: White (#ffffff)

### Elements (Top to Bottom)

#### 1. Status Bar
- Position: top-0, full width
- Height: 44px
- Contains: Time (9:41), Cellular signal, WiFi, Battery icons

#### 2. Logo Icon
- Position: centered horizontally
- Vertical position: ~31% from top (node: 8:4768)
- Size: shopping bag icon with circular background

#### 3. Brand Title "Shoppe"
- Font: Raleway Bold
- Size: 52px
- Color: #202020
- Position: top-[390px], centered horizontally
- Letter spacing: -0.52px

#### 4. Subtitle
- Text: "Beautiful eCommerce UI Kit for your online store"
- Font: Nunito Sans Light
- Size: 19px
- Line height: 33px
- Color: #202020
- Position: top-[469px]
- Width: 249px, centered

#### 5. Primary Button "Let's get started"
- Background: #004cff
- Border radius: 16px
- Size: 335px × 61px
- Position: top-[634px], centered
- Text: Nunito Sans Light, 22px, line-height 31px
- Text color: #f3f3f3

#### 6. Secondary Action "I already have an account"
- Position: top-[713px]
- Text: Nunito Sans Light, 15px, line-height 26px
- Color: #202020 (opacity 90%)
- Arrow button: blue circle with right arrow icon

#### 7. Home Indicator
- Position: bottom-[24px] (approximate)
- Size: 134px × 5px
- Color: black
- Border radius: 34px

## Assets (from Figma MCP)

- imgButton
- imgArrow
- imgBarsStatusBarLightStatusBar
- imgBorder
- imgCap
- imgCapacity
- imgWifi
- imgCellularConnection
- imgBackground
- imgEllipse
- imgGroup1000004649

## Technical Requirements

- Vue 3 with `<script setup>` and Composition API
- Tailwind CSS for styling (mapped to Figma tokens)
- No hardcoded values - use design tokens
- Component structure must match Figma layer hierarchy
- 1:1 pixel-perfect implementation

## Component Structure

```
StartPage.vue
├── Status Bar (component)
│   ├── Time
│   ├── Cellular
│   ├── WiFi
│   └── Battery
├── Logo
├── Brand Title
├── Subtitle
├── Primary Button (component)
├── Secondary Action (component)
│   ├── Text
│   └── Arrow Button
└── Home Indicator
```
