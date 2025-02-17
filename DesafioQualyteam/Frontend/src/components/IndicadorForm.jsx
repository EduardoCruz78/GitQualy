import { useState } from "react";

const IndicadorForm = ({ onCadastro }) => {
  const [nome, setNome] = useState("");
  const [formaCalculo, setFormaCalculo] = useState("MÉDIA");

  const handleSubmit = async (e) => {
    e.preventDefault();

    // Cria o objeto com os dados do novo indicador
    const novoIndicador = { nome, formaCalculo };

    try {
      // Envia a requisição POST para o backend
      const response = await fetch("http://localhost:5240/api/Indicador/cadastrar", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          "Accept": "*/*", 
        },
        body: JSON.stringify(novoIndicador),
      });

      if (!response.ok) {
        throw new Error(`Erro ao cadastrar indicador: ${response.status}`);
      }

      // Limpa os campos após o cadastro bem-sucedido
      setNome("");
      setFormaCalculo("MÉDIA");

      // Chama o callback para atualizar a lista de indicadores
      onCadastro();
    } catch (error) {
      console.error("Erro ao cadastrar indicador:", error.message);
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
