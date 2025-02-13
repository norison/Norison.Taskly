import {makeAutoObservable} from "mobx";
import {TodoDto, PagedList} from "./TodoStore.types.ts";

const baseUrl = import.meta.env.VITE_BASE_URL;

console.log("BaseURL", baseUrl);

class TodosStore {
  public totalCount: number = 0;
  public todos: TodoDto[] = [];
  public isLoading: boolean = false;

  constructor() {
    makeAutoObservable(this);
  }

  public loadPage(page: number, pageSize: number, filter?: string, sort?: string) {
    const params = new URLSearchParams({
      page: page.toString(),
      pageSize: pageSize.toString(),
    });

    if (filter) params.append("filter", filter);
    if (sort) params.append("sort", sort);

    fetch(`${baseUrl}/todos?page=1&pageSize=5`)
      .then(response => response.json())
      .then((data: PagedList<TodoDto>) => {
        this.totalCount = data.totalCount;
        this.todos = data.data;
      })
      .catch(error => console.error("Error fetching todos:", error));
  }
}

export default new TodosStore();