import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { Task } from '../../models/task.model';

@Component({
    selector: 'app-task-list',
    standalone: true,
    imports: [CommonModule, RouterModule],
    templateUrl: './task-list.component.html',
    styleUrls: ['./task-list.component.css']
})
export class TaskListComponent implements OnInit {
    tasks: Task[] = [];
    loading = false;

    constructor(private taskService: TaskService) { }

    ngOnInit(): void {
        this.loadTasks();
    }

    loadTasks(): void {
        this.loading = true;
        this.taskService.getAll().subscribe({
            next: (tasks) => {
                this.tasks = tasks;
                this.loading = false;
            },
            error: (error) => {
                console.error('Erro ao carregar tarefas:', error);
                this.loading = false;
            }
        });
    }

    deleteTask(id: number): void {
        if (confirm('Tem certeza que deseja excluir esta tarefa?')) {
            this.taskService.delete(id).subscribe({
                next: () => this.loadTasks(),
                error: (error) => console.error('Erro ao excluir tarefa:', error)
            });
        }
    }

    moveTask(id: number, direction: 'up' | 'down'): void {
        this.taskService.move(id, direction).subscribe({
            next: () => this.loadTasks(),
            error: (error) => console.error('Erro ao mover tarefa:', error)
        });
    }

    isHighCost(cost: number): boolean {
        return cost >= 1000;
    }
}