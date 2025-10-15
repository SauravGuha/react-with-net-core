
import CssBaseline from '@mui/material/CssBaseline';
import { Box, Container, LinearProgress } from '@mui/material';
import NavBar from './NavBar';
import { Outlet, useLocation } from 'react-router-dom';
import "../style.css";
import Home from '../../features/home/Home';
import { LoadingContext } from '../../hooks/appDataContext';
import { useState } from 'react';

function App() {

  const location = useLocation();
  
  const [loading, setLoading] = useState<boolean>(false);
  function updateIsloading(value: boolean) {
    setLoading(value);
  }

  return (
    <LoadingContext.Provider value={{ isLoading: loading, loading: updateIsloading }}>
      <Box sx={{ backgroundColor: "#eeeeee", minHeight: '100vh' }}>
        <CssBaseline />
        {
          loading ? <LinearProgress sx={{ marginTop: 1, marginBottom: 1 }} /> : <></>
        }

        {location.pathname === '/'
          ? <Home />
          : <>
            <NavBar />
            <Container maxWidth='xl' sx={{ marginTop: 1 }}>
              <Outlet />
            </Container></>}
      </Box>
    </LoadingContext.Provider>
  )
}

export default App
