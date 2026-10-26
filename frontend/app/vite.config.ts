import react from '@vitejs/plugin-react'
import { defineConfig } from 'vite'
import mkcert from 'vite-plugin-mkcert';

// vite modern frontend build tool, uses SEM Modules
// https://vite.dev/config/
export default defineConfig({
  plugins: [
    react(),
    mkcert()
  ],
  server: {
    https:{},
    port:3000
  }
})
