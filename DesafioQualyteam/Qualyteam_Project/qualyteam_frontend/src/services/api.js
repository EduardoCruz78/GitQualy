import axios from 'axios';

const api = axios.create({
  baseURL: 'http://localhost:5000/api', // Substitua pela URL do backend
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
    console.error('Erro ao cadastrar indicador:', error);
    throw error;
  }
};

// Registrar coleta
export const createCollection = async (indicatorId, collectionData) => {
  try {
    const response = await api.post(`/indicators/${indicatorId}/collections`, collectionData);
    return response.data;
  } catch (error) {
    console.error('Erro ao registrar coleta:', error);
    throw error;
  }
};

// Atualizar coleta
export const updateCollection = async (indicatorId, collectionId, updatedData) => {
  try {
    const response = await api.put(`/indicators/${indicatorId}/collections/${collectionId}`, updatedData);
    return response.data;
  } catch (error) {
    console.error('Erro ao atualizar coleta:', error);
    throw error;
  }
};

// Listar indicadores
export const fetchIndicators = async () => {
  try {
    const response = await api.get('/indicators');
    return response.data;
  } catch (error) {
    console.error('Erro ao listar indicadores:', error);
    throw error;
  }
};