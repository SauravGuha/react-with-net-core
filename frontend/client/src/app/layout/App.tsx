
import CssBaseline from '@mui/material/CssBaseline';
import { useState } from 'react';
import type { Activity } from '../../types/activity';
import { Box, Container } from '@mui/material';
import NavBar from './NavBar';
import Dashboard from '../../features/activities/dashboard/Dashboard';

function App() {


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
    <Box sx={{ backgroundColor: "#eeeeee", minHeight:'100vh' }}>
      <CssBaseline />
      <NavBar showActivityForm={showActivityForm} selectedActivity={selectedActivity} />
      <Container maxWidth='xl' sx={{ marginTop: 1 }}>
        <Dashboard
          selectedActivity={selectedActivity}
          activity={activity}
          showForm={showForm}
          showActivityForm={showActivityForm} />
      </Container>
    </Box>
  )
}

export default App
