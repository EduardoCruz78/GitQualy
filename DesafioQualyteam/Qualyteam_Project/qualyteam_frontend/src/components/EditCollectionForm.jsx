import React, { useState } from 'react';
import { updateCollection } from '../services/api';

function EditCollectionForm({ indicatorId, collection, onUpdate }) {
  const [newValue, setNewValue] = useState(collection.value);

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      await updateCollection(indicatorId, collection.date, newValue);
      onUpdate();
    } catch (error) {
      console.error('Erro ao atualizar coleta:', error);
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Novo Valor:
        <input
          type="number"
          value={newValue}
          onChange={(e) => setNewValue(e.target.value)}
        />
      </label>
      <button type="submit">Atualizar</button>
    </form>
  );
}

export default EditCollectionForm;