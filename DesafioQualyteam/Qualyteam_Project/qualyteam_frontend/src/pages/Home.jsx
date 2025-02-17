import React, { useEffect, useState } from 'react';
import { getIndicators, addCollection, calculateResult, updateCollection } from '../services/api';
import IndicatorForm from '../components/IndicatorForm';
import CollectionTable from '../components/CollectionTable';
import EditCollectionForm from '../components/EditCollectionForm';

function Home() {
  const [indicators, setIndicators] = useState([]);
  const [selectedIndicatorId, setSelectedIndicatorId] = useState(null);
  const [result, setResult] = useState(null);
  const [editingCollection, setEditingCollection] = useState(null);

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

  const handleAddCollection = async (date, value) => {
    if (!selectedIndicatorId) return;
    try {
      await addCollection(selectedIndicatorId, { date, value });
      fetchIndicators(); // Atualiza a lista de indicadores
    } catch (error) {
      console.error('Erro ao adicionar coleta:', error);
    }
  };

  const handleCalculateResult = async () => {
    if (!selectedIndicatorId) return;
    try {
      const calculatedResult = await calculateResult(selectedIndicatorId);
      setResult(calculatedResult);
    } catch (error) {
      console.error('Erro ao calcular resultado:', error);
    }
  };

  return (
    <div>
      <h2>Cadastro de Indicadores</h2>
      <IndicatorForm onIndicatorCreated={fetchIndicators} />

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
      <CollectionTable
        collections={
          indicators.find((i) => i.id === selectedIndicatorId)?.collections || []
        }
        onEdit={(collection) => setEditingCollection(collection)}
      />

      {editingCollection && (
        <EditCollectionForm
          indicatorId={selectedIndicatorId}
          collection={editingCollection}
          onUpdate={() => {
            setEditingCollection(null);
            fetchIndicators();
          }}
        />
      )}

      <h2>Calcular Resultado</h2>
      <button onClick={handleCalculateResult}>Calcular Resultado</button>
      {result !== null && <p>Resultado: {result}</p>}
    </div>
  );
}

export default Home;