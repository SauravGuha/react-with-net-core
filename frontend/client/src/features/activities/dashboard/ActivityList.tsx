import { Box, Typography } from "@mui/material"
import ActivityCard from "./ActivityCard"
import useActivityReactQuery from "../../../hooks/useActivityReactQuery";
import useAccountReactQuery from "../../../hooks/useAccountReactQuery";
import { Fragment } from "react/jsx-runtime";


export default function ActivityList() {
    const { activitiesGroup, isPending } = useActivityReactQuery();
    const { userData } = useAccountReactQuery();

    if (isPending && !activitiesGroup) return <></>

    if (!isPending && !activitiesGroup) return <Typography variant="h3">No activity found</Typography>

    if (!isPending && activitiesGroup && activitiesGroup.pages.length == 0) return <Typography variant="h3">No activity found</Typography>

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            {
                activitiesGroup!.pages.map((item, index) => {
                    return (<Fragment key={index}>
                        {item.result.map(act => <ActivityCard key={act.id} activity={act} userData={userData} />)}
                    </Fragment>)
                })
            }
        </Box>
    )
}
