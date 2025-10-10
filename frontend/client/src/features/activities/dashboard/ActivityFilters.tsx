import { Box, ListItemText, MenuItem, MenuList, Paper, Typography } from "@mui/material";
import FilterListIcon from '@mui/icons-material/FilterList';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";

export default function ActivityFilters() {
    return (
        <Box sx={{ display: "flex", flexDirection: 'column', gap: 3 }}>
            <Paper sx={{ p: 3 }}>
                <Box sx={{ width: '100%' }}>
                    <Typography variant="h6" sx={{ display: "flex", alignItems: 'center', mb: 1, color: 'lightblue' }}>
                        <FilterListIcon sx={{ mr: 1 }} />
                        Filters
                    </Typography>
                    <MenuList>
                        <MenuItem>
                            <ListItemText primary="All events" />
                        </MenuItem>
                        <MenuItem>
                            <ListItemText primary="I am going" />
                        </MenuItem>
                        <MenuItem>
                            <ListItemText primary="I am hosting" />
                        </MenuItem>
                    </MenuList>
                </Box>
            </Paper>
            <Box component={Paper} sx={{ width: '100%', p: 3 }}>
                <Typography variant="h6" sx={{ display: "flex", alignItems: 'center', mb: 1, color: 'lightblue' }}>
                    <CalendarTodayIcon sx={{ mr: 1 }} />
                    Select Date
                </Typography>
                <Calendar />
            </Box>
        </Box>
    )
}
