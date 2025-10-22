import { Box, Paper, Tab, Tabs } from "@mui/material";
import { useState, type SyntheticEvent } from "react";


export default function ProfileContent() {
    const [value, setValue] = useState(0);
    const tabContent = [
        { label: 'About', content: <div>About</div> },
        { label: 'Photos', content: <div>Photos</div> },
        { label: 'Events', content: <div>Events</div> },
        { label: 'Followers', content: <div>Followers</div> },
        { label: 'Following', content: <div>Following</div> }
    ];

    const handleChange = (event: SyntheticEvent, newValue: number) => {
        setValue(newValue);
    };
    return (
        <Box component={Paper} sx={{ display: 'flex', alignItems: 'flex-start', mt: 2, p: 3, height: 500 }} elevation={3}>
            <Tabs
                orientation="vertical"
                variant="scrollable"
                value={value}
                onChange={handleChange}
                aria-label="Vertical tabs example"
                sx={{ borderRight: 1, height: 450, minWidth: 200 }}
            >
                {tabContent.map((item, index) =>
                    <Tab key={index} label={item.label} sx={{ mr: 3 }} />
                )}
            </Tabs>
            <Box sx={{ flexGrow: 1, p: 3 }}>
                {tabContent[value].content}
            </Box>
        </Box>
    )
}
