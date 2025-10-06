
import { Grid } from "@mui/material"
import type { Activity } from "../../../types/activity"
import ActivityList from "./ActivityList"
import ActivityDetail from "../details/ActivityDetail"

type DashboardProps = {
    activities: Activity[],
    selectedActivity: (activity: Activity | undefined) => void,
    activity: Activity | undefined
}


export default function Dashboard({ activities, selectedActivity, activity }: DashboardProps) {
    return (
        <>
            <Grid container spacing={2}>
                <Grid size={8}>
                    <ActivityList activities={activities} />
                </Grid>
                <Grid size={4}>
                    {activity && <ActivityDetail activity={activities[0]} />}
                </Grid>
            </Grid>
        </>
    )
}
