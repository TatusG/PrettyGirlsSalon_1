import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:7207/api',
  timeout: 10000,
});

// Interceptor para errores
api.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('API Error:', error.response?.data || error.message);
    return Promise.reject(error);
  }
);

export default api;