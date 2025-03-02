import React from 'react';
import logo from '@/Assets/Qualyteam-Logo.png';

const Header = () => {
    return (
        <header className="header">
            <div className="logo">
                <img
                    src={logo}
                    alt="Qualyteam Logo"
                    className="logo-icon"
                />
                <h1>Indicadores Qualyteam</h1>
            </div>
            <div className="header-actions">
                <span>Sistema de Gestão de Indicadores</span>
            </div>
        </header>
    );
};

export default Header;