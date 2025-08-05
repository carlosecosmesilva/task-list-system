import { Routes } from '@angular/router';

export const routes: Routes = [
    {
        path: '',
        redirectTo: '/tasks',
        pathMatch: 'full'
    },
    {
        path: 'tasks',
        loadComponent: () => import('./tasks/pages/task-list/task-list.component').then(m => m.TaskListComponent)
    },
    {
        path: 'tasks/new',
        loadComponent: () => import('./tasks/pages/task-form/task-form.component').then(m => m.TaskFormComponent)
    },
    {
        path: 'tasks/edit/:id',
        loadComponent: () => import('./tasks/pages/task-form/task-form.component').then(m => m.TaskFormComponent)
    },
    {
        path: '**',
        redirectTo: '/tasks'
    }
];