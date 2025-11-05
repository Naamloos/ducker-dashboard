import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import laravel from "laravel-vite-plugin";
import { mkdirSync } from 'fs';
import path from 'path';
import tailwindcss from '@tailwindcss/vite';

const outDir = '../Dev.Naamloos.Ducker/wwwroot/build';
mkdirSync(outDir, { recursive: true });

// https://vite.dev/config/
export default defineConfig({
  plugins: [
    laravel({
      input: ['src/main.tsx'],
      publicDirectory: outDir,
      refresh: true,
    }),
    react(),
    tailwindcss(),
  ],
  resolve: {
    alias: {
      '@': path.resolve(__dirname, 'src'),
    }
  },
  build: {
    outDir,
    emptyOutDir: true,
  }
})
