import { createContext, useState } from "react";
import api from "../compartidos/api";

export const AutenticacionContext = createContext();

export function ProveedorAutenticacion({ children }) {
  const [estilista, setEstilista] = useState(null);

  const login = async (usuario, contraseña) => {
    try {
      const respuesta = await api.post('/Autentication', { 
        UserName: usuario, 
        UserPassword: contraseña 
      });
      setEstilista(respuesta.data);
      return true;
    } catch (error) {
      console.error("Error de autenticación:", error);
      return false;
    }
  };

  return (
    <AutenticacionContext.Provider value={{ estilista, login }}>
      {children}
    </AutenticacionContext.Provider>
  );
}