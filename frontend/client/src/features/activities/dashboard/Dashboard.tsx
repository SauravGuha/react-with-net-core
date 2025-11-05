
import { Box, Button, Grid } from "@mui/material"
import ActivityList from "./ActivityList"
import ActivityFilters from "./ActivityFilters"
import useActivityReactQuery from "../../../hooks/useActivityReactQuery"



export default function Dashboard() {
    const { isFetchingNextPage, fetchNextPage, hasNextPage } = useActivityReactQuery();
    return (
        <>
            <Grid container spacing={2}>
                <Grid size={8}>
                    <ActivityList />
                    <Box sx={{ display: "flex", justifyContent: "center", alignContent: "center", mt: 3 }}>
                        <Button onClick={() => { fetchNextPage() }}
                            loading={isFetchingNextPage}
                            disabled={!hasNextPage || isFetchingNextPage}>Fetch next</Button>
                    </Box>
                </Grid>
                <Grid
                    size={4}
                    sx={{position:'sticky',
                        alignSelf:'flex-start',
                        top:75
                    }}>
                    <ActivityFilters />
                </Grid>
            </Grid>
        </>
    )
}
