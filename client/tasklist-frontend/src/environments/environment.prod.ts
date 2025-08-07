import { version } from "os";

export const environment = {
    production: true,
    apiUrl: 'https://task-list-system.onrender.com/api',// Alterado para URL de produção
    apiTimeout: 10000, // 10 segundos
    enableLogging: false,
    appName: 'Task List System',
    version: '1.0.0',
};