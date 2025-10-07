import { Group } from "@mui/icons-material";
import { AppBar, Box, Button, Container, MenuItem, Toolbar, Typography } from "@mui/material";
import type { Activity } from "../../types/activity";


type Props = {
    showActivityForm: (value: boolean) => void,
    selectedActivity: (activity: Activity | undefined) => void,
}

export default function NavBar({ showActivityForm, selectedActivity }: Props) {
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
                        <Button size='large' variant="contained" color="warning"
                            onClick={() => {
                                selectedActivity(undefined);
                                showActivityForm(true);
                            }}>Create Activity</Button>
                    </Toolbar>
                </Container>
            </AppBar>
        </Box>
    )
}
