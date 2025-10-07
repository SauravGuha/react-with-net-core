import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material"
import type { Activity } from "../../../types/activity"

type ActivityDetailsProp = {
    activity: Activity,
    unSelectActivity: () => void
}

export default function ActivityDetail({ activity, unSelectActivity }: ActivityDetailsProp) {
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
                <Button color="inherit" onClick={() => { unSelectActivity() }} >Cancel</Button>
            </CardActions>
        </Card>
    )
}
