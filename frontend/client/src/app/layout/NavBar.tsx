import { Group } from "@mui/icons-material";
import { AppBar, Box, Button, Container, MenuItem, Toolbar, Typography } from "@mui/material";


export default function NavBar() {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Container maxWidth='xl'>
                    <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
                        <Box sx={{ display: "flex" }}>
                            <Group fontSize="large" />
                            <Typography variant="h4" fontWeight='bold'>Reactivities</Typography>
                        </Box>
                        <Box sx={{ display: "flex" }}>
                            <MenuItem sx={{ fontSize: '1.2rem', textTransform: "uppercase", fontWeight: 'bold' }}>Activities</MenuItem>
                            <MenuItem sx={{ fontSize: '1.2rem', textTransform: "uppercase", fontWeight: 'bold' }}>About</MenuItem>
                            <MenuItem sx={{ fontSize: '1.2rem', textTransform: "uppercase", fontWeight: 'bold' }}>Contact Us</MenuItem>
                        </Box>
                        <Button size='large' variant="contained" color="warning">Create Activity</Button>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    )
}
