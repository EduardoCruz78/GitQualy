// Arquivo: Frontend/src/components/ColetaForm.jsx

import { useState } from "react";
import api from "../api/api";

const ColetaForm = ({ indicadores, onColeta }) => {
  const [indicadorId, setIndicadorId] = useState("");
  const [data, setData] = useState("");
  const [valor, setValor] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!indicadorId || !data || !valor) {
      alert("Preencha todos os campos.");
      return;
    }
    try {
      await api.post("/coletas", {
        indicadorId: parseInt(indicadorId),
        data,
        valor: parseFloat(valor)
      });
      setIndicadorId("");
      setData("");
      setValor("");
      onColeta();
    } catch (error) {
      console.error("Erro ao registrar coleta:", error.response?.data || error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="form-container">
      <h2 className="form-title">Registrar Coleta</h2>
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
      <div className="form-group">
        <label htmlFor="data">Data:</label>
        <input
          type="date"
          id="data"
          name="data"
          value={data}
          onChange={(e) => setData(e.target.value)}
          required
          className="form-input"
        />
      </div>
      <div className="form-group">
        <label htmlFor="valor">Valor:</label>
        <input
          type="number"
          step="0.01"
          id="valor"
          name="valor"
          value={valor}
          onChange={(e) => setValor(e.target.value)}
          required
          className="form-input"
        />
      </div>
      <button type="submit" className="btn-submit">Registrar</button>
    </form>
  );
};

export default ColetaForm;
