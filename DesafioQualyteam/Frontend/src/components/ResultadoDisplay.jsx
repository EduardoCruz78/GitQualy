import { useState } from "react";

const ResultadoDisplay = ({ indicadores }) => {
  const [indicadorId, setIndicadorId] = useState("");
  const [resultado, setResultado] = useState(null);

  const handleCalcular = async () => {
    try {
      const response = await fetch(`http://localhost:5240/api/Indicador/resultado/${indicadorId}`);
      if (!response.ok) {
        throw new Error(`Erro ao calcular resultado: ${response.status}`);
      }
      const data = await response.json();
      setResultado(data);
    } catch (error) {
      console.error("Erro ao calcular resultado:", error.message);
    }
  };

  return (
    <div className="form-container">
      <h2 className="form-title">Calcular Resultado</h2>
      <div className="form-group">
        <label htmlFor="indicadorId">Indicador:</label>
        <select
          id="indicadorId"
          name="indicadorId"
          value={indicadorId}
          onChange={(e) => setIndicadorId(e.target.value)}
          required
          className="form-select"
        >
          <option value="">Selecione um indicador</option>
          {indicadores.map((indicador) => (
            <option key={indicador.id} value={indicador.id}>
              {indicador.nome}
            </option>
          ))}
        </select>
      </div>
      <button onClick={handleCalcular} className="btn-submit">Calcular</button>
      {resultado !== null && <p>Resultado: {resultado}</p>}
    </div>
  );
};

export default ResultadoDisplay;


