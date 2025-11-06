import axios from 'axios';

window.axios = axios;

window.axios.defaults.headers.common['X-Requested-With'] = 'XMLHttpRequest';

const root = document.documentElement
const isDark = localStorage.theme === "dark" || (!("theme" in localStorage) && window.matchMedia("(prefers-color-scheme: dark)").matches)
root.classList.toggle("dark", isDark)
root.setAttribute("data-theme", isDark ? "dark" : "light")