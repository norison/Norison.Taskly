export enum TodoStatus {
  Created = 0,
  Completed = 1,
  Cancelled = 2,
  Deleted = 3
}

export type TodoDto = {
  id: string;
  title: string;
  description?: string;
  status: TodoStatus;
  createdDateTime: Date;
  lastEditedDatetime: Date;
}

export type CreateOrUpdateTodoRequest = {
  title: string;
  description?: string;
}

export type CreateTodoResponse = {
  id: string;
}

export type ChangeTodoStatusRequest = {
  status: TodoStatus;
}

export type PagedList<T> = {
  totalCount: number;
  data: T[];
}