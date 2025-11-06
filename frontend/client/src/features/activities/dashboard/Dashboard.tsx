
import { Grid } from "@mui/material"
import ActivityList from "./ActivityList"
import ActivityFilters from "./ActivityFilters"


export default function Dashboard() {
    return (
        <>
            <Grid container spacing={2}>
                <Grid size={8}>
                    <ActivityList />
                </Grid>
                <Grid
                    size={4}
                    sx={{
                        position: 'sticky',
                        alignSelf: 'flex-start',
                        top: 75
                    }}>
                    <ActivityFilters />
                </Grid>
            </Grid>
        </>
    )
}
