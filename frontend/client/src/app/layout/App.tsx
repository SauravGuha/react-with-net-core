
import CssBaseline from '@mui/material/CssBaseline';
import { Box, Container } from '@mui/material';
import NavBar from './NavBar';
import { Outlet } from 'react-router-dom';

function App() {

  return (
    <Box sx={{ backgroundColor: "#eeeeee", minHeight: '100vh' }}>
      <CssBaseline />
      <NavBar />
      <Container maxWidth='xl' sx={{ marginTop: 1 }}>
        <Outlet />
      </Container>
    </Box>
  )
}

export default App
