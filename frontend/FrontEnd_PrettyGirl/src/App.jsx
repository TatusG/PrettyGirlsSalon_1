import { Routes, Route, Navigate } from 'react-router-dom';
import { Home } from './components/Home';
import { Login } from './operations/auth/Login'
import { DashboardEstilista } from './components/DashboardEstilista';
import { Register } from './operations/stylist-crud/Register';
import { UpdatePassword } from './operations/auth/UpdatePassword';
import { AdminDashboard } from './components/AdminDashboard';
import { Citas } from './operations/stylist-crud/Citas';

export function App() {  
  return (    
    <Routes>
      <Route path="/" element={<Home />} />        
      <Route path="/login" element={<Login />} />
      <Route path='/dashboard' element={<DashboardEstilista />} />
      <Route path='/admin/dashboard' element={<AdminDashboard />} />     
      <Route path='/registrar' element={<Register />} />
      <Route path='/actualizar-password' element={<UpdatePassword />} />      
      <Route path='/citas' element={<Citas />} />
      <Route path="*" element={<Navigate to="/" replace />} />
    </Routes>    
  );
}