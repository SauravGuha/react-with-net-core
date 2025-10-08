import { Box, Typography } from "@mui/material"
import ActivityCard from "./ActivityCard"
import useActivities from "../../../hooks/useActivities"


export default function ActivityList() {
    const { activities, isPending } = useActivities();

    if (isPending && !activities) return <Typography variant="h3">Loading</Typography>

    if (!isPending && activities?.length == 0) return <Typography variant="h3">No activity found</Typography>

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            {
                activities!.map(item => {
                    return <ActivityCard key={item.id} activity={item} />
                })
            }
        </Box>
    )
}
