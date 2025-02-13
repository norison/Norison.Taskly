import {FC, useEffect} from "react";
import todosStore from "./stores/TodosStore.ts";
import {observer} from "mobx-react-lite";

const App: FC = observer(() => {

  useEffect(() => {
    todosStore.loadPage(1, 5);
  }, []);

  return (
    <ul>
      {todosStore.todos.map(todo => (
        <li key={todo.id}>{todo.title}</li>
      ))}
    </ul>
  )
});

export default App
