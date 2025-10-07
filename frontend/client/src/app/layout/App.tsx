
import CssBaseline from '@mui/material/CssBaseline';
import axios from 'axios';
import { useEffect, useState } from 'react';
import type { Activity } from '../../types/activity';
import { Box, Container } from '@mui/material';
import NavBar from './NavBar';
import Dashboard from '../../features/activities/dashboard/Dashboard';

function App() {

  const [activities, setActivities] = useState<Activity[]>([]);
  useEffect(() => {
    axios.get<Activity[]>("https://localhost:7135/api/activity/getallactivities")
      .then(result => {
        setActivities(result.data);
      });
  }, []);

  const [activity, setActivity] = useState<Activity | undefined>(undefined);

  function selectedActivity(activity: Activity | undefined) {
    setActivity(activity);
  }

  function unSelectActivity(){
    setActivity(undefined);
  }

  return (
    <Box sx={{ backgroundColor: "#eeeeee" }}>
      <CssBaseline />
      <NavBar />
      <Container maxWidth='xl' sx={{ marginTop: 1 }}>
        <Dashboard activities={activities} 
        selectedActivity={selectedActivity} 
        activity={activity}
        unSelectActivity={unSelectActivity} />
      </Container>
    </Box>
  )
}

export default App
