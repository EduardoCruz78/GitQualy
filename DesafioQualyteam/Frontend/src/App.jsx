import { useEffect, useState } from "react";
import IndicadorForm from "./components/IndicadorForm";
import ColetaForm from "./components/ColetaForm";
import ColetaUpdateForm from "./components/ColetaUpdateForm";
import ResultadoDisplay from "./components/ResultadoDisplay";
import Header from "./components/Header";
import Footer from "./components/Footer";
import Sidebar from "./components/Sidebar";
import api from "./api/api";
import "./App.css";

const App = () => {
  const [indicadores, setIndicadores] = useState([]);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [activeTab, setActiveTab] = useState("dashboard");

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

  const renderContent = () => {
    switch (activeTab) {
      case "dashboard":
        return <ResultadoDisplay indicadores={indicadores} />;
      case "cadastrarIndicador":
        return <IndicadorForm onCadastro={carregarIndicadores} />;
      case "registrarColeta":
        return <ColetaForm indicadores={indicadores} onColeta={carregarIndicadores} />;
      case "atualizarColeta":
        return <ColetaUpdateForm onUpdate={carregarIndicadores} />;
      default:
        return <ResultadoDisplay indicadores={indicadores} />;
    }
  };

  return (
    <div className="app">
      <Header />
      <div className="main-container">
        <Sidebar activeTab={activeTab} setActiveTab={setActiveTab} />
        <main className="content">
          {loading && (
            <div className="loading-container">
              <div className="spinner"></div>
              <p>Carregando dados...</p>
            </div>
          )}
          {error && (
            <div className="error-container">
              <p>{error}</p>
              <button onClick={carregarIndicadores} className="btn-refresh">
                Tentar novamente
              </button>
            </div>
          )}
          {renderContent()}
        </main>
      </div>
      <Footer />
    </div>
  );
};

export default App;