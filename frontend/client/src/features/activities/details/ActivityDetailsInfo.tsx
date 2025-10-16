import { CalendarToday, Info, Place } from "@mui/icons-material";
import { Box, Button, Divider, Grid, Paper, Typography } from "@mui/material";
import { useActivityContext } from "../../../hooks/appDataContext";
import MapComponent from "../../../app/component/MapComponent";
import { useState } from "react";


export default function ActivityDetailsInfo() {
    const activity = useActivityContext();
    const [mapOpen, setMapOpen] = useState(false);
    return (
        <Paper sx={{ mb: 2 }}>

            <Grid container alignItems="center" pl={2} py={1}>
                <Grid size={1}>
                    <Info color="info" fontSize="large" />
                </Grid>
                <Grid size={11}>
                    <Typography>{activity.description}</Typography>
                </Grid>
            </Grid>
            <Divider />
            <Grid container alignItems="center" pl={2} py={1}>
                <Grid size={1}>
                    <CalendarToday color="info" fontSize="large" />
                </Grid>
                <Grid size={11}>
                    <Typography>{activity.eventDate}</Typography>
                </Grid>
            </Grid>
            <Divider />

            <Grid container alignItems="center" pl={2} py={1}>
                <Grid size={1}>
                    <Place color="info" fontSize="large" />
                </Grid>
                <Grid size={11} sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center  ' }}>
                    <Typography>
                        {activity.venue}, {activity.city}
                    </Typography>
                    <Box sx={{ display: 'flex', flexDirection: 'column' }}>
                        <Button onClick={() => { setMapOpen(false) }}>Hide</Button>
                        <Button onClick={() => { setMapOpen(true) }}>Show</Button>
                    </Box>
                </Grid>
            </Grid>
            {mapOpen && <Box sx={{ height: 400, display: 'block', zIndex: 1000 }}>
                <MapComponent latitude={activity.latitude} longitude={activity.longitude} venue={activity.venue} />
            </Box>
            }
        </Paper >
    )
}