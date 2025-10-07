import { Box } from "@mui/material"
import type { Activity } from "../../../types/activity"
import ActivityCard from "./ActivityCard"


type ActivityListProps = {
    activities: Activity[],
    selectedActivity: (activity: Activity | undefined) => void
}

export default function ActivityList({ activities, selectedActivity }: ActivityListProps) {
    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            {
                activities.map(item => {
                    return <ActivityCard key={item.id} activity={item} selectedActivity={selectedActivity} />
                })
            }
        </Box>
    )
}
