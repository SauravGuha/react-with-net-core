import { Paper, Typography, List, ListItem, Chip, ListItemAvatar, Avatar, ListItemText, Grid } from "@mui/material";
import { useActivityContext } from "../../../hooks/appDataContext";

export default function ActivityDetailsSidebar() {

    const activity = useActivityContext();
    const hostAttendee = activity.attendees?.find(e => e.isHost);
    const attendeesCount = activity?.attendees?.filter(e => e.isAttending).length;

    return (
        <>
            <Paper
                sx={{
                    textAlign: 'center',
                    border: 'none',
                    backgroundColor: 'primary.main',
                    color: 'white',
                    p: 2,
                }}
            >
                <Typography variant="h6">
                    {attendeesCount} people going
                </Typography>
            </Paper>
            <Paper sx={{ padding: 2 }}>
                {activity?.attendees?.filter(e => e.isAttending).map(att => <Grid key={att.user.id} container alignItems="center">
                    <Grid size={8}>
                        <List sx={{ display: 'flex', flexDirection: 'column' }}>
                            <ListItem>
                                <ListItemAvatar>
                                    <Avatar
                                        variant="rounded"
                                        alt={att.user.displayName + ' image'}
                                        src={att.user.imageUrl ? att.user.imageUrl : '/assets/user.png'}
                                        sx={{ width: 75, height: 75, mr: 3 }}
                                    />
                                </ListItemAvatar>
                                <ListItemText>
                                    <Typography variant="h6">{att.user.displayName}</Typography>
                                    {att.isAttending && (
                                        <Typography variant="body2" color="orange">
                                            Following
                                        </Typography>
                                    )}
                                </ListItemText>
                            </ListItem>
                        </List>
                    </Grid>
                    <Grid size={4} sx={{ display: 'flex', flexDirection: 'column', alignItems: 'flex-end', gap: 1 }}>
                        {hostAttendee?.user.id == att.user?.id && (
                            <Chip
                                label="Host"
                                color="warning"
                                variant='filled'
                                sx={{ borderRadius: 2 }}
                            />
                        )}
                    </Grid>
                </Grid>)}

            </Paper>
        </>
    );
}