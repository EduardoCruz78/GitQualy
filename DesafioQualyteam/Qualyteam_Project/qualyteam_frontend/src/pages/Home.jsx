import React, { useEffect, useState } from 'react';
import { getIndicators, addCollection } from '../services/api';
import IndicatorForm from '../components/IndicatorForm';
import CollectionTable from '../components/CollectionTable';

function Home() {
  const [indicators, setIndicators] = useState([]);
  const [selectedIndicatorId, setSelectedIndicatorId] = useState(null);
  const [collections, setCollections] = useState([]);

  useEffect(() => {
    fetchIndicators();
  }, []);

  const fetchIndicators = async () => {
    try {
      const data = await getIndicators();
      setIndicators(data);
    } catch (error) {
      console.error('Erro ao listar indicadores:', error);
    }
  };

  const handleAddIndicator = (newIndicator) => {
    setIndicators([...indicators, newIndicator]);
  };

  const handleAddCollection = async (date, value) => {
    if (!selectedIndicatorId) return;
    try {
      await addCollection(selectedIndicatorId, { date, value });
      setCollections([...collections, { date, value }]);
    } catch (error) {
      console.error('Erro ao adicionar coleta:', error);
    }
  };

  return (
    <div>
      <h2>Cadastro de Indicadores</h2>
      <IndicatorForm onIndicatorCreated={handleAddIndicator} />
      <h2>Registrar Coletas</h2>
      <select
        value={selectedIndicatorId || ''}
        onChange={(e) => setSelectedIndicatorId(e.target.value)}
      >
        <option value="">Selecione um indicador</option>
        {indicators.map((indicator) => (
          <option key={indicator.id} value={indicator.id}>
            {indicator.name}
          </option>
        ))}
      </select>
      <button onClick={() => handleAddCollection(new Date(), Math.random() * 100)}>
        Adicionar Coleta Aleatória
      </button>
      <h2>Coletas Registradas</h2>
      <CollectionTable collections={collections} />
    </div>
  );
}

export default Home;