import { Group } from "@mui/icons-material";
import { AppBar, Box, Container, MenuItem, Toolbar, Typography } from "@mui/material";
import { Link, NavLink } from "react-router-dom";
import useAccountReactQuery from "../../hooks/useAccountReactQuery";
import UserMenu from "./UserMenu";


export default function NavBar() {
    const { userData } = useAccountReactQuery();

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
                            <MenuItem component={NavLink} to="/about"
                                sx={{
                                    fontSize: '1.2rem',
                                    textTransform: "uppercase",
                                    fontWeight: 'bold',
                                    '&.active': { // active is class that gets added by NavLink
                                        color: "yellow"
                                    }
                                }}>About</MenuItem>
                        </Box>
                        <MenuItem>
                            {userData
                                ? <UserMenu data={userData} />
                                : <>
                                    <MenuItem component={NavLink} to='/login' sx={{
                                        '&.active': {
                                            color: 'yellow'
                                        }
                                    }}>Login</MenuItem>
                                    <MenuItem component={NavLink} to='/register' sx={{
                                        '&.active': {
                                            color: 'yellow'
                                        }
                                    }}>Register</MenuItem>
                                </>
                            }
                        </MenuItem>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    )
}
