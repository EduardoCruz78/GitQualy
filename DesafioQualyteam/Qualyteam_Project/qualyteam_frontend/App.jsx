import React from 'react';
import Header from './src/components/Header';
import IndicatorForm from './src/components/IndicatorForm';
import CollectionTable from './src/components/CollectionTable';

function App() {
  return (
    <div className="App">
      <h1>Teste de Renderização</h1> {/* Adicione esta linha */}
      <Header />
      <main>
        <IndicatorForm />
        <CollectionTable collections={[]} />
      </main>
    </div>
  );
}

export default App;