
import { Grid } from "@mui/material"
import type { Activity } from "../../../types/activity"
import ActivityList from "./ActivityList"
import ActivityDetail from "../details/ActivityDetail"
import ActivityForm from "../form/ActivityForm"

type DashboardProps = {
    activities: Activity[],
    selectedActivity: (activity: Activity | undefined) => void,
    activity: Activity | undefined,
    showForm: boolean,
    showActivityForm: (value: boolean) => void
}


export default function Dashboard({ activities, selectedActivity, activity, showForm, showActivityForm }: DashboardProps) {
    return (
        <>
            <Grid container spacing={2}>
                <Grid size={8}>
                    <ActivityList activities={activities} selectedActivity={selectedActivity} />
                </Grid>
                <Grid size={4}>
                    {showForm ? <ActivityForm activity={activity} showActivityForm={showActivityForm} /> :
                        activity && <ActivityDetail activity={activities[0]}
                            selectedActivity={selectedActivity}
                            showActivityForm={showActivityForm} />}
                </Grid>
            </Grid>
        </>
    )
}
