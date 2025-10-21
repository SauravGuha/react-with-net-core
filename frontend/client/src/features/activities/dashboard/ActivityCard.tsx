import { Avatar, Box, Button, Card, CardActions, CardContent, CardHeader, Chip, Divider, Typography } from "@mui/material"
import type { Activity, UserSchema } from "../../../types"
import useActivityReactQuery from "../../../hooks/useActivityReactQuery"
import { Link } from "react-router-dom"
import ScheduleIcon from '@mui/icons-material/Schedule';
import FmdGoodIcon from '@mui/icons-material/FmdGood';
import { eventDateString } from "../../../lib/common";
import AvatarPopover from "../../../app/component/AvatarPopover";


export default function ActivityCard({ activity, userData }: { activity: Activity, userData?: UserSchema }) {
    const hostAttendee = activity.attendees?.find(e => e.isHost);
    const isHost = hostAttendee?.user.id == userData?.id;
    const isGoing = hostAttendee?.isAttending;
    const label = isHost ? "You are hosting" : "You are going";
    const isCancelled = activity.isCancelled;
    const color = isHost ? "secondary" : isGoing ? "warning" : "default";

    const { isDeleting, activityDelete } = useActivityReactQuery();

    return (
        <Card elevation={3} sx={{ minWidth: 275 }}>

            <Box sx={{ display: "flex", justifyContent: "space-between", alignItems: "center" }}>
                <CardHeader
                    sx={{
                        fontSize: 25,
                        fontWeight: "bold"
                    }}
                    avatar={<Avatar style={{ height: 80, width: 80 }} />}
                    title={<>
                        <Typography variant="h5">{activity.title}</Typography>
                    </>}
                    subheader={
                        <>
                            Hosted by <Link to={`/profile/${hostAttendee?.user.id}`}>{hostAttendee?.user.displayName}</Link>
                        </>
                    } />
                <Box sx={{ display: "flex", flexDirection: "column", gap: 2, mr: 2 }}>
                    {(isHost || isGoing) && <Chip variant="outlined" label={label} color={color} />}
                    {isCancelled && <Chip label='Cancelled' color='error' />}
                </Box>
            </Box>

            <Divider sx={{ mb: 3 }} />

            <CardContent sx={{ p: 0 }}>
                <Box sx={{ display: "flex", alignItems: "center", mb: 2, px: 2 }}>
                    <ScheduleIcon sx={{ mr: 1 }} />
                    <Typography variant="body2" sx={{ mr: 5 }}>{eventDateString(activity.eventDate)}</Typography>
                    <FmdGoodIcon sx={{ mr: 1 }} />
                    <Typography variant="body2">{activity.city} / {activity.venue}</Typography>
                </Box>
                <Divider sx={{ mb: 3 }} />
                <Box sx={{ display: "flex", gap: 2, backgroundColor: "grey.200", px: 3 }}>
                    {
                        activity.attendees?.map(item => <AvatarPopover key={userData?.id}
                            userData={item.user} />)
                    }
                </Box>
            </CardContent>

            <CardActions sx={{ display: "flex", justifyContent: "space-between", pb: 2 }}>
                <Chip label={activity.category} variant="outlined" />
                <Box sx={{ display: "flex", gap: 1 }}>
                    <Button size="small" color="warning"
                        variant="contained" loading={isDeleting}
                        onClick={() => activityDelete(activity.id)}>Delete</Button>
                    <Button component={Link} to={`/activity/${activity.id}`}
                        size="small" variant="contained">View</Button>
                </Box>
            </CardActions>
        </Card>
    )
}
