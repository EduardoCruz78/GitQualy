import { useEffect, useState } from "react";
import IndicadorForm from "./components/IndicadorForm";
import ColetaForm from "./components/ColetaForm";
import ResultadoDisplay from "./components/ResultadoDisplay";

const App = () => {
  const [indicadores, setIndicadores] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);

  const carregarIndicadores = async () => {
    try {
      setLoading(true);
      setError(null); // Limpa qualquer erro anterior
  
      const response = await fetch("http://localhost:5240/api/Indicador");
  
      if (!response.ok) {
        throw new Error(`Erro ao carregar indicadores: ${response.status}`);
      }
  
      const data = await response.json();
      setIndicadores(data);
    } catch (err) {
      setError(err.message || "Erro desconhecido ao carregar indicadores.");
    } finally {
      setLoading(false);
    }
  };

  // Efeito para carregar indicadores ao iniciar o componente
  useEffect(() => {
    carregarIndicadores();
  }, []);

  return (
    <div className="App">
      <div className="form-container">
        <h1>Desafio Qualyteam</h1>

        {/* Exibe mensagem de carregamento ou erro */}
        {loading && <p>Carregando indicadores...</p>}
        {error && <p style={{ color: "red" }}>Erro: {error}</p>}

        {/* Div para o botão de carregar indicadores */}
        <button onClick={carregarIndicadores} className="btn-submit" disabled={loading}>
          {loading ? "Carregando..." : "Carregar Indicadores"}
        </button>
      </div>

      {/* Formulário para cadastrar indicadores */}
      <IndicadorForm onCadastro={carregarIndicadores} />

      {/* Formulário para registrar coletas */}
      <ColetaForm indicadores={indicadores} onColeta={carregarIndicadores} />

      {/* Componente para exibir resultados */}
      <ResultadoDisplay indicadores={indicadores} />
    </div>
  );
};

export default App;
