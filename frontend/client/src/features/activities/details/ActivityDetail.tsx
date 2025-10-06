import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material"
import type { Activity } from "../../../types/activity"

type ActivityDetailsProp = {
    activity: Activity
}

export default function ActivityDetail({ activity }: ActivityDetailsProp) {
    return (
        <Card>
            <CardMedia component='img' src={`/images/categoryImages/${activity.category.toLowerCase()}.jpg`} />
            <CardContent>
                <Typography variant="h5">{activity.title}</Typography>
                <Typography variant="subtitle1">{activity.eventDate}</Typography>
                <Typography variant="body1">{activity.description}</Typography>
            </CardContent>
            <CardActions>
                <Button color="primary" >Edit</Button>
                <Button color="inherit" >Cancel</Button>
            </CardActions>
        </Card>
    )
}
