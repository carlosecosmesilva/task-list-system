import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError, timeout } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Task } from '../models/task.model';
import { environment } from '../../../environments/environment.test';

@Injectable({
    providedIn: 'root'
})
export class TaskService {
    private apiUrl = `${environment.apiUrl}/task`;

    constructor(private http: HttpClient) {
        if (environment.enableLogging) {
            console.log('TaskService inicializado com URL:', this.apiUrl);
        }
    }

    getAll(): Observable<Task[]> {
        return this.http.get<Task[]>(this.apiUrl)
            .pipe(
                timeout(environment.apiTimeout),
                catchError(this.handleError)
            );
    }

    getById(id: number): Observable<Task> {
        return this.http.get<Task>(`${this.apiUrl}/${id}`)
            .pipe(
                timeout(environment.apiTimeout),
                catchError(this.handleError)
            );
    }

    create(task: Omit<Task, 'id' | 'displayOrder'>): Observable<Task> {
        if (environment.enableLogging) {
            console.log('Criando tarefa:', task);
        }

        return this.http.post<Task>(this.apiUrl, task)
            .pipe(
                timeout(environment.apiTimeout),
                catchError(this.handleError)
            );
    }

    update(id: number, task: Partial<Task>): Observable<Task> {
        if (environment.enableLogging) {
            console.log('Atualizando tarefa:', id, task);
        }

        return this.http.put<Task>(`${this.apiUrl}/${id}`, task)
            .pipe(
                timeout(environment.apiTimeout),
                catchError(this.handleError)
            );
    }

    delete(id: number): Observable<void> {
        if (environment.enableLogging) {
            console.log('Excluindo tarefa:', id);
        }

        return this.http.delete<void>(`${this.apiUrl}/${id}`)
            .pipe(
                timeout(environment.apiTimeout),
                catchError(this.handleError)
            );
    }

    move(id: number, direction: 'up' | 'down'): Observable<void> {
        if (environment.enableLogging) {
            console.log('Movendo tarefa:', id, direction);
        }

        return this.http.patch<void>(`${this.apiUrl}/${id}/move?direction=${direction}`, {})
            .pipe(
                timeout(environment.apiTimeout),
                catchError(this.handleError)
            );
    }

    private handleError = (error: HttpErrorResponse) => {
        let errorMessage = 'Erro desconhecido';

        if (environment.enableLogging) {
            console.error('Erro HTTP completo:', error);
        }

        if (error.error instanceof ErrorEvent) {
            // Erro do lado do cliente
            errorMessage = `Erro de conectividade: ${error.error.message}`;
        } else {
            // Erro do lado do servidor
            switch (error.status) {
                case 0:
                    errorMessage = 'Erro de conexão. Verifique se o servidor está rodando.';
                    break;
                case 409:
                    errorMessage = 'Uma tarefa com esse nome já existe.';
                    break;
                case 404:
                    errorMessage = 'Tarefa não encontrada.';
                    break;
                case 400:
                    errorMessage = error.error || 'Dados inválidos fornecidos.';
                    break;
                case 500:
                    errorMessage = 'Erro interno do servidor.';
                    break;
                default:
                    errorMessage = `Erro ${error.status}: ${error.error || error.message}`;
            }
        }

        if (environment.enableLogging) {
            console.error('Erro processado:', errorMessage);
        }

        return throwError(() => new Error(errorMessage));
    };
}