import React from 'react';
import { Link } from 'react-router-dom';
import '../assets/styles/home.css';

// Importación de imágenes (rutas relativas desde este archivo)
import cortesImage from '../assets/images/cortes.png';
import maquillajeImage from '../assets/images/maquillaje.png';
import manicureImage from '../assets/images/manicure.png';
import alisadoImage from '../assets/images/alisado.png';
import portadaImage from '../assets/images/portaadaa.png';
import stylistBgImage from '../assets/images/estilista.png';
import salonBgImage from '../assets/images/salon.png';
import salonLogoWhite from '../assets/images/log3.png';

export function Home() {
  return (
    <div className="home-container">
      {/* Sección Hero */}
      <div className="hero-section" style={{ backgroundImage: `url(${portadaImage})` }}>
        <div className="hero-overlay"></div>
        <div className="hero-content">          
          <p className="salon-tagline">Tu belleza es nuestra pasión</p>
        </div>
      </div>

      {/* Sección de Servicios */}
      <div className="services-section">
        <h3>Nuestros Servicios Exclusivos</h3>

        <div className="services-grid">
          <div className="service-card">
            <div className="service-image">
              <img src={cortesImage} alt="Modelo con un moderno corte de cabello" />
            </div>
            <h4>Cortes & Peinados</h4>
            <p>Estilos modernos que realzan tu belleza</p>
          </div>

          <div className="service-card">
            <div className="service-image">
              <img src={manicureImage} alt="Manos con uñas perfectamente arregladas" />
            </div>
            <h4>Manicure & Pedicure</h4>
            <p>Uñas perfectas para cada ocasión</p>
          </div>

          <div className="service-card">
            <div className="service-image">
              <img src={maquillajeImage} alt="Maquillaje profesional aplicado" />
            </div>
            <h4>Maquillaje Profesional</h4>
            <p>Resalta tus mejores rasgos</p>
          </div>

          <div className="service-card">
            <div className="service-image">
              <img src={alisadoImage} alt="Cabello liso después de tratamiento" />
            </div>
            <h4>Tratamientos de Alisado</h4>
            <p>Cabello liso y manejable por más tiempo</p>
          </div>
        </div>
      </div>

      {/* Sección de Acceso */}
      <div className="access-section">
        <h3>¿Cómo deseas acceder?</h3>

        <div className="access-buttons">
          {/* Botón para estilistas con imagen de fondo */}
          <Link to="/login" className="access-button stylist-btn">
            <div className="access-content">              
              <h4>Soy Estilista</h4>
              <p>Accede a tu panel profesional</p>              
            </div>
            <img src={stylistBgImage} alt="Estilista"/>
          </Link>

          {/* Botón para clientes con logo */}
          <Link to="/cita" className="access-button client-btn">
            <div className="access-content">
              <h4>Reserva tu Experiencia</h4>
              <p>Agenda tu cita ahora mismo</p>              
            </div>
            <img src={salonLogoWhite} alt="Cita"/>
          </Link>
        </div>
      </div>

      {/* Sección de Testimonios */}
      <div className="testimonials-section">
        <h3>Lo que dicen nuestras clientas</h3>

        <div className="testimonial-card">
          <p>"¡El mejor servicio que he recibido! Carmen es una artista con las tijeras."</p>
          <div className="testimonial-author">
            <span>⭐️⭐️⭐️⭐️⭐️</span>
            <p>- María G.</p>
          </div>
        </div>
      </div>
    </div>
  );
}