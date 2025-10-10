import { Button, Card, CardActions, CardContent, CardMedia, Typography } from "@mui/material"
import useActivities from "../../../hooks/useActivities"
import { Link, useParams } from "react-router-dom";

export default function ActivityDetail() {

    const { id } = useParams();
    const { isGettingActivity, activity } = useActivities(id);

    if (isGettingActivity) return <Typography variant="h3">Fetching</Typography>

    if (!activity) return <Typography variant="h3">Not found</Typography>
    else {
        return (
            <Card>
                <CardMedia component='img' src={`/images/categoryImages/${activity.category.toLowerCase()}.jpg`} />
                <CardContent>
                    <Typography variant="h5">{activity.title}</Typography>
                    <Typography variant="subtitle1">{activity.eventDate}</Typography>
                    <Typography variant="body1">{activity.description}</Typography>
                </CardContent>
                <CardActions>
                    <Button component={Link} to="/activities" color="inherit">Cancel</Button>
                    <Button component={Link} to={`/updateactivity/${id}`} color="primary">Edit</Button>
                </CardActions>
            </Card>
        )
    }
}
