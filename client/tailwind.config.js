/** @type {import('tailwindcss').Config} */
module.exports = {
  content: ['./src/**/*.{html,ts}'],
  theme: {
    extend: {
      colors: {
        relief: {
          ink: '#17332f',
          forest: '#1c5a4c',
          mint: '#dcebe1',
          signal: '#df6548',
          paper: '#f4f3ec',
        },
      },
      fontFamily: {
        sans: ['DM Sans', 'sans-serif'],
        display: ['Manrope', 'sans-serif'],
      },
    },
  },
  plugins: [],
};
