import './App.css'
import {Login} from './pages/Login/Login/Login';
import {BrowserRouter,Routes,Route, Navigate} from 'react-router-dom';

function App() {
  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Navigate to="/login" replace />} />
        <Route path="/login" element={<Login />} />
      </Routes>
    </BrowserRouter>
  )
}

export default App;
