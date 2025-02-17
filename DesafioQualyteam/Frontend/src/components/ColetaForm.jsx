import { useState } from "react";

const ColetaForm = ({ indicadores, onColeta }) => {
  const [indicadorId, setIndicadorId] = useState("");
  const [data, setData] = useState("");
  const [valor, setValor] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Valida os campos antes de enviar
    if (!indicadorId || !data || !valor) {
      alert("Preencha todos os campos.");
      return;
    }

    const novaColeta = {
      indicadorId: parseInt(indicadorId),
      data,
      valor: parseFloat(valor),
    };

    try {
      const response = await fetch("http://localhost:5240/api/Indicador/coleta", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(novaColeta),
      });

      if (!response.ok) {
        throw new Error(`Erro ao registrar coleta: ${response.status}`);
      }

      // Limpa os campos após o cadastro bem-sucedido
      setIndicadorId("");
      setData("");
      setValor("");

      // Chama o callback para atualizar a lista de coletas
      onColeta();
    } catch (error) {
      console.error("Erro ao registrar coleta:", error.message);
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

export default ColetaForm;  // Aqui está a exportação padrão (default)
