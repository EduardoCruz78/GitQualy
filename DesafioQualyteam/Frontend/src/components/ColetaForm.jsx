import { useState } from "react";
import api from "../api/api";

const ColetaForm = ({ indicadores, onColeta }) => {
  const [indicadorId, setIndicadorId] = useState("");
  const [data, setData] = useState("");
  const [valor, setValor] = useState("");
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    if (!indicadorId || !data || !valor) {
      alert("Preencha todos os campos.");
      return;
    }
    
    setLoading(true);
    setSuccess(false);
    
    try {
      await api.post("/coletas", {
        indicadorId: parseInt(indicadorId),
        data,
        valor: parseFloat(valor)
      });
      
      setIndicadorId("");
      setData("");
      setValor("");
      setSuccess(true);
      onColeta();
      
      setTimeout(() => {
        setSuccess(false);
      }, 3000);
    } catch (error) {
      console.error("Erro ao registrar coleta:", error.response?.data || error.message);
    } finally {
      setLoading(false);
    }
  };

  const indicadorSelecionado = indicadores.find(
    (ind) => ind.id === parseInt(indicadorId)
  );

  return (
    <div className="card">
      <div className="card-header">
        <h2 className="card-title">Registrar Nova Coleta</h2>
      </div>
      
      {success && (
        <div className="success-alert">
          Coleta registrada com sucesso!
        </div>
      )}
      
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="indicadorId">Selecione o Indicador</label>
          <select
            id="indicadorId"
            name="indicadorId"
            value={indicadorId}
            onChange={(e) => setIndicadorId(e.target.value)}
            required
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
        
        {indicadorSelecionado && (
          <div className="indicador-info">
            <p>Método de cálculo: {indicadorSelecionado.formaCalculo === "Media" ? "Média" : "Soma"}</p>
          </div>
        )}
        
        <div className="form-group">
          <label htmlFor="data">Data da Coleta</label>
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
          <label htmlFor="valor">Valor Coletado</label>
          <input
            type="number"
            step="0.01"
            id="valor"
            name="valor"
            value={valor}
            onChange={(e) => setValor(e.target.value)}
            required
            className="form-input"
            placeholder="Ex: 98.5"
          />
        </div>
        
        <button 
          type="submit" 
          className="btn-submit"
          disabled={loading}
        >
          {loading ? "Registrando..." : "Registrar Coleta"}
        </button>
      </form>
    </div>
  );
};

export default ColetaForm;

