// Arquivo: Frontend/src/App.jsx

import { useEffect, useState } from "react";
import IndicadorForm from "./components/IndicadorForm";
import ColetaForm from "./components/ColetaForm";
import ColetaUpdateForm from "./components/ColetaUpdateForm";
import ResultadoDisplay from "./components/ResultadoDisplay";
import api from "./api/api";

const App = () => {
  const [indicadores, setIndicadores] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");

  const carregarIndicadores = async () => {
    try {
      setLoading(true);
      setError("");
      const response = await api.get("/indicadores");
      setIndicadores(response.data);
    } catch (err) {
      setError(err.response?.data || "Erro ao carregar indicadores.");
    } finally {
      setLoading(false);
    }
  };

  useEffect(() => {
    carregarIndicadores();
  }, []);

  return (
    <div className="App">
      <div className="form-container">
        <h1>Desafio Qualyteam</h1>
        {loading && <p>Carregando indicadores...</p>}
        {error && <p style={{ color: "red" }}>{error}</p>}
        <button onClick={carregarIndicadores} className="btn-submit" disabled={loading}>
          {loading ? "Carregando..." : "Carregar Indicadores"}
        </button>
      </div>
      <IndicadorForm onCadastro={carregarIndicadores} />
      <ColetaForm indicadores={indicadores} onColeta={carregarIndicadores} />
      <ColetaUpdateForm onUpdate={carregarIndicadores} />
      <ResultadoDisplay indicadores={indicadores} />
    </div>
  );
};

export default App;
