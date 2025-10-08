import { Box, Typography } from "@mui/material"
import type { Activity } from "../../../types/activity"
import ActivityCard from "./ActivityCard"
import useActivities from "../../../hooks/useActivities"


type ActivityListProps = {
    selectedActivity: (activity: Activity | undefined) => void
}

export default function ActivityList({ selectedActivity }: ActivityListProps) {
    const { activities, isPending } = useActivities();

    if (isPending && !activities) return <Typography variant="h3">Loading</Typography>

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            {
                activities!.map(item => {
                    return <ActivityCard key={item.id} activity={item} selectedActivity={selectedActivity} />
                })
            }
        </Box>
    )
}
