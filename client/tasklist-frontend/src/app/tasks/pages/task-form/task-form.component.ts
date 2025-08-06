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
        if (id) {
            this.taskId = Number(id);
            this.isEditMode = true;
            this.loadTask();
        }
    }

    loadTask(): void {
        if (!this.taskId) return;

        this.loading = true;
        this.taskService.getById(this.taskId).subscribe({
            next: (task) => {
                this.taskForm.patchValue({
                    name: task.name,
                    cost: task.cost,
                    dueDate: this.formatDateForInput(task.dueDate)
                });
                this.loading = false;
            },
            error: (error) => {
                this.errorMessage = error.message;
                this.loading = false;
                setTimeout(() => this.router.navigate(['/tasks']), 2000);
            }
        });
    }

    onSubmit(): void {
        if (this.taskForm.valid) {
            this.loading = true;
            this.errorMessage = '';

            const formValue = this.taskForm.value;

            const taskData = {
                name: formValue.name,
                cost: Number(formValue.cost),
                dueDate: new Date(formValue.dueDate).toISOString()
            };

            if (this.isEditMode && this.taskId) {
                this.taskService.update(this.taskId, taskData as UpdateTaskDto).subscribe({
                    next: () => this.router.navigate(['/tasks']),
                    error: (error) => {
                        this.errorMessage = error.message;
                        this.loading = false;
                    }
                });
            } else {
                this.taskService.create(taskData as CreateTaskDto).subscribe({
                    next: () => this.router.navigate(['/tasks']),
                    error: (error) => {
                        this.errorMessage = error.message;
                        this.loading = false;
                    }
                });
            }
        }
    }

    onCancel(): void {
        this.router.navigate(['/tasks']);
    }

    private formatDateForInput(dateString: string): string {
        const date = new Date(dateString);
        return date.toISOString().slice(0, 16);
    }

    get name() { return this.taskForm.get('name'); }
    get cost() { return this.taskForm.get('cost'); }
    get dueDate() { return this.taskForm.get('dueDate'); }
}