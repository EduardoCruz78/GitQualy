import React from 'react';

const Footer = () => {
  const currentYear = new Date().getFullYear();
  
  return (
    <footer className="footer">
      <div className="footer-content">
        <div className="footer-links">
          <a href="#" className="footer-link">Sobre</a>
          <a href="#" className="footer-link">Suporte</a>
          <a href="#" className="footer-link">Documentação</a>
        </div>
        <p>Qualyteam - Gestão de Indicadores</p>
      </div>
      <div className="footer-copyright">
        &copy; {currentYear} Qualyteam. Todos os direitos reservados.
      </div>
    </footer>
  );
};

export default Footer;

