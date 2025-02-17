import React, { useState } from 'react';
import { createIndicator } from '../services/api';

function IndicatorForm({ onIndicatorCreated }) {
  const [name, setName] = useState('');
  const [calculationType, setCalculationType] = useState('0'); // Valor inicial como string "0"

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      // Converta calculationType para número antes de enviar
      const newIndicator = await createIndicator({
        name,
        calculationType: parseInt(calculationType, 10), // Converte para número
      });
      console.log('Indicador cadastrado:', newIndicator);
      onIndicatorCreated(newIndicator); // Callback para atualizar a lista de indicadores
      setName('');
      setCalculationType('0'); // Reinicia o valor padrão
    } catch (error) {
      alert('Indicador Cadastrado');
    }
  };

  return (
    <form onSubmit={handleSubmit}>
      <h2>Cadastrar Indicador</h2>
      <div>
        <label>Nome:</label>
        <input
          type="text"
          value={name}
          onChange={(e) => setName(e.target.value)}
          required
        />
      </div>
      <div>
        <label>Tipo de Cálculo:</label>
        <select
          value={calculationType}
          onChange={(e) => setCalculationType(e.target.value)} // Mantém como string até o envio
        >
          <option value="0">Soma</option> {/* Use valores numéricos como strings */}
          <option value="1">Média</option>
        </select>
      </div>
      <button type="submit">Cadastrar</button>
    </form>
  );
}

export default IndicatorForm;