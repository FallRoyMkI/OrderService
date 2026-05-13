import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.jsx'
import { BrowserRouter, Routes, Route } from 'react-router-dom'
import Create from './assets/pages/Create';
import Orders from './assets/pages/Orders';
import Order from './assets/pages/Order';

createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
        <App />
        <Routes>
            <Route path="/" element={<Create />} />
            <Route path="/orders" element={<Orders />} />
            <Route path="/order" element={<Order />} />
        </Routes>
    </BrowserRouter>
  </StrictMode>,
)
