
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
    setShowform(false);
    setActivity(activity);
  }

  const [showForm, setShowform] = useState<boolean>(false);
  function showActivityForm(value: boolean) {
    setShowform(value);
  }

  return (
    <Box sx={{ backgroundColor: "#eeeeee" }}>
      <CssBaseline />
      <NavBar showActivityForm={showActivityForm} selectedActivity={selectedActivity} />
      <Container maxWidth='xl' sx={{ marginTop: 1 }}>
        <Dashboard activities={activities}
          selectedActivity={selectedActivity}
          activity={activity}
          showForm={showForm}
          showActivityForm={showActivityForm} />
      </Container>
    </Box>
  )
}

export default App
