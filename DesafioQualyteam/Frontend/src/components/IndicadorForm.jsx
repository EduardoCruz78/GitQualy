// Arquivo: Frontend/src/components/IndicadorForm.jsx

import { useState } from "react";
import api from "../api/api";

const IndicadorForm = ({ onCadastro }) => {
  const [nome, setNome] = useState("");
  const [formaCalculo, setFormaCalculo] = useState("MÉDIA");

  const handleSubmit = async (e) => {
    e.preventDefault();
    try {
      const response = await api.post("/indicadores", { nome, formaCalculo });
      if (response.status === 201) {
        setNome("");
        setFormaCalculo("MÉDIA");
        onCadastro();
      }
    } catch (error) {
      console.error("Erro ao cadastrar indicador:", error.response?.data || error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="form-container">
      <h2 className="form-title">Cadastrar Indicador</h2>
      <div className="form-group">
        <label htmlFor="nome">Nome:</label>
        <input
          type="text"
          id="nome"
          name="nome"
          value={nome}
          onChange={(e) => setNome(e.target.value)}
          required
          className="form-input"
        />
      </div>
      <div className="form-group">
        <label htmlFor="formaCalculo">Forma de Cálculo:</label>
        <select
          id="formaCalculo"
          name="formaCalculo"
          value={formaCalculo}
          onChange={(e) => setFormaCalculo(e.target.value)}
          required
          className="form-select"
        >
          <option value="MÉDIA">MÉDIA</option>
          <option value="SOMA">SOMA</option>
        </select>
      </div>
      <button type="submit" className="btn-submit">Cadastrar</button>
    </form>
  );
};

export default IndicadorForm;
