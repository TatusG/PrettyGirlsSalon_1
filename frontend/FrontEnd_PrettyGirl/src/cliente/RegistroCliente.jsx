import { useState } from 'react';
import api from '../compartidos/api';

export default function RegistroCliente({ alRegistroExitoso }) {
  const [formulario, setFormulario] = useState({
    dni: '',
    nombre: '',
    telefono: '',
    correo: ''
  });
  const [errores, setErrores] = useState({});

  const validarDNI = (dni) => /^\d{8}$/.test(dni);

  const manejarEnvio = async (e) => {
    e.preventDefault();
    
    // Validaciones
    const nuevosErrores = {};
    if (!validarDNI(formulario.dni)) nuevosErrores.dni = "DNI debe tener 8 dígitos";
    if (!formulario.nombre) nuevosErrores.nombre = "Nombre es requerido";
    
    if (Object.keys(nuevosErrores).length > 0) {
      setErrores(nuevosErrores);
      return;
    }

    try {
      await api.post('/clientes', formulario);
      alRegistroExitoso();
    } catch (error) {
      setErrores({ general: "Error al registrar. Intente nuevamente." });
    }
  };

  return (
    <form onSubmit={manejarEnvio}>
      <h2>Registro de Cliente</h2>
      
      {errores.general && <div className="error">{errores.general}</div>}

      <div>
        <label>DNI:</label>
        <input
          type="text"
          value={formulario.dni}
          onChange={(e) => setFormulario({...formulario, dni: e.target.value})}
          className={errores.dni ? "error-input" : ""}
        />
        {errores.dni && <span className="error">{errores.dni}</span>}
      </div>

      {/* Más campos con validación */}

      <button type="submit">Registrarse</button>
    </form>
  );
}