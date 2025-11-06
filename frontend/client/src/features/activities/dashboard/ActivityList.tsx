import { Box, Typography } from "@mui/material"
import ActivityCard from "./ActivityCard"
import useActivityReactQuery from "../../../hooks/useActivityReactQuery";
import useAccountReactQuery from "../../../hooks/useAccountReactQuery";
import { useInView } from "react-intersection-observer";
import { useEffect } from "react";


export default function ActivityList() {
    const { activitiesGroup, isPending, fetchNextPage, hasNextPage } = useActivityReactQuery();
    const { userData } = useAccountReactQuery();
    const { ref, inView } = useInView({
        threshold: 0.5
    });

    useEffect(() => {
        if (inView && hasNextPage) {
            fetchNextPage();
        }
    }, [inView, hasNextPage, fetchNextPage]);

    if (isPending && !activitiesGroup) return <></>

    if (!isPending && !activitiesGroup) return <Typography variant="h3">No activity found</Typography>

    if (!isPending && activitiesGroup && activitiesGroup.pages.length == 0) return <Typography variant="h3">No activity found</Typography>

    return (
        <Box sx={{ display: "flex", flexDirection: "column", gap: 1 }}>
            {
                activitiesGroup!.pages.map((item, index) => {
                    return (
                        <Box
                            display='flex'
                            flexDirection='column'
                            gap={2}
                            key={index}>
                            {item.result.map((act, index) =>
                                <Box ref={index == item.result.length - 1 ? ref : null}>
                                    <ActivityCard key={act.id} activity={act} userData={userData} />
                                </Box>
                            )
                            }
                        </Box>
                    )
                })
            }
        </Box>
    )
}
