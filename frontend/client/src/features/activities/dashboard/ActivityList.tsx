import { Box } from "@mui/material"
import type { Activity } from "../../../types/activity"
import ActivityCard from "./ActivityCard"


type ActivityListProps = {
    activities: Activity[]
}

export default function ActivityList({ activities }: ActivityListProps) {
    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            {
                activities.map(item => {
                    return <ActivityCard key={item.id} activity={item} />
                })
            }
        </Box>
    )
}
