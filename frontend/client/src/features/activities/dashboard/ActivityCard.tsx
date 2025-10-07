import { Button, Card, CardActions, CardContent, Chip, Typography } from "@mui/material"
import type { Activity } from "../../../types/activity"

type ActivityCardProps = {
    activity: Activity,
    selectedActivity: (activity: Activity | undefined) => void
}

export default function ActivityCard({ activity, selectedActivity }: ActivityCardProps) {
    return (
        <Card sx={{ minWidth: 275 }}>
            <CardContent>
                <Typography variant="h5">{activity.title}</Typography>
                <Typography sx={{ color: "text.secondary", mb: 1 }}>{activity.eventDate}</Typography>
                <Typography variant="body2">{activity.description}</Typography>
                <Typography variant="subtitle1">{activity.city} / {activity.venue}</Typography>
            </CardContent>
            <CardActions sx={{ display: "flex", justifyContent: "space-between", pb: 2 }}>
                <Chip label={activity.category} variant="outlined" />
                <Button size="small" variant="contained"
                    onClick={() => selectedActivity(activity)}>View</Button>
            </CardActions>
        </Card>
    )
}
