import { useState, useEffect } from "react";
import api from "../api/api";

const ResultadoDisplay = ({ indicadores }) => {
  const [indicadorId, setIndicadorId] = useState("");
  const [resultado, setResultado] = useState(null);
  const [loading, setLoading] = useState(false);
  const [error, setError] = useState("");
  const [totais, setTotais] = useState({
    indicadores: 0,
    coletas: 0
  });

  useEffect(() => {
    const fetchTotais = async () => {
      try {
        const indicadoresRes = await api.get("/indicadores");
        const coletasRes = await api.get("/coletas");
        
        setTotais({
          indicadores: indicadoresRes.data.length,
          coletas: coletasRes.data.length
        });
      } catch (error) {
        console.error("Erro ao buscar totais:", error);
      }
    };
    
    fetchTotais();
  }, []);

  const handleCalcular = async () => {
    if (!indicadorId) {
      setError("Selecione um indicador para calcular.");
      return;
    }
    
    setLoading(true);
    setError("");
    
    try {
      const response = await api.get(`/indicadores/${indicadorId}/resultado`);
      setResultado(response.data);
    } catch (error) {
      console.error("Erro ao calcular resultado:", error.response?.data || error.message);
      setError("Não foi possível calcular o resultado. Verifique se existem coletas para este indicador.");
    } finally {
      setLoading(false);
    }
  };

  const indicadorSelecionado = indicadores.find(
    (ind) => ind.id === parseInt(indicadorId)
  );

  return (
    <div className="dashboard-container">
      <h2 className="page-title">Dashboard</h2>
      
      <div className="dashboard-grid">
        <div className="stat-card">
          <div className="stat-title">Total de Indicadores</div>
          <div className="stat-value">{totais.indicadores}</div>
          <div className="stat-icon">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
              <line x1="12" y1="20" x2="12" y2="10"></line>
              <line x1="18" y1="20" x2="18" y2="4"></line>
              <line x1="6" y1="20" x2="6" y2="16"></line>
            </svg>
          </div>
        </div>
        
        <div className="stat-card">
          <div className="stat-title">Total de Coletas</div>
          <div className="stat-value">{totais.coletas}</div>
          <div className="stat-icon">
            <svg xmlns="http://www.w3.org/2000/svg" width="24" height="24" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
              <path d="M21 15a2 2 0 0 1-2 2H7l-4 4V5a2 2 0 0 1 2-2h14a2 2 0 0 1 2 2z"></path>
            </svg>
          </div>
        </div>
      </div>

      <div className="card">
        <div className="card-header">
          <h3 className="card-title">Calcular Resultado do Indicador</h3>
        </div>
        
        {error && <div className="error-container">{error}</div>}
        
        <div className="form-group">
          <label htmlFor="indicadorId">Selecione o Indicador:</label>
          <select
            id="indicadorId"
            name="indicadorId"
            value={indicadorId}
            onChange={(e) => setIndicadorId(e.target.value)}
            className="form-select"
          >
            <option value="">-- Selecione um indicador --</option>
            {indicadores.map((indicador) => (
              <option key={indicador.id} value={indicador.id}>
                {indicador.nome}
              </option>
            ))}
          </select>
        </div>
        
        
        
        <button 
          onClick={handleCalcular} 
          className="btn-submit"
          disabled={loading}
        >
          {loading ? (
            <>
              <span className="spinner-sm"></span>
              Calculando...
            </>
          ) : (
            "Calcular Resultado"
          )}
        </button>
        
        {resultado !== null && (
          <div className="resultado-container">
            <h3 className="resultado-titulo">Resultado do Indicador</h3>
            <div className="resultado-valor">
              {resultado}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default ResultadoDisplay;