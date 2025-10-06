
import CssBaseline from '@mui/material/CssBaseline';
import axios from 'axios';
import { useEffect, useState } from 'react';
import type { Activity } from './types/activity';
import { List, ListItem, ListItemText, Typography } from '@mui/material';

function App() {

  const [activities, setActivities] = useState<Activity[]>([]);
  useEffect(() => {
    axios.get<Activity[]>("https://localhost:7135/api/activity/getallactivities")
    .then(result=>{
      setActivities(result.data);
    });
  }, []);

  return (
    <>
      <CssBaseline />
      <Typography variant='h3'>Reactivities</Typography>
      <List>
        {
          activities.map(item=>{
            return <ListItem key={item.id}>
              <ListItem >
                <ListItemText>{item.title}</ListItemText>
              </ListItem>
            </ListItem>
          })
        }
      </List>
    </>
  )
}

export default App
