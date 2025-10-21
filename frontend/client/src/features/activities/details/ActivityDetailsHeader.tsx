import { Card, Badge, CardMedia, Box, Typography, Button } from "@mui/material";
import { Link } from "react-router-dom";
import { eventDateString } from "../../../lib/common";
import { useActivityContext } from "../../../hooks/appDataContext";
import useAccountReactQuery from "../../../hooks/useAccountReactQuery";
import useActivityReactQuery from "../../../hooks/useActivityReactQuery";


export default function ActivityDetailsHeader() {
    const activity = useActivityContext();
    const { userData } = useAccountReactQuery();
    const { isUpdatingAttendee, addUpdateAttendee, isUpdating, activityUpdate } = useActivityReactQuery(activity.id);
    const hostAttendee = activity.attendees?.find(e => e.isHost);
    const isCancelled = activity.isCancelled;
    const isHost = hostAttendee?.user.id == userData?.id;
    const isGoing = activity.attendees?.find(e => e.user.id == userData?.id)?.isAttending;

    return (
        <Card sx={{ position: 'relative', mb: 2, backgroundColor: 'transparent', overflow: 'hidden' }}>
            {isCancelled && (
                <Badge
                    sx={{ position: 'absolute', left: 40, top: 20, zIndex: 1000 }}
                    color="error"
                    badgeContent="Cancelled"
                />
            )}
            <CardMedia
                component="img"
                height="300"
                image={`/images/categoryImages/${activity.category.toLowerCase()}.jpg`}
                alt={`${activity.category.toLowerCase()} image`}
            />
            <Box sx={{
                position: 'absolute',
                bottom: 0,
                width: '100%',
                color: 'white',
                padding: 2,
                display: 'flex',
                flexDirection: 'row',
                justifyContent: 'space-between',
                alignItems: 'flex-end',
                background: 'linear-gradient(to top, rgba(0, 0, 0, 1.0), transparent)',
                boxSizing: 'border-box',
            }}>
                {/* Text Section */}
                <Box>
                    <Typography variant="h4" sx={{ fontWeight: 'bold' }}>{activity.title}</Typography>
                    <Typography variant="subtitle1">{eventDateString(activity.eventDate)}</Typography>
                    <Typography variant="subtitle2">
                        Hosted by <Link to={`/profile/${hostAttendee?.user.id}`}
                            style={{ color: 'white', fontWeight: 'bold' }}>{hostAttendee?.user.displayName}</Link>
                    </Typography>
                </Box>

                {/* Buttons aligned to the right */}
                <Box sx={{ display: 'flex', gap: 2 }}>
                    {isHost ? (
                        <>
                            <Button
                                variant='contained'
                                color={isCancelled ? 'success' : 'error'}
                                onClick={async () => {
                                    activity.isCancelled = !isCancelled;
                                    await activityUpdate(activity);
                                }}
                                loading={isUpdating}
                            >
                                {isCancelled ? 'Re-activate Activity' : 'Cancel Activity'}
                            </Button>
                            <Button
                                variant="contained"
                                color="primary"
                                component={Link}
                                to={`/updateactivity/${activity.id}`}
                                disabled={isCancelled}
                            >
                                Manage Event
                            </Button>
                        </>
                    ) : (
                        <Button
                            variant="contained"
                            color={isGoing ? 'primary' : 'info'}
                            onClick={async () => {
                                await addUpdateAttendee({
                                    activityId: activity.id,
                                    isAttending: !isGoing,
                                    isHost: isHost,
                                    userId: userData!.id
                                });
                            }}
                            disabled={isUpdatingAttendee || isCancelled}
                        >
                            {isGoing ? 'Cancel Attendance' : 'Join Activity'}
                        </Button>
                    )}
                </Box>
            </Box>
        </Card>
    )
}
