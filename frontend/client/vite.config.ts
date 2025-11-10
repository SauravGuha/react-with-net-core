import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react-swc'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  build: {
    outDir: '../../backend/webapp/wwwroot',
    chunkSizeWarningLimit: 1500,
    
  },
  server: {
    port: 5000
  }
})
