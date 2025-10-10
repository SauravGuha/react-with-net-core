import { Grid, Typography } from "@mui/material"
import useActivities from "../../../hooks/useActivities"
import { useParams } from "react-router-dom";
import ActivityDetailsHeader from "./ActivityDetailsHeader";
import ActivityDetailsInfo from "./ActivityDetailsInfo";
import ActivityDetailsChat from "./ActivityDetailsChat";
import ActivityDetailsSidebar from "./ActivityDetailsSidebar";

export default function ActivityDetailsPage() {

    const { id } = useParams();
    const { isGettingActivity, activity } = useActivities(id);

    if (isGettingActivity) return <Typography variant="h3">Fetching</Typography>

    if (!activity) return <Typography variant="h3">Not found</Typography>
    else if (activity.id != id) return <Typography variant="h3">Fetching</Typography>
    else {
        return (
            <Grid container spacing={3}>
                <Grid size={8}>
                    <ActivityDetailsHeader activity={activity} />
                    <ActivityDetailsInfo activity={activity} />
                    <ActivityDetailsChat />
                </Grid>
                <Grid size={4}>
                    <ActivityDetailsSidebar />
                </Grid>
            </Grid>
        )
    }
}
