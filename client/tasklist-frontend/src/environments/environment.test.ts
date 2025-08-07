import { version } from "os";

export const environment = {
    production: false,
    apiUrl: 'http://localhost:5039/api', // Mudança: HTTPS para HTTP
    apiTimeout: 5000,
    enableLogging: true,
    appName: 'Lista de Tarefas - Teste',
    version: '1.0.0'
};