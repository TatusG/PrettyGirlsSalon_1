import { useState } from "react";
import * as API from "../../services/data";
import { useNavigate, Link } from "react-router-dom";
import '../../assets/styles/update-password.css';
import logo from '../../assets/images/iso1.png';

export function UpdatePassword() {
    const [form, setForm] = useState({
        userName: "",
        oldPassword: "",
        newPassword: "",
        confirmPassword: ""
    });
    const [error, setError] = useState(null);
    const [isLoading, setIsLoading] = useState(false);
    const navigate = useNavigate();

    const handleChange = (e) => {
        const { name, value } = e.target;
        setForm(prev => ({
            ...prev,
            [name]: value
        }));
    };

    async function handleSubmit(e) {
        e.preventDefault();
        setIsLoading(true);
        setError(null);

        // Validaciones
        if (form.newPassword !== form.confirmPassword) {
            setError("Las contraseñas no coinciden");
            setIsLoading(false);
            return;
        }

        if (form.newPassword.length < 6) {
            setError("La nueva contraseña debe tener al menos 6 caracteres");
            setIsLoading(false);
            return;
        }

        try {
            const response = await API.UpdatePassword( form.userName, form.oldPassword, form.newPassword);            
            if (response.success) {
                alert(response.message);
                navigate("/login");
            } 
        } catch (error) {
            alert(error.message);
        } finally {
            setIsLoading(false);
        }
    }

    return (
        <div className="update-password-wrapper">
            <div className="update-password-header">
                <img src={logo} alt="Logo Pretty Girl" className="update-password-logo" />
                <h1 className="update-password-saloon-name">PRETTY GIRL</h1>
                <h2 className="update-password-saloon-subtitle">BEAUTY SALON</h2>
            </div>
            
            <div className="update-password-container">
                <h2>Cambiar Contraseña</h2>
                
                <form onSubmit={handleSubmit} className="update-password-form">
                    {error && <div className="error-message">{error}</div>}

                    <div className="form-group">
                        <label>Nombre de Usuario:</label>
                        <input
                            type="text"
                            name="userName"
                            value={form.userName}
                            onChange={handleChange}
                            placeholder="Ingresa tu nombre de usuario"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Contraseña Actual:</label>
                        <input
                            type="password"
                            name="oldPassword"
                            value={form.oldPassword}
                            onChange={handleChange}
                            placeholder="Ingresa tu contraseña actual"
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Nueva Contraseña:</label>
                        <input
                            type="password"
                            name="newPassword"
                            value={form.newPassword}
                            onChange={handleChange}
                            placeholder="Mínimo 6 caracteres"
                            minLength={6}
                            required
                        />
                    </div>

                    <div className="form-group">
                        <label>Confirmar Nueva Contraseña:</label>
                        <input
                            type="password"
                            name="confirmPassword"
                            value={form.confirmPassword}
                            onChange={handleChange}
                            placeholder="Repite tu nueva contraseña"
                            minLength={6}
                            required
                        />
                    </div>

                    <button
                        type="submit"
                        className={`update-password-button ${isLoading ? 'loading' : ''}`}
                        disabled={isLoading}
                    >
                        {isLoading ? 'Actualizando...' : 'Actualizar Contraseña'}
                    </button>

                    <div className="login-link">
                        <Link to="/login">Volver a Iniciar Sesión</Link>
                    </div>
                </form>
            </div>
        </div>
    );
}