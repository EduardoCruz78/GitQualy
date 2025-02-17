import React, { useEffect, useState } from 'react';
import { fetchIndicators } from '../services/api';

function CollectionTable({ indicatorId }) {
  const [collections, setCollections] = useState([]);

  useEffect(() => {
    const loadCollections = async () => {
      try {
        const data = await fetchIndicators();
        const indicator = data.find((item) => item.id === indicatorId);
        setCollections(indicator?.collections || []);
      } catch (error) {
        console.error('Erro ao carregar coletas:', error);
      }
    };
    loadCollections();
  }, [indicatorId]);

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