import api from "../compartidos/api";

export const obtenerEstilistasDisponibles = async (servicioId, fecha) => {
  try {
    const respuesta = await api.get(`/estilistas/disponibles`, {
      params: { 
        servicioId,
        fecha: fecha.toISOString() 
      }
    });
    return respuesta.data;
  } catch (error) {
    console.error("Error obteniendo estilistas:", error);
    return [];
  }
};