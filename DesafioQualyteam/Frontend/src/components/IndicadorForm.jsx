import { useState } from "react";
import api from "../api/api";

const IndicadorForm = ({ onCadastro }) => {
  const [nome, setNome] = useState("");
  const [formaCalculo, setFormaCalculo] = useState("Media");
  const [loading, setLoading] = useState(false);
  const [success, setSuccess] = useState(false);

  const handleSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);
    setSuccess(false);
    
    try {
      const response = await api.post("/indicadores", { nome, formaCalculo });
      if (response.status === 201) {
        setNome("");
        setFormaCalculo("Media");
        setSuccess(true);
        onCadastro();
        
       
        setTimeout(() => {
          setSuccess(false);
        }, 3000);
      }
    } catch (error) {
      console.error("Erro ao cadastrar indicador:", error.response?.data || error.message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="card">
      <div className="card-header">
        <h2 className="card-title">Cadastrar Indicador</h2>
      </div>
      
      {success && (
        <div className="success-alert">
          Indicador cadastrado com sucesso!
        </div>
      )}
      
      <form onSubmit={handleSubmit}>
        <div className="form-group">
          <label htmlFor="nome">Nome do Indicador</label>
          <input
            type="text"
            id="nome"
            name="nome"
            value={nome}
            onChange={(e) => setNome(e.target.value)}
            required
            className="form-input"
            placeholder="Ex: Taxa de Defeitos"
          />
        </div>
        
        <div className="form-group">
          <label htmlFor="formaCalculo">Método de Cálculo</label>
          <select
            id="formaCalculo"
            name="formaCalculo"
            value={formaCalculo}
            onChange={(e) => setFormaCalculo(e.target.value)}
            required
            className="form-select"
          >
            <option value="Media">Média</option>
            <option value="Soma">Soma</option>
          </select>
        </div>
        
        <button 
          type="submit" 
          className="btn-submit"
          disabled={loading}
        >
          {loading ? "Cadastrando..." : "Cadastrar Indicador"}
        </button>
      </form>
    </div>
  );
};

export default IndicadorForm;