import { Group } from "@mui/icons-material";
import { AppBar, Box, Button, Container, MenuItem, Toolbar, Typography } from "@mui/material";
import { yellow } from "@mui/material/colors";
import { Link, NavLink } from "react-router-dom";


export default function NavBar() {
    return (
        <Box sx={{ flexGrow: 1 }}>
            <AppBar position="static">
                <Container maxWidth='xl'>
                    <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
                        <Box sx={{ display: "flex" }}>
                            <MenuItem component={Link} to=''>
                                <Group fontSize="large" />
                                <Typography variant="h4" fontWeight='bold'>Reactivities</Typography>
                            </MenuItem>
                        </Box>
                        <Box sx={{ display: "flex" }}>
                            <MenuItem component={NavLink} to="/activities"
                                sx={{
                                    fontSize: '1.2rem',
                                    textTransform: "uppercase",
                                    fontWeight: 'bold',
                                    '&.active': { // active is class that gets added by NavLink
                                        color: "yellow"
                                    }
                                }}>Activities</MenuItem>
                            <MenuItem sx={{
                                fontSize: '1.2rem',
                                textTransform: "uppercase",
                                fontWeight: 'bold',
                                '&.active': {
                                    color: "yellow"
                                }
                            }}
                                component={NavLink} to='/createactivity'>
                                Create Activity
                            </MenuItem>
                        </Box>
                        <MenuItem>
                            User Menu
                        </MenuItem>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    )
}
