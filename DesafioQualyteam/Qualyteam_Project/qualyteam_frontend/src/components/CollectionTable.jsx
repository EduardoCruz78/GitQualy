import React from 'react';

function CollectionTable({ collections }) {
  return (
    <table border="1">
      <thead>
        <tr>
          <th>Data</th>
          <th>Valor Coletado</th>
        </tr>
      </thead>
      <tbody>
        {collections.map((collection) => (
          <tr key={collection.id}>
            <td>{new Date(collection.date).toLocaleDateString()}</td>
            <td>{collection.value}</td>
          </tr>
        ))}
      </tbody>
    </table>
  );
}

export default CollectionTable;