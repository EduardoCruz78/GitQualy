import { useEffect, useState } from "react";
import api from "../api/api";

const ColetaUpdateForm = ({ onUpdate }) => {
  const [coletas, setColetas] = useState([]);
  const [selectedColetaId, setSelectedColetaId] = useState("");
  const [data, setData] = useState("");
  const [valor, setValor] = useState("");

  useEffect(() => {
    const fetchColetas = async () => {
      try {
        const response = await api.get("/coletas");
        setColetas(response.data);
      } catch (error) {
        console.error("Erro ao buscar coletas:", error.message);
      }
    };
    fetchColetas();
  }, []);

  useEffect(() => {
    if (selectedColetaId) {
      const selected = coletas.find(c => c.id === parseInt(selectedColetaId));
      if (selected) {
        const dateObj = new Date(selected.data);
        const formattedDate = dateObj.toISOString().split("T")[0];
        setData(formattedDate);
        setValor(selected.valor);
      }
    } else {
      setData("");
      setValor("");
    }
  }, [selectedColetaId, coletas]);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const id = parseInt(selectedColetaId);
    if (!id) {
      alert("Selecione uma coleta válida.");
      return;
    }
    if (!data || !valor) {
      alert("Preencha todos os campos.");
      return;
    }
    try {
      const response = await api.put(`/coletas/${id}`, { data, valor: parseFloat(valor) });
      if (response.status !== 200) {
        throw new Error(`Erro ao atualizar coleta: ${response.status}`);
      }
      alert("Coleta atualizada com sucesso!");
      setSelectedColetaId("");
      setData("");
      setValor("");
      onUpdate();
    } catch (error) {
      console.error("Erro ao atualizar coleta:", error.message);
      alert("Erro ao atualizar coleta: " + error.message);
    }
  };

  return (
    <form onSubmit={handleSubmit} className="form-container">
      <h2 className="form-title">Atualizar Coleta</h2>
      <div className="form-group">
        <label htmlFor="coletaSelect">Selecione a Coleta:</label>
        <select
          id="coletaSelect"
          value={selectedColetaId}
          onChange={(e) => setSelectedColetaId(e.target.value)}
          required
          className="form-select"
        >
          <option value="">Selecione uma coleta</option>
          {coletas.map((coleta) => (
            <option key={coleta.id} value={coleta.id}>
              {`ID: ${coleta.id} - Data: ${new Date(coleta.data).toLocaleDateString()} - Valor: ${coleta.valor}`}
            </option>
          ))}
        </select>
      </div>
      <div className="form-group">
        <label htmlFor="data">Nova Data:</label>
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
        <label htmlFor="valor">Novo Valor:</label>
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
      <button type="submit" className="btn-submit">Atualizar Coleta</button>
    </form>
  );
};

export default ColetaUpdateForm;
