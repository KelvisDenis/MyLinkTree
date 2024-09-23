// src/ProtectedRoute.js
import React from 'react';
import { Navigate } from 'react-router-dom';

export default function ProtectedRoute ({ element})  {
  const isAuthenticated = !!localStorage.getItem('token'); // Verifica se o usuário está autenticado


  return isAuthenticated ? element : <Navigate to="/" replace />;
};
