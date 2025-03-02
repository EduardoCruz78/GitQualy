
import React from 'react';

const Sidebar = ({ activeTab, setActiveTab }) => {
  const menuItems = [
    {
      id: 'dashboard',
      name: 'Dashboard',
      icon: (
        <svg className="sidebar-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <rect x="3" y="3" width="7" height="7"></rect>
          <rect x="14" y="3" width="7" height="7"></rect>
          <rect x="14" y="14" width="7" height="7"></rect>
          <rect x="3" y="14" width="7" height="7"></rect>
        </svg>
      )
    },
    {
      id: 'cadastrarIndicador',
      name: 'Cadastrar Indicador',
      icon: (
        <svg className="sidebar-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <line x1="12" y1="5" x2="12" y2="19"></line>
          <line x1="5" y1="12" x2="19" y2="12"></line>
        </svg>
      )
    },
    {
      id: 'registrarColeta',
      name: 'Registrar Coleta',
      icon: (
        <svg className="sidebar-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <path d="M12 2L2 7l10 5 10-5-10-5z"></path>
          <path d="M2 17l10 5 10-5"></path>
          <path d="M2 12l10 5 10-5"></path>
        </svg>
      )
    },
    {
      id: 'atualizarColeta',
      name: 'Atualizar Coleta',
      icon: (
        <svg className="sidebar-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <path d="M23 4v6h-6"></path>
          <path d="M20.49 15a9 9 0 1 1-2.12-9.36L23 10"></path>
        </svg>
      )
    },
    {
      id: 'relatorios',
      name: 'Relatórios',
      icon: (
        <svg className="sidebar-icon" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 24 24" fill="none" stroke="currentColor" strokeWidth="2" strokeLinecap="round" strokeLinejoin="round">
          <path d="M14 2H6a2 2 0 0 0-2 2v16a2 2 0 0 0 2 2h12a2 2 0 0 0 2-2V8z"></path>
          <polyline points="14 2 14 8 20 8"></polyline>
          <line x1="16" y1="13" x2="8" y2="13"></line>
          <line x1="16" y1="17" x2="8" y2="17"></line>
          <polyline points="10 9 9 9 8 9"></polyline>
        </svg>
      )
    }
  ];

  return (
    <nav className="sidebar">
      <div className="sidebar-header">
        <div className="sidebar-title">MENU PRINCIPAL</div>
      </div>
      
      {menuItems.slice(0, 1).map((item) => (
        <div
          key={item.id}
          className={`sidebar-item ${activeTab === item.id ? 'active' : ''}`}
          onClick={() => setActiveTab(item.id)}
        >
          {item.icon}
          <span className="sidebar-item-text">{item.name}</span>
        </div>
      ))}
      
      <div className="sidebar-divider"></div>
      <div className="sidebar-title" style={{ padding: '0 1.5rem', marginBottom: '0.5rem' }}>INDICADORES</div>

      {menuItems.slice(1, 4).map((item) => (
        <div
          key={item.id}
          className={`sidebar-item ${activeTab === item.id ? 'active' : ''}`}
          onClick={() => setActiveTab(item.id)}
        >
          {item.icon}
          <span className="sidebar-item-text">{item.name}</span>
        </div>
      ))}
      
      <div className="sidebar-divider"></div>
      <div className="sidebar-title" style={{ padding: '0 1.5rem', marginBottom: '0.5rem' }}>ANÁLISES</div>
      
      {menuItems.slice(4).map((item) => (
        <div
          key={item.id}
          className={`sidebar-item ${activeTab === item.id ? 'active' : ''}`}
          onClick={() => setActiveTab(item.id)}
        >
          {item.icon}
          <span className="sidebar-item-text">{item.name}</span>
        </div>
      ))}
      
      <div className="sidebar-footer">
        © 2025 Sistema de Indicadores
      </div>
    </nav>
  );
};

export default Sidebar;