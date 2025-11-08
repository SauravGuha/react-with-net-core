import { Box, ListItemText, MenuItem, MenuList, Paper, Typography } from "@mui/material";
import FilterListIcon from '@mui/icons-material/FilterList';
import CalendarTodayIcon from '@mui/icons-material/CalendarToday';
import Calendar from "react-calendar";
import "react-calendar/dist/Calendar.css";
import { useFilterContext } from "../../../hooks/appDataContext";

export default function ActivityFilters() {
    const { changeFilterBy, changeFilterDate } = useFilterContext();
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
                            <ListItemText onClick={() => changeFilterBy("All events")} primary="All events" />
                        </MenuItem>
                        <MenuItem>
                            <ListItemText onClick={() => changeFilterBy("I am going")} primary="I am going" />
                        </MenuItem>
                        <MenuItem>
                            <ListItemText onClick={() => changeFilterBy("I am hosting")} primary="I am hosting" />
                        </MenuItem>
                    </MenuList>
                </Box>
            </Paper>
            <Box component={Paper} sx={{ width: '100%', p: 3 }}>
                <Typography variant="h6" sx={{ display: "flex", alignItems: 'center', mb: 1, color: 'lightblue' }}>
                    <CalendarTodayIcon sx={{ mr: 1 }} />
                    Select Date
                </Typography>
                <Calendar onChange={(value) => changeFilterDate(value!)} />
            </Box>
        </Box>
    )
}
