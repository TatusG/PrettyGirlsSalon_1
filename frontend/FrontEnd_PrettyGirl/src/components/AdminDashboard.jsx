import { useEffect, useState } from 'react';
import { Link, Outlet, useNavigate } from 'react-router-dom';
import '../assets/styles/dashboard.css';
import logo from '../assets/images/log1.png';
import * as API from '../services/data';

export function AdminDashboard() {
  const [adminData, setAdminData] = useState({
    userName: "",
    name: "",
    role: "Administrador"
  });

  const navigate = useNavigate();
  // Carga los datos del administrador desde sessionStorage
  // y redirige a login si no hay datos
  useEffect(() => {
    const adminSession = sessionStorage.getItem('user');
    if (adminSession) {
      const admin = JSON.parse(adminSession);
      setAdminData({
        userName: admin.userName,
        name: admin.nombre,
        role: admin.especialidad
      });
    } else {
      navigate('/login');
    }
  },[navigate]);

  async function handleSubmit(e) {
    e.preventDefault();
    // Validación de campos requeridos
    if (!adminData.userName || !adminData.name) {
      alert("Usuario y nombre son requeridos");
      return;
    }

    try {
      const response = await API.LoadStylist(adminData.userName);
      sessionStorage.setItem('token', response.token);
      sessionStorage.setItem('user', JSON.stringify({
        userName: response.usuario.usuario,
        nombre: response.usuario.nombre,
        especialidad: response.usuario.especialidad,
        email: response.usuario.email
      }));
      navigate('/admin-dashboard');
    } catch (error) {
      alert(error.message);
    }
  }

  const handleLogout = () => {
    sessionStorage.removeItem('user');
    sessionStorage.removeItem('token');
    navigate('/login');
  };

  return (
    <div className="dashboard-container admin-theme">
      {/* Sidebar con tema azul */}
      <div className="sidebar admin-sidebar">
        <div className="sidebar-header">
          <img src={logo} alt="Pretty Girl Salon" className="logo" />
          <h2 className="admin-title">Panel de Control</h2>
          <div className="admin-info">
            <p className="welcome-text">Bienvenido, <strong>{adminData.name}</strong></p>
            <p className="role-badge">Administrador</p>
          </div>
        </div>

        <nav className="sidebar-nav">
          {/* Solo opciones de administración */}
          <Link to="/admin-dashboard/estilistas" className="nav-button admin-button">
            <span className="icon">👔</span> Gestionar Estilistas
          </Link>

          <Link to="/admin-dashboard/servicios" className="nav-button admin-button">
            <span className="icon">✂️</span> Gestionar Servicios
          </Link>

          <Link to="/admin-dashboard/citas" className="nav-button admin-button">
            <span className="icon">📊</span> Reporte de Citas
          </Link>

          <Link to="/admin-dashboard/clientes" className="nav-button admin-button">
            <span className="icon">👥</span> Base de Clientes
          </Link>

          <Link to="/admin-dashboard/reportes" className="nav-button admin-button">
            <span className="icon">📈</span> Estadísticas
          </Link>
        </nav>

        <div className="sidebar-footer">
          <button onClick={handleLogout} className="logout-button admin-logout">
            <span className="icon">🚪</span> Cerrar Sesión
          </button>
        </div>
      </div>

      <div className="main-content admin-main">
        <Outlet />
      </div>
    </div>
  );
}
