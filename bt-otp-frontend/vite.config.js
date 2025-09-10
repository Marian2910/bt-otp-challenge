import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import fs from 'fs'

export default defineConfig({
  plugins: [react()],
  server: {
    https: {
      key: fs.readFileSync('./certs/localhost+1-key.pem'),
      cert: fs.readFileSync('./certs/localhost+1.pem'),
    },
    proxy: {
      '/api': {
        target: 'https://localhost:7124',
        changeOrigin: true,
        secure: false,
      },
    },
  },
});
