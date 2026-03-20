/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{vue,js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        // Figma Design Tokens - Shoppe Brand Colors
        brand: {
          blue: '#004cff',      // Primary button background
          black: '#202020',     // Primary text color
          white: '#f3f3f3',     // Button text color
        }
      },
      fontFamily: {
        // Figma Font Families
        raleway: ['Raleway', 'sans-serif'],
        nunito: ['Nunito Sans', 'sans-serif'],
      },
      fontSize: {
        // Figma Font Sizes
        '52': ['52px', '1'],
        '22': ['22px', '31px'],
        '19': ['19px', '33px'],
        '15': ['15px', '26px'],
        '14': ['14px', '1'],
      },
      letterSpacing: {
        'shoppe': '-0.52px',
      },
      borderRadius: {
        'shoppe-btn': '16px',
        'shoppe-xl': '34px',
      }
    },
  },
  plugins: [],
}
