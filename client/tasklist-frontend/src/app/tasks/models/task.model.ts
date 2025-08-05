export interface Task {
    id: number;
    name: string;
    cost: number;
    dueDate: string; // ISO string format
    displayOrder: number;
}

// DTOs que correspondem ao backend
export interface CreateTaskDto {
    name: string;
    cost: number;
    dueDate: string;
}

export interface UpdateTaskDto {
    name: string;
    cost: number;
    dueDate: string;
}