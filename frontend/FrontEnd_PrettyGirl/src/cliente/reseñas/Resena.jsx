import React from 'react';
import '../../assets/styles/style.css';
import Logo1 from '../../assets/image/Logo1.jpeg';

function Resena({ onBack }) {
  return (
    <div className="resena-contenedor">
      <div className="resena-formulario">
        <img src={Logo1} alt="Logo" className="logo-grande" />
        <h2>Escribe tu Reseña</h2>
        <textarea placeholder="Tu opinión..." rows="5"></textarea>
        <button 
          className="boton-volver"
          onClick={onBack}
        >
          Volver
        </button>
      </div>
    </div>
  );
}

export default Resena;