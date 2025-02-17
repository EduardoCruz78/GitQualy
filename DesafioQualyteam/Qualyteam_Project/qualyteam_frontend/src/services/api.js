import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5001/api', // Certifique-se de que o backend está rodando nesta URL
  headers: {
    'Content-Type': 'application/json',
  },
});

// Cadastro de indicador
export const createIndicator = async (indicatorData) => {
  try {
    const response = await api.post('/indicators', indicatorData);
    return response.data;
  } catch (error) {
    console.error('Erro ao cadastrar indicador:', error.response?.data || error.message);
    throw error;
  }
};

// Registrar coleta
export const addCollection = async (indicatorId, collectionData) => {
  try {
    const response = await api.post(`/indicators/${indicatorId}/collections`, collectionData);
    return response.data;
  } catch (error) {
    console.error('Erro ao registrar coleta:', error.response?.data || error.message);
    throw error;
  }
};

// Atualizar coleta
export const updateCollection = async (indicatorId, collectionId, updatedData) => {
  try {
    const response = await api.put(`/indicators/${indicatorId}/collections/${collectionId}`, updatedData);
    return response.data;
  } catch (error) {
    console.error('Erro ao atualizar coleta:', error.response?.data || error.message);
    throw error;
  }
};

// Listar indicadores
export const fetchIndicators = async () => {
  try {
    const response = await api.get('/indicators');
    return response.data;
  } catch (error) {
    console.error('Erro ao listar indicadores:', error.response?.data || error.message);
    throw error;
  }
};

// Calcular resultado
export const calculateResult = async (indicatorId) => {
  try {
    const response = await api.get(`/indicators/${indicatorId}/result`);
    return response.data;
  } catch (error) {
    console.error('Erro ao calcular resultado:', error.response?.data || error.message);
    throw error;
  }
};