import {FC, useEffect} from "react";
import todosStore from "./stores/TodosStore.ts";
import {observer} from "mobx-react-lite";
import {Checkbox, List, ListItem, ListItemButton, ListItemIcon, ListItemText} from "@mui/material";
import {TodoStatus} from "./stores/TodoStore.types.ts";

const App: FC = observer(() => {

  useEffect(() => {
    todosStore.loadPage(1, 5);
  }, []);

  return (
    <List>
      {todosStore.todos.map(todo => (
        <ListItem key={todo.id}>
          <ListItemButton role={undefined} dense>
            <ListItemIcon>
              <Checkbox
                edge="start"
                checked={todo.status == TodoStatus.Completed}
                tabIndex={-1}
                disableRipple
              />
            </ListItemIcon>
            <ListItemText primary={`${todo.title} - ${TodoStatus[todo.status]}`}/>
          </ListItemButton>
        </ListItem>
      ))}
    </List>
  )
});

export default App
