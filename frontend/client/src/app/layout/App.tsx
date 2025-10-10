
import CssBaseline from '@mui/material/CssBaseline';
import { Box, Container } from '@mui/material';
import NavBar from './NavBar';
import { Outlet, useLocation } from 'react-router-dom';
import "../style.css";
import Home from '../../features/home/Home';

function App() {

  const location = useLocation();

  return (
    <Box sx={{ backgroundColor: "#eeeeee", minHeight: '100vh' }}>
      <CssBaseline />
      {location.pathname === '/'
        ? <Home />
        : <>
          <NavBar />
          <Container maxWidth='xl' sx={{ marginTop: 1 }}>
            <Outlet />
          </Container></>}
    </Box>
  )
}

export default App
