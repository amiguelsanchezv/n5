import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'
import { readFileSync } from 'fs'
import { fileURLToPath } from 'url'
import { dirname, resolve } from 'path'

const __filename = fileURLToPath(import.meta.url)
const __dirname = dirname(__filename)

// https://vitejs.dev/config/
export default defineConfig(({ mode }) => {
  // Determine which env file to load based on mode or default to .dev.env
  const envFile = mode === 'local' 
    ? resolve(__dirname, './environments/.local.env')
    : resolve(__dirname, './environments/.dev.env')
  
  // Read and parse the env file
  let envVars = {}
  try {
    const envContent = readFileSync(envFile, 'utf-8')
    envContent.split('\n').forEach(line => {
      const trimmed = line.trim()
      if (trimmed && !trimmed.startsWith('#')) {
        const [key, ...valueParts] = trimmed.split('=')
        if (key && valueParts.length > 0) {
          envVars[key.trim()] = valueParts.join('=').trim()
        }
      }
    })
  } catch (error) {
    console.warn(`Could not load env file: ${envFile}`, error)
  }

  return {
    plugins: [react()],
    server: {
      port: 5173,
      open: true
    },
    define: {
      // Expose env variables to the client
      'import.meta.env.VITE_API_END_POINT': JSON.stringify(envVars.VITE_API_END_POINT || 'http://localhost:8080')
    }
  }
})
