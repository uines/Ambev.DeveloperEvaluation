/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      colors: {
        // Cores da Ambev para uso fácil no Tailwind
        'ambev-blue': '#00509A',
        'ambev-yellow': '#FFC72C',
        'ambev-light-blue': '#E6F3FA',
      },
      fontFamily: {
        // Define a fonte Inter
        'inter': ['Inter', 'sans-serif'],
        // Define Helvetica Neue para o logo, se necessário
        'helvetica-neue': ['Helvetica Neue', 'Arial', 'sans-serif'],
      }
    },
  },
  plugins: [],
}