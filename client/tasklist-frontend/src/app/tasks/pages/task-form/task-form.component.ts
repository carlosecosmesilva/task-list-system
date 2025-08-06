import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { TaskService } from '../../services/task.service';
import { Task, CreateTaskDto, UpdateTaskDto } from '../../models/task.model';

// Material Imports
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';

@Component({
    selector: 'app-task-form',
    standalone: true,
    imports: [
        CommonModule,
        ReactiveFormsModule,
        MatCardModule,
        MatButtonModule,
        MatIconModule,
        MatInputModule,
        MatFormFieldModule,
        MatProgressSpinnerModule,
        MatDatepickerModule,
        MatNativeDateModule
    ],
    templateUrl: './task-form.component.html',
    styleUrls: ['./task-form.component.css']
})
export class TaskFormComponent implements OnInit {
    taskForm: FormGroup;
    isEditMode = false;
    taskId?: number;
    loading = false;
    errorMessage = '';

    constructor(
        private fb: FormBuilder,
        private taskService: TaskService,
        private router: Router,
        private route: ActivatedRoute
    ) {
        this.taskForm = this.fb.group({
            name: ['', [Validators.required, Validators.maxLength(100)]],
            cost: [0, [Validators.required, Validators.min(0)]],
            dueDate: ['', Validators.required]
        });
    }

    ngOnInit(): void {
        const id = this.route.snapshot.paramMap.get('id');
        console.log('🔍 ID recebido da rota:', id);

        if (id) {
            this.taskId = Number(id);
            this.isEditMode = true;
            console.log('✏️ Modo de edição ativado para tarefa ID:', this.taskId);
            this.loadTask();
        } else {
            console.log('➕ Modo de criação ativado');
        }
    }

    loadTask(): void {
        if (!this.taskId) {
            console.error('❌ ID da tarefa não encontrado');
            return;
        }

        console.log('📡 Carregando dados da tarefa ID:', this.taskId);
        this.loading = true;
        this.errorMessage = '';

        this.taskService.getById(this.taskId).subscribe({
            next: (task) => {
                console.log('✅ Dados da tarefa carregados:', task);
                this.taskForm.patchValue({
                    name: task.name,
                    cost: task.cost,
                    dueDate: this.formatDateForInput(task.dueDate)
                });
                this.loading = false;
            },
            error: (error) => {
                console.error('❌ Erro ao carregar tarefa:', error);
                this.errorMessage = error.message;
                this.loading = false;
                // Redirecionar após 3 segundos
                setTimeout(() => this.router.navigate(['/tasks']), 3000);
            }
        });
    }

    onSubmit(): void {
        if (this.taskForm.valid) {
            this.loading = true;
            this.errorMessage = '';

            const formValue = this.taskForm.value;
            console.log('📝 Dados do formulário:', formValue);

            const taskData = {
                name: formValue.name,
                cost: Number(formValue.cost),
                dueDate: new Date(formValue.dueDate).toISOString()
            };

            console.log('📦 Dados processados para envio:', taskData);

            if (this.isEditMode && this.taskId) {
                console.log('✏️ Atualizando tarefa ID:', this.taskId);
                this.taskService.update(this.taskId, taskData as UpdateTaskDto).subscribe({
                    next: (result) => {
                        console.log('✅ Tarefa atualizada com sucesso:', result);
                        this.router.navigate(['/tasks']);
                    },
                    error: (error) => {
                        console.error('❌ Erro ao atualizar tarefa:', error);
                        this.errorMessage = error.message;
                        this.loading = false;
                    }
                });
            } else {
                console.log('➕ Criando nova tarefa');
                this.taskService.create(taskData as CreateTaskDto).subscribe({
                    next: (result) => {
                        console.log('✅ Tarefa criada com sucesso:', result);
                        this.router.navigate(['/tasks']);
                    },
                    error: (error) => {
                        console.error('❌ Erro ao criar tarefa:', error);
                        this.errorMessage = error.message;
                        this.loading = false;
                    }
                });
            }
        } else {
            console.error('❌ Formulário inválido:', this.taskForm.errors);
        }
    }

    onCancel(): void {
        console.log('❌ Cancelando operação');
        this.router.navigate(['/tasks']);
    }

    private formatDateForInput(dateString: string): string {
        const date = new Date(dateString);
        // Formato para input datetime-local: YYYY-MM-DDTHH:MM
        return date.toISOString().slice(0, 16);
    }

    get name() { return this.taskForm.get('name'); }
    get cost() { return this.taskForm.get('cost'); }
    get dueDate() { return this.taskForm.get('dueDate'); }
}