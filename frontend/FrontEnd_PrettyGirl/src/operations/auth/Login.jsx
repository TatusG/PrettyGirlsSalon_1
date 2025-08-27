import { useEffect, useState } from 'react';
import { Link } from 'react-router-dom';
import * as API from '../../services/data';
import image from '../../assets/images/iso1.png';
import { useNavigate } from 'react-router-dom';
import '../../assets/styles/login.css'
import { GiToken } from 'react-icons/gi';

export function Login() {
  const [stylist, setStylist] = useState({ UserName: "", Password: "" });  
  const [error, setError] = useState("");
  const [isLoading, setIsLoading] = useState(false);

  const navigate = useNavigate();

  // Verifica si el usuario ya está autenticado
  function handleChange(e) { 
    const { name, value } = e.target; 
    setStylist(prevState => ({ 
      ...prevState,
      [name]: value
    })); 
  } 

  async function handleSubmit(e) {
    e.preventDefault();
    setIsLoading(true);
    setError(""); 
    // Validación de campos requeridos
    if (!stylist.UserName || !stylist.Password) {
      setError("Usuario y contraseña son requeridos");
      return;
    }

    try {
      const response = await API.Login(stylist.UserName, stylist.Password);
      sessionStorage.setItem('token', response.token);

      if (response.success) {
          sessionStorage.setItem('user', JSON.stringify({
          userName: response.usuario.usuario,
          nombre : response.usuario.nombre,
          especialidad: response.usuario.especialidad,
          email: response.usuario.email
        }));        

        if (response.usuario.especialidad === "Administrador") {
          alert("Bienvenido Administrador " + response.usuario.nombre);
          navigate('/admin/dashboard');
        } else {
          alert("Bienvenido " + response.usuario.nombre);
          navigate('/dashboard');
        }
      }
    } catch (error) {
      alert(error.message);
      navigate('/login');
    } finally {
      setIsLoading(false);
    }
}

  return (
    <div className="login-container">      
      <img src={image} width="150" height="150" alt="Logo del salon" className="login-logo" 
      onClick={()=>navigate('/')} style={{cursor:'pointer'}}/>      
      <h1 className="login-title">PRETTY GIRL</h1>
      <h2 className="login-sub-title">BEAUTY SALON</h2>
      <h3 className="login-form-title">Iniciar Sesión</h3>

      <div className="login-card">
        <form onSubmit={handleSubmit} className="login-form">
          {error && <div className="error-message">{error}</div>}

          <div className="form-group">
            <label htmlFor="UserName">Usuario</label>
            <input
              type="text"
              id="UserName"
              name="UserName"
              value={stylist.UserName}
              onChange={handleChange}
              className="form-input"
              placeholder="Ej: carmen"
              required
            />
          </div>

          <div className="form-group">
            <label htmlFor="Password">Contraseña</label>
            <input
              type="password"
              id="Password"
              name="Password"
              value={stylist.Password}
              onChange={handleChange}
              className="form-input"
              placeholder="Ingresa tu contraseña"
              required
            />
          </div>

          <button
            type="submit"
            className={`login-button ${isLoading ? 'loading' : ''}`}
            disabled={isLoading}
          >
            {isLoading ? 'Iniciando sesión...' : 'Iniciar Sesión'}
          </button>

          <div className="login-links">
            <Link to="/actualizar-password" className="forgot-password">
              ¿Olvidaste tu contraseña?
            </Link>
            <div className="register-link">
              ¿No tienes cuenta? <Link to="/registrar" className="register-link-text">Regístrate aquí</Link>
            </div>
          </div>
        </form>
      </div>
    </div>
  );
}