/** @type {import('tailwindcss').Config} */
module.exports = {
  important: false,
  content: [
    "./src/**/*.{html,ts}",
  ],
  theme: {
    extend: {
      fontFamily: {
        poppins: ['Poppins', 'sans-serif'],
      },
    },
    plugins: [],
  }
}