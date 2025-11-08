import { Avatar, Box, Card, CardContent, CardHeader, Tab, Tabs, Typography } from "@mui/material";
import { useRef, useState } from "react";
import { useProfileEventReactQuery } from "../../hooks/useProfileEventReactQuery";
import { eventDateString } from "../../lib/common";



export default function ProfileEvent({ userId }: { userId: string }) {
    const [value, setValue] = useState(0);
    const filterRef = useRef(value);
    const { isFetching, events } = useProfileEventReactQuery(userId, getFiltervalue(filterRef.current));

    function getFiltervalue(value: number) {
        return value == 0 ? "past" : value == 1 ? "future" : "hosting";
    }

    const handleChange = (_: React.SyntheticEvent, newValue: number) => {
        setValue(newValue);
        filterRef.current = newValue;
    };

    if (isFetching || !events) return <></>;

    return (
        <Box sx={{ width: '100%' }}>
            <Box sx={{ borderBottom: 1, borderColor: 'divider' }}>
                <Tabs value={value} onChange={handleChange} aria-label="basic tabs example">
                    <Tab label="Past" />
                    <Tab label="Future" />
                    <Tab label="Hosting" />
                </Tabs>
                <Box display='flex' marginTop={3} gap={2}>
                    {
                        events?.map((event, index) => <Card key={index} style={{width:'20%'}}>
                            <CardHeader
                                avatar={<Avatar
                                    style={{ height: 80, width: 80 }} alt={event.category}
                                    src={`/images/categoryImages/${event.category.toLocaleLowerCase()}.jpg`} />}
                            />
                            <CardContent>
                                <Typography variant="body2" sx={{ color: 'text.primary' }}>
                                    {event.title}
                                </Typography>
                                <Typography variant="body1" sx={{ color: 'text.secondary' }}>
                                    {eventDateString(event.eventDate)}
                                </Typography>
                            </CardContent>
                        </Card>)
                    }
                </Box>
            </Box>
        </Box>
    )
}
