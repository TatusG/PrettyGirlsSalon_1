import React from 'react';
import '../assets/styles/menu.css';
import Logo2 from '../assets/image/Logo2.jpeg';

function Menu({ onNext }) {
  return (
    <div className="menu-contenedor">
      <div className="menu-contenido">
        <img src={Logo2} alt="Logo" className="logo-grande" />
        <h1>Agregar Datos</h1>
        <form>
          <label htmlFor="nombre">Nombre: </label>
          <input 
            type="text" 
            id="nombre" 
            placeholder="Escribe tu nombre" 
          />
          <button 
            type="button"
            className="boton-redireccion"
            onClick={onNext}
          >
            Siguiente
          </button>
        </form>
      </div>
    </div>
  );
}

export default Menu;