import React, { useState } from 'react';
import { createIndicator, addCollection } from '../services/api';

function IndicatorForm({ onIndicatorCreated }) {
  const [name, setName] = useState('');
  const [calculationType, setCalculationType] = useState('0'); // Valor inicial como string "0"
  const [date, setDate] = useState(''); // Data da coleta
  const [value, setValue] = useState(''); // Valor coletado

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const newIndicator = await createIndicator({
        name,
        calculationType: parseInt(calculationType, 10),
      });
      console.log('Indicador cadastrado:', newIndicator);

      if (date && value) {
        await addCollection(newIndicator.id, {
          date: new Date(date).toISOString(), // Certifique-se de enviar no formato ISO
          value: parseFloat(value),
        });
      }

      onIndicatorCreated();
      setName('');
      setCalculationType('0');
      setDate('');
      setValue('');
    } catch (error) {
      alert('Erro ao cadastrar indicador ou registrar coleta');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <label>
        Nome:
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
      </label>
      <label>
        Tipo de Cálculo:
        <select
          value={calculationType}
          onChange={(e) => setCalculationType(e.target.value)}
        >
          <option value="0">Soma</option>
          <option value="1">Média</option>
        </select>
      </label>
      <label>
        Data da Coleta:
        <input
          type="date"
          value={date}
          onChange={(e) => setDate(e.target.value)}
        />
      </label>
      <label>
        Valor Coletado:
        <input
          type="number"
          value={value}
          onChange={(e) => setValue(e.target.value)}
        />
      </label>
      <button type="submit">Cadastrar</button>
    </form>
  );
}

export default IndicatorForm;