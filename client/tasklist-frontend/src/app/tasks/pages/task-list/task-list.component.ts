import { Component, OnInit, OnDestroy, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';
import { Subscription } from 'rxjs';

// Material Imports (sem MatChipsModule)
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTableModule } from '@angular/material/table';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatCardModule } from '@angular/material/card';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [
        CommonModule,
        RouterModule,
        MatButtonModule,
        MatIconModule,
        MatTableModule,
        MatProgressSpinnerModule,
        MatCardModule,
        MatTooltipModule
    ],
    templateUrl: './task-list.component.html',
    styleUrls: ['./task-list.component.scss']
})
export class TaskListComponent implements OnInit, OnDestroy {
    tasks: Task[] = [];
    loading = false;
    errorMessage = '';
    private subscription: Subscription = new Subscription();

    // Colunas da tabela utilizando Material
    displayedColumns: string[] = ['name', 'cost', 'dueDate', 'order', 'actions'];

    constructor(
        private taskService: TaskService,
        private cdr: ChangeDetectorRef
    ) { }

    ngOnInit(): void {
        this.loadTasks();
    }

    ngOnDestroy(): void {
        this.subscription.unsubscribe();
    }

    loadTasks(): void {
        this.loading = true;
        this.errorMessage = '';
        this.tasks = [];

        console.log('🔄 Iniciando carregamento de tarefas...');
        this.cdr.detectChanges();

        const timeoutId = setTimeout(() => {
            if (this.loading) {
                console.log('⏰ Timeout atingido, forçando parada do loading');
                this.loading = false;
                this.errorMessage = 'Timeout: Servidor demorou muito para responder';
                this.cdr.detectChanges();
            }
        }, 15000);

        const sub = this.taskService.getAll().subscribe({
            next: (tasks) => {
                clearTimeout(timeoutId);
                console.log('✅ Tarefas recebidas:', tasks);
                this.tasks = tasks || [];
                this.loading = false;
                this.cdr.detectChanges();
            },
            error: (error) => {
                clearTimeout(timeoutId);
                console.error('❌ Erro ao carregar tarefas:', error);
                this.errorMessage = error.message || 'Erro ao carregar tarefas';
                this.loading = false;
                this.tasks = [];
                this.cdr.detectChanges();
            }
        });

        this.subscription.add(sub);
    }

    deleteTask(id: number): void {
        if (confirm('Tem certeza que deseja excluir esta tarefa?')) {
            const sub = this.taskService.delete(id).subscribe({
                next: () => {
                    console.log('✅ Tarefa excluída com sucesso');
                    this.loadTasks();
                },
                error: (error) => {
                    console.error('❌ Erro ao excluir tarefa:', error);
                    this.errorMessage = error.message || 'Erro ao excluir tarefa';
                }
            });
            this.subscription.add(sub);
        }
    }

    moveTask(id: number, direction: 'up' | 'down'): void {
        const sub = this.taskService.move(id, direction).subscribe({
            next: () => {
                console.log(`✅ Tarefa movida ${direction}`);
                this.loadTasks();
            },
            error: (error) => {
                console.error('❌ Erro ao mover tarefa:', error);
                this.errorMessage = error.message || 'Erro ao mover tarefa';
            }
        });
        this.subscription.add(sub);
    }

    isHighCost(cost: number): boolean {
        return cost >= 1000;
    }

    retry(): void {
        console.log('🔄 Tentando novamente...');
        this.loadTasks();
    }

    debugState(): void {
        console.log('🐛 Estado atual:', {
            loading: this.loading,
            errorMessage: this.errorMessage,
            tasksCount: this.tasks.length,
            tasks: this.tasks
        });

        if (this.loading && this.tasks.length > 0) {
            console.log('🔧 Forçando parada do loading (debug)');
            this.loading = false;
            this.cdr.detectChanges();
        }
    }

    forceStopLoading(): void {
        console.log('🛑 Forçando parada do loading');
        this.loading = false;
        this.cdr.detectChanges();
    }
}