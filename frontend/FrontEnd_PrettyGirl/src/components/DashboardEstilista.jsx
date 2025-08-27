import { useEffect, useState } from 'react';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import '../assets/styles/dashboard.css';
import logo from '../assets/images/logo1.png';
import * as API from '../services/data';

export function DashboardEstilista() {
const [stylistData, setStylistData] = useState({
  userName: "",
  name: "",
  specialty: ""
});
const navigate = useNavigate();

useEffect(() => {
  const stylistRaw = sessionStorage.getItem('user');
  if (stylistRaw) {
    const stylist = JSON.parse(stylistRaw);
    setStylistData({
      userName: stylist.userName,
      name: stylist.nombre,
      specialty: stylist.especialidad
    });
  } else {
    navigate('/login');
  }
}, [navigate]);
const handleLogout = () => {
  sessionStorage.removeItem('user');
  sessionStorage.removeItem('token');
  navigate('/login');
};



return (
    <div className="dashboard-container stylist-theme">
      {/* Sidebar con tema rosa */}
      <div className="sidebar stylist-sidebar">
        <div className="sidebar-header">
          <img src={logo} alt="Pretty Girl Salon" className="logo" />
          <h2 className="stylist-title">Mi Espacio de Trabajo</h2>
          <div className="stylist-info">
            <p className="welcome-text">Hola, <strong>{stylistData.name}</strong>!</p>
            <p className="specialty-badge">{stylistData.specialty}</p>
          </div>
        </div>

        <nav className="sidebar-nav">
          {/* Solo opciones relevantes para estilistas */}
          <Link to="/dashboard/agenda" className="nav-button stylist-button">
            <span className="icon">📅</span> Mi Agenda
          </Link>

          <Link to="/dashboard/clientes" className="nav-button stylist-button">
            <span className="icon">💇‍♀️</span> Mis Clientes
          </Link>

          <Link to="/dashboard/servicios" className="nav-button stylist-button">
            <span className="icon">💅</span> Mis Servicios
          </Link>

          <Link to="/dashboard/rendimiento" className="nav-button stylist-button">
            <span className="icon">🌟</span> Mi Rendimiento
          </Link>

          <Link to="/dashboard/perfil" className="nav-button stylist-button">
            <span className="icon">👤</span> Mi Perfil
          </Link>
        </nav>

        <div className="sidebar-footer">
          <button onClick={handleLogout} className="logout-button stylist-logout">
            <span className="icon">👋</span> Salir
          </button>
        </div>
      </div>

      <div className="main-content stylist-main">
        <Outlet />
      </div>
    </div>
  );
}