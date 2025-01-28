﻿/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Pages/**/*.cshtml",
    "./Views/**/*.cshtml",
    "./wwwroot/js/**/*.js",
  ],
  theme: {
    fontFamily: {
      sans: ['Work Sans', 'sans-serif'],
    },
    extend: {
      colors: {
        background: {
          DEFAULT: "#04080F",
          50: "#2B55A0",
          100: "#264D90",
          200: "#1E3C70",
          300: "#152A4F",
          400: "#0D192F",
          500: "#04080F",
          600: "#000000",
        },
        primary: {
          DEFAULT: '#EB5E28',
          50: '#FBDCD0',
          100: '#F9CEBD',
          200: '#F5B298',
          300: '#F29673',
          400: '#EE7A4D',
          500: '#EB5E28',
          600: '#C84513',
          700: '#95330E',
          800: '#622209',
          900: '#2E1004',
          950: '#150702'
        },
        success: {
          50: "#E8FAF0",
          100: "#D1F4E0",
          200: "#A2E9C1",
          300: "#74DFA2",
          400: "#45D483",
          500: "#17C964",
          600: "#12A150",
          700: "#0E793C",
          800: "#095028",
          900: "#052814",
        },
        info: {
          100: "#D2F1FF",
          200: "#A4DFFF",
          300: "#78C8FF",
          400: "#56B2FF",
          500: "#1E8EFF",
          600: "#156EDB",
          700: "#0F52B7",
          800: "#093993",
          900: "#05287A",
        },
        warning: {
          50: "#FEFCE8",
          100: "#FDEDD3",
          200: "#FBDBA7",
          300: "#F9C97C",
          400: "#F7B750",
          500: "#F5A524",
          600: "#C4841D",
          700: "#936316",
          800: "#62420E",
          900: "#312107",
        },
        danger: {
          50: "#FEE7EF",
          100: "#FDD0DF",
          200: "#FAA0BF",
          300: "#F871A0",
          400: "#F54180",
          500: "#F31260",
          600: "#C20E4D",
          700: "#920B3A",
          800: "#610726",
          900: "#310413",
        },
        gold: {
          DEFAULT: "#C89B3C",
          50: "#F0E4CB",
          100: "#ECDCBB",
          200: "#E3CC9B",
          300: "#DABC7C",
          400: "#D1AB5C",
          500: "#C89B3C",
          600: "#9F7A2D",
          700: "#735921",
          800: "#483714",
          900: "#1C1508",
          950: "#060502",
        },
      },
      layout: {
        disabledOpacity: "0.3",
        radius: {
          small: "4px",
          medium: "6px",
          large: "8px",
        },
        borderWidth: {
          small: "1px",
          medium: "2px",
          large: "3px",
        },
      },
    },
  },
  plugins: [require("@tailwindcss/forms")],
};
