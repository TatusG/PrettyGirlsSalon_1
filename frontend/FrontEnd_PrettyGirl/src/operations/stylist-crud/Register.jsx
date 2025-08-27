import { useState } from "react";
import * as API from '../../services/data';
import { useNavigate, Link } from "react-router-dom";
import '../../assets/styles/register.css';
import logo from '../../assets/images/logo3.png'; 

export function Register() {
    const [form, setForm] = useState({ 
        userName: "", 
        userPassword: "", 
        fullName: "", 
        specialty: "", 
        email: "", 
        isActive: true
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

        if (!form.email.includes('@')) {
            setError("Por favor ingresa un email válido");
            setIsLoading(false);
            return;
        }

        if (form.userPassword.length < 6) {
            setError("La contraseña debe tener al menos 6 caracteres");
            setIsLoading(false);
            return;
        }

        try {
            const response = await API.Register(
                form.userName, 
                form.userPassword, 
                form.fullName, 
                form.specialty, 
                form.email
            );
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
        <div className="register-container">
            <img src={logo} alt="Pretty Girl Salon" className="register-logo" />
            <h2>Registrar Nuevo Estilista</h2>

            <form onSubmit={handleSubmit} className="register-form">
                {error && <div className="error-message">{error}</div>}

                <div className="form-group">
                    <label>Nombre de Usuario:</label>
                    <input
                        type="text"
                        name="userName"
                        value={form.userName}
                        onChange={handleChange}
                        placeholder="Ej: carmen.estilista"
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Contraseña:</label>
                    <input
                        type="password"
                        name="userPassword"
                        value={form.userPassword}
                        onChange={handleChange}
                        placeholder="Mínimo 6 caracteres"
                        minLength={6}
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Nombre Completo:</label>
                    <input
                        type="text"
                        name="fullName"
                        value={form.fullName}
                        onChange={handleChange}
                        placeholder="Ej: Carmen Rodríguez"
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Especialidad:</label>
                    <input
                        type="text"
                        name="specialty"
                        value={form.specialty}
                        onChange={handleChange}
                        placeholder="Ej: Coloración, Cortes, etc."
                        required
                    />
                </div>

                <div className="form-group">
                    <label>Email:</label>
                    <input
                        type="email"
                        name="email"
                        value={form.email}
                        onChange={handleChange}
                        placeholder="Ej: carmen@prettygirl.com"
                        required
                    />
                </div>

                <button
                    type="submit"
                    className="register-button"
                    disabled={isLoading}
                >
                    {isLoading ? 'Registrando...' : 'Registrar Estilista'}
                </button>

                <div className="login-link">
                    ¿Ya tienes una cuenta? <Link to="/login">Inicia sesión aquí</Link>
                </div>
            </form>
        </div>
    );
}