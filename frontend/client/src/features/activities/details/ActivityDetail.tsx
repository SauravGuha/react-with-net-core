import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material"
import type { Activity } from "../../../types/activity"

type ActivityDetailsProp = {
    activity: Activity,
    showActivityForm: (value: boolean) => void,
    selectedActivity: (activity: Activity | undefined) => void,
}

export default function ActivityDetail({ activity, selectedActivity, showActivityForm }: ActivityDetailsProp) {
    return (
        <Card>
            <CardMedia component='img' src={`/images/categoryImages/${activity.category.toLowerCase()}.jpg`} />
            <CardContent>
                <Typography variant="h5">{activity.title}</Typography>
                <Typography variant="subtitle1">{activity.eventDate}</Typography>
                <Typography variant="body1">{activity.description}</Typography>
            </CardContent>
            <CardActions>
                <Button color="inherit" onClick={() => { selectedActivity(undefined) }} >Cancel</Button>
                <Button color="primary" onClick={() => showActivityForm(true)} >Edit</Button>
            </CardActions>
        </Card>
    )
}
