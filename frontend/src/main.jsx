import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import CardSurvey from './components/CardSurvey.jsx'
import App from './App.jsx'

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
